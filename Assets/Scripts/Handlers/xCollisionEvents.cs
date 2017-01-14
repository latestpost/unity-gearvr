using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CollisionEvents : MessageHandler 

{
	private GameObject go;

	public CollisionEvents(GameObject go){
		this.go = go;
	}

	public override void HandleMessage (Message message)
	{
		if (message.Type == MessageType.Blowup) {
		if (go) {
			Vector3 cF = go.transform.TransformDirection (Vector3.up);
			go.GetComponent<Rigidbody> ().AddForce (cF * 1000);
					
			}
		}
	}
}