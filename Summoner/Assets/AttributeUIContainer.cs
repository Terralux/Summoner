using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUIContainer : MonoBehaviour {

	public Text attValue;
	public Text attImage;
	private ElementalAffinity myEA;

	public void Init (ElementalAffinity ea) {
		myEA = ea;
		UpdateGraphics ();
	}

	public void UpdateGraphics () {
		attImage.text = myEA.targetElement.ToString ();
		attValue.text = myEA.value.ToString ();
	}
}