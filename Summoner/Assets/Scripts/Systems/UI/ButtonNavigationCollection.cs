using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigationCollection : MonoBehaviour {

	public Button[] buttonCollection;
	private int currentButtonIndex = 0;
	public bool isHorizontal = false;

	public void Move(bool isMovingUp){
		if (isMovingUp) {
			MoveUp ();
		} else {
			MoveDown ();
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
		GetComponent<BaseMenu> ().Hide ();
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
		UpdateButtonsEnabled ();
		if (isHorizontal) {
			InputHandler.LeftStickEvent().horizontalAnalogEvent.becameActive += Move;
			InputHandler.DPadEvent ().horizontalAnalogEvent.becameActive += Move;
		} else {
			InputHandler.LeftStickEvent().verticalAnalogEvent.becameActive += Move;
			InputHandler.DPadEvent().verticalAnalogEvent.becameActive += Move;
		}

		InputHandler.AEvent().becameActive += Activate;
	}

	void OnDisable(){
		if (isHorizontal) {
			InputHandler.LeftStickEvent().horizontalAnalogEvent.becameActive -= Move;
			InputHandler.DPadEvent ().horizontalAnalogEvent.becameActive -= Move;
		} else {
			InputHandler.LeftStickEvent().verticalAnalogEvent.becameActive -= Move;
			InputHandler.DPadEvent().verticalAnalogEvent.becameActive -= Move;
		}

		InputHandler.AEvent().becameActive -= Activate;
	}
}