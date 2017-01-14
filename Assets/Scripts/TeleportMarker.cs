using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMarker : MonoBehaviour {
	private GameState gameState;

	// Use this for initialization
	void Start () {
		gameState = GameState.GetInstance;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = gameState.teleportPosition;
	}
}
