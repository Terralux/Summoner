using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigationCollection : MonoBehaviour {

	public Button[] buttonCollection;
	private int currentButtonIndex = 0;
	public bool isHorizontal = false;

	public void Move(bool isMovingDown){
		if (isMovingDown) {
			MoveDown ();
		} else {
			MoveUp ();
		}
	}

	public void MoveUp(){
		currentButtonIndex--;
		if (currentButtonIndex < 0) {
			currentButtonIndex = buttonCollection.Length - 1;
		}
		UpdateButtonsEnabled ();
	}

	public void MoveDown(){
		currentButtonIndex++;
		if (currentButtonIndex >= buttonCollection.Length) {
			currentButtonIndex = 0;
		}
		UpdateButtonsEnabled ();
	}

	public void Activate(){
		buttonCollection [currentButtonIndex].onClick.Invoke ();
	}

	private void UpdateButtonsEnabled(){
		for (int i = 0; i < buttonCollection.Length; i++) {
			if (i == currentButtonIndex) {
				buttonCollection [i].interactable = true;
			} else {
				buttonCollection [i].interactable = false;
			}
		}
	}

	void OnEnable(){
		Debug.Log (InputHandler.VerticalLeftStick);
		Debug.Log (InputHandler.VerticalLeftStick.becameActive);
		UpdateButtonsEnabled ();
		if (isHorizontal) {
			InputHandler.HorizontalLeftStick.becameActive += Move;
		} else {
			InputHandler.VerticalLeftStick.becameActive += Move;
		}
	}

	void OnDisable(){
		if (isHorizontal) {
			InputHandler.HorizontalLeftStick.becameActive -= Move;
		} else {
			InputHandler.VerticalLeftStick.becameActive -= Move;
		}
	}
}