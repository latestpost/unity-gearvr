using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		//print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
		//print("Their relative velocity is " + collisionInfo.relativeVelocity);
		if (collisionInfo.collider.tag == "Cube") {
			Destroy (collisionInfo.collider.gameObject);
			Destroy (gameObject);
		}
	}
}
