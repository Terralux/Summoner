using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Invention : ScriptableObject{
	public Blueprint Material1;
	public Blueprint Material2;
	public Blueprint Material3;

	public BaseItem result;
}
