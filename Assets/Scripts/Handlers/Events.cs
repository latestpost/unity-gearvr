using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO; 
using System.Text;

public class Events : MessageHandler 

{
	private GameObject[] goListArray;
	private List<GameObject> goList;
	private CubeContainer cubeContainer;
	public GameObject container;
	private GameState gameState;

	void Start () 
	{
		goListArray = Resources.LoadAll<GameObject>("Prefabs");
		goList = goListArray.ToList();
		buildMenu();
		this.gameState = GameState.GetInstance;
	}

	void buildMenu() {
		int index = 0;


		while (index < goList.Count()) {
			GameObject toBuild = goList [index];
			GameObject createObject = Instantiate (toBuild) as GameObject;
			Rigidbody rb = createObject.transform.GetComponent<Rigidbody>();
			Destroy(rb);
			Vector3 v = createObject.transform.position;
			v.x = v.x + index * 2f;
			createObject.transform.position = v;
			index++;
		}

	}

	IEnumerator SaveWWW(string data){
		string url = "http://192.168.0.3:8000/savelevel.php";

		WWWForm form = new WWWForm();
		form.AddField("data", data);
		WWW www = new WWW(url, form);
		yield return www;

			// check for errors
			if (www.error == null)
			{
				Debug.Log("WWW Ok!: " + www.data);
			} else {
				Debug.Log("WWW Error: "+ www.error);
			}

	}

	IEnumerator LoadWWW() {
		string url = "http://192.168.0.3:8000/level.xml";
		WWW www = new WWW(url);
		yield return www;
		Debug.Log (www.text);


		cubeContainer = new CubeContainer();
		//var serializer = new XmlSerializer(typeof(CubeContainer));
		//var stream = new FileStream(Application.persistentDataPath+"/level.xml", FileMode.Open);
		//var items = serializer.Deserialize(stream) as CubeContainer;
		//stream.Close();

		var items = new XmlSerializer(typeof(CubeContainer)).Deserialize(new StringReader(www.text)) as CubeContainer;
		//var items = serializer.Deserialize(www.text) as CubeContainer;

		int counter = 0;

		while (counter < items.Cubes.Count) {
			Cube cube = items.Cubes[counter] as Cube;
			Debug.Log (cube.Name);
			GameObject instance = Instantiate(Resources.Load("Prefabs/" + cube.Name, typeof(GameObject))) as GameObject;
			instance.transform.parent = container.transform;
			instance.transform.position = cube.Position;
			instance.transform.rotation = cube.Rotation;
			counter++;
		}
	}

	public override void HandleMessage (Message message)
	{
		GameObject createObject;
		if (message.Type == MessageType.AddPrefab) {

			int index = this.gameState.prefabInt;
			Debug.Log (this.gameState.prefabInt);
			GameObject toBuild = goList [index];

			createObject = Instantiate (toBuild) as GameObject;
				
			Vector3 v = message.GameObjectValue.transform.position;
			Vector3 playerForward = message.GameObjectValue.transform.root.gameObject.transform.TransformDirection (Vector3.forward);
			v.y = v.y - 0.1f;
			v += playerForward;
			v += playerForward;
			v += playerForward;

			createObject.transform.position = v;
			createObject.transform.parent = container.transform;
		}

		if (message.Type == MessageType.Blowup) {
			Vector3 cF = transform.TransformDirection(Vector3.up);
			foreach(Transform child in container.transform)
			{
				child.gameObject.GetComponent<Rigidbody>().AddForce(cF * 1000);
			}
		}

		if (message.Type == MessageType.RemovePrefab) {

			Vector3 v = message.GameObjectValue.transform.position;
			Vector3 playerForward = message.GameObjectValue.transform.root.gameObject.transform.TransformDirection (Vector3.forward);
			v.y = v.y - 0.1f;
			RaycastHit hit;

			Debug.DrawRay (v, playerForward, Color.green);

			if (Physics.Raycast (v, playerForward, out hit, 4f)) {
				Debug.Log (hit);
				Destroy (hit.transform.gameObject);
			}
		}

		if (message.Type == MessageType.SaveObjects) {
			cubeContainer = new CubeContainer();
			foreach(Transform child in container.transform)
			{
				Cube cube = new Cube ();
				string[] words = child.name.Split ('(');
				cube.Name = words [0];
				cube.Position = child.position;
				cube.Rotation = child.rotation;
				cubeContainer.Cubes.Add (cube);
			}

			var serializer = new XmlSerializer (typeof(CubeContainer));
			var stream = new FileStream (Application.dataPath + "levellocal.xml", FileMode.Create);
			serializer.Serialize (stream, cubeContainer);
			stream.Close ();

			string text = System.IO.File.ReadAllText(Application.dataPath + "levellocal.xml");

			StartCoroutine(SaveWWW(text));

		}

		if (message.Type == MessageType.LoadObjects) {

			StartCoroutine(LoadWWW());

		}

		if (message.Type == MessageType.ClearObjects) {
			Debug.Log ("destroying");
			foreach(Transform child in container.transform)
			{
				Destroy(child.gameObject);
			}
		}
	}
}