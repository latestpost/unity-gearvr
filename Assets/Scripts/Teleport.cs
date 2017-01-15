using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	private GameState gameState;

	// Use this for initialization
	void Start () {
		gameState = GameState.GetInstance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() 
	{
		Vector3 fwd = transform.TransformDirection(Vector3.back);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, fwd, out hit, 100.0F)){

				Vector3 v = hit.point;
				float rounded;
				rounded = Mathf.Round (v.x);
				v.x = rounded;
				rounded = Mathf.Round (v.y);
				v.y = rounded;
				v.y = v.y + 0.1f;
				rounded = Mathf.Round (v.z);
				v.z = rounded;


				gameState.teleportPosition = v;
			if (hit.transform.name != "Ground") {
				gameState.selectedObject = hit.transform.gameObject;
			}
		}
	}
}
