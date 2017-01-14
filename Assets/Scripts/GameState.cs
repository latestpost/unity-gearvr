using System;

public class GameState
{
	public int prefabInt = 0;
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