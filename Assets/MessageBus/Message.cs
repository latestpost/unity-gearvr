using UnityEngine;

public enum MessageType
{
	NONE,
	AddPrefab,
	ClickVRButton,
	Menu,
	SelectPrefab,
	RemovePrefab,
	LoadObjects,
	SaveObjects,
	ClearObjects,
	Collision,
	Blowup
}

public struct Message
{
	public MessageType Type;
	public int IntValue;
	public float FloatValue;
	public float StringValue;
	public Vector3 Vector3Value;
	public GameObject GameObjectValue;
}