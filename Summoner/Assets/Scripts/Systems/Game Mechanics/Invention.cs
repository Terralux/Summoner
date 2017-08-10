using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Invention {
	public Blueprint Material1;
	public Blueprint Material2;
	public Blueprint Material3;

	public BaseItem result;

	public Invention(Blueprint ingredient1, Blueprint ingredient2, Blueprint ingredient3, BaseItem result){
		Material1 = ingredient1;
		Material2 = ingredient2;
		Material3 = ingredient3;

		this.result = result;
	}
}