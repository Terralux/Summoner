using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ConvertCornerText : MonoBehaviour {

	private string text = "CreateCornerMesh (points [4], points [5], points [6], points [2], false);";
	private string collectiveString = "";

	public InputField input;

	public void OnClick () {
		collectiveString = "";
		text = input.text.Remove(0, text.IndexOf('(') + 1);
		string[] textArray = text.Split(',');
		int[] numbers = new int[4];
		for(int i = 0; i < textArray.Length - 1; i++){
			numbers[i] = int.Parse(textArray[i].Substring(textArray[i].IndexOf('[') + 1, textArray[i].IndexOf(']') - (textArray[i].IndexOf('[') + 1)));
		}

		collectiveString += "CreateTriangle ( points[" + numbers[0] + "], points[" + numbers[1] + "], points[" + numbers[2] + "]);";
		collectiveString += "\nCreateTriangle ( points[" + numbers[0] + "], points[" + numbers[2] + "], points[" + numbers[3] + "]);";
		collectiveString += "\nCreateTriangle ( points[" + numbers[0] + "], points[" + numbers[3] + "], points[" + numbers[1] + "]);";

		if(textArray[textArray.Length - 1].Contains("true")){
			collectiveString += "\n\nCreateTriangle ( points[" + numbers[3] + "], points[" + numbers[2] + "], points[" + numbers[1] + "]);";
		}

		Debug.Log(collectiveString);

		input.text = "";
	}
}