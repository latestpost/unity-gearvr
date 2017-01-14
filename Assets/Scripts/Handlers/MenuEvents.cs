using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MenuEvents : MessageHandler 

{
	public GameObject player;
	public GameObject currentSpawn;
	private GameObject[] goListArray;
	private List<GameObject> goList;
	private GameState gameState;

	void Start () 
	{
		goListArray = Resources.LoadAll<GameObject>("Prefabs");
		goList = goListArray.ToList();
		this.gameState = GameState.GetInstance;
	}

	void setSpawn(int index) {
		GameObject toBuild = goList[index];
		Destroy (currentSpawn);
		currentSpawn = Instantiate (toBuild) as GameObject;
		Vector3 v = new Vector3 (0.1f, 2f, 3f);
		Vector3 scale = new Vector3 (1f, 1f, 1f);
		currentSpawn.transform.SetParent(transform);
		currentSpawn.transform.localPosition = v;
		currentSpawn.transform.localScale = scale;
		Rigidbody rb = currentSpawn.transform.GetComponent<Rigidbody>();
		Destroy(rb);
		this.gameState.prefabInt = index;

	}

	public override void HandleMessage (Message message)
	{
		if (message.Type == MessageType.Menu) {
			switch (message.IntValue) {
			case -1:
				Vector3 v = transform.position;
				v.z = -150000;
				transform.position = v;
				break;
			case 1:
				Vector3 playerForward = player.transform.TransformDirection(Vector3.forward);
				transform.position = player.transform.position + playerForward + playerForward;
				transform.rotation = player.transform.rotation;
				break;
			}
		}

		if (message.Type == MessageType.ClickVRButton) {
			Debug.Log ("VR BUTTON Event from " + message.GameObjectValue.name);

			switch (message.GameObjectValue.name) {
			case "0":
				setSpawn(0);
				break;
			
			case "1":
				setSpawn(1);
				break;
			
			case "2":
				setSpawn (2);
				break;
			
			case "3":
				setSpawn (3);
				break;
			
			case "4":
				setSpawn (4);
				break;
			
			case "5":
				setSpawn (5);
				break;
			
			case "6":
				setSpawn (6);
				break;
			
			case "7":
				setSpawn (7);
				break;

			case "CloseMenu": // hide menu
				Vector3 v = transform.position;
				v.z = -150000;
				transform.position = v;
				break;

			}
		}
			
		if (message.Type == MessageType.SelectPrefab) {
			int index = message.IntValue;
			setSpawn (index);
		}
	}
}