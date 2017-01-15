using System;
using UnityEngine;

public class GameState
{
	public int prefabInt = 0;
	public Vector3 teleportPosition = new Vector3(0,0,0);
	public GameObject selectedObject;
	public int points = 0;

	private static GameState instance = new GameState();
	private GameState() { }

	public static GameState GetInstance
	{
		get
		{
			return instance;
		}
	}
}