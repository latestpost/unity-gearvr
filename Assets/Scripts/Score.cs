using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
	private GameState gameState;

	// Use this for initialization
	void Start () {
		gameState = GameState.GetInstance;
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textMesh = GetComponent<TextMesh>();
		textMesh.text = ""+gameState.points;
	}
}
