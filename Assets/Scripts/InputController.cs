using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	Message eventMessage;
	int prefabInt = 0;
	int maxPrefab = 8;

	private int keyDelay = 10;
	private int timePassed = 0;
	private GameState gameState;

	// Use this for initialization
	void Start () {
		QualitySettings.antiAliasing = 2;
		gameState = GameState.GetInstance;
	}
	
	// Update is called once per frame
	void Update () {

		timePassed++;

		Vector3 playerForward = transform.root.gameObject.transform.TransformDirection(Vector3.forward);
		Vector3 playerLeft = transform.root.gameObject.transform.TransformDirection(Vector3.left);

		Vector3 cameraForward = transform.TransformDirection(Vector3.forward);

		if (Input.GetKeyDown ("space")) {
			/*
			Vector3 v = transform.position;
			Vector3 cF = transform.TransformDirection(Vector3.forward);
			GameObject createObject = Instantiate (Resources.Load ("Weapons/Sphere", typeof(GameObject))) as GameObject;
			createObject.transform.rotation = transform.rotation;
			createObject.transform.position = v;
			createObject.GetComponent<Rigidbody>().AddForce(cF * 1000);
			*/

			eventMessage = new Message ();
			eventMessage.Type = MessageType.AddPrefab;
			eventMessage.IntValue = prefabInt;
			eventMessage.GameObjectValue = gameObject;
			MessageBus.Instance.SendMessage (eventMessage);
		}
			

		if (Input.GetKeyDown ("o")) {
			if (gameState.prefabInt < maxPrefab) {
				gameState.prefabInt++;
			}
			eventMessage = new Message ();
			eventMessage.Type = MessageType.SelectPrefab;
			eventMessage.IntValue = gameState.prefabInt;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("9")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.SaveObjects;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("1")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.LoadObjects;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("2")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.ClearObjects;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("l")) {
			gameState.prefabInt--;
			if (gameState.prefabInt < 0) {
				gameState.prefabInt = 0;
			}
			eventMessage = new Message ();
			eventMessage.Type = MessageType.SelectPrefab;
			eventMessage.IntValue = prefabInt;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("p")) {
			
			eventMessage = new Message ();
			eventMessage.Type = MessageType.Blowup;
			eventMessage.IntValue = prefabInt;
			eventMessage.GameObjectValue = gameObject;
			MessageBus.Instance.SendMessage (eventMessage);

		}

		if (Input.GetKeyDown ("x")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.RemovePrefab;
			eventMessage.IntValue = prefabInt;
			eventMessage.GameObjectValue = gameObject;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("m")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.Menu;
			eventMessage.IntValue = 1;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKeyDown ("n")) {
			eventMessage = new Message ();
			eventMessage.Type = MessageType.Menu;
			eventMessage.IntValue = -1;
			MessageBus.Instance.SendMessage (eventMessage);
		}

		if (Input.GetKey("w")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position += playerForward;
				timePassed = 0;
			}
		}


		if (Input.GetKey ("a")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position += playerLeft;
				timePassed = 0;
			}
		}
		if (Input.GetKey ("s")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position -= playerLeft;
				timePassed = 0;
			}
		}
		if (Input.GetKey ("z")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position -= playerForward;
				timePassed = 0;
			}
		}
		if (Input.GetKey ("u")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position = new Vector3 (transform.root.gameObject.transform.position.x, transform.root.gameObject.transform.position.y + 1f, transform.root.gameObject.transform.position.z);
				timePassed = 0;
			}
		}
		if (Input.GetKey ("j")) {
			if (timePassed > keyDelay) {
				transform.root.gameObject.transform.position = new Vector3 (transform.root.gameObject.transform.position.x, transform.root.gameObject.transform.position.y - 1f, transform.root.gameObject.transform.position.z);
				timePassed = 0;
			}
		}

		if (Input.GetKeyDown ("r")) {
			transform.root.gameObject.transform.Rotate (0, -90, 0);
		}
		if (Input.GetKeyDown ("t")) {
			transform.root.gameObject.transform.Rotate (0, 90, 0);
		}
	}
}
