using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour {

	private GameState gameState;

	// Use this for initialization
	void Start () {
		gameState = GameState.GetInstance;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		//print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
		//print("Their relative velocity is " + collisionInfo.relativeVelocity);
		if (collisionInfo.collider.name == "Ground") {
			Destroy (gameObject);
			gameState.points = gameState.points + 100;
		}
	}
}
