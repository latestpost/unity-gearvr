using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Cube {

	public string Name;
	public Vector3 Position;
	public Quaternion Rotation;

}
	

[XmlRoot("CubeCollection")]
public class CubeContainer
{
	[XmlArray("Cubes")]
	[XmlArrayItem("Cube")]
	public List<Cube> Cubes = new List<Cube>();
}