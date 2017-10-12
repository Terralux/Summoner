using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	
	#region Bumpers
	public static InputEventDigital LB;
	public static InputEventDigital RB;
	#endregion

	#region ActionButtons
	public static InputEventDigital X;
	public static InputEventDigital Y;
	public static InputEventDigital A;
	public static InputEventDigital B;
	#endregion

	#region CenterButtons
	public static InputEventDigital Start;
	public static InputEventDigital Back;
	#endregion

	#region Triggers
	public static InputEventAnalog LeftTrigger;
	public static InputEventAnalog RightTrigger;
	#endregion

	#region LeftStick
	public static InputEventVector2Analog LeftStick;
	#endregion

	#region DPad
	public static InputEventVector2Analog DPad;
	#endregion

	void Awake(){
		LB = new InputEventDigital ("LB");
		RB = new InputEventDigital ("RB");

		X = new InputEventDigital ("X");
		Y = new InputEventDigital ("Y");
		A = new InputEventDigital ("A");
		B = new InputEventDigital ("B");

		Start = new InputEventDigital ("Start");
		Back = new InputEventDigital ("Back");

		LeftTrigger = new InputEventAnalog ("LT");
		RightTrigger = new InputEventAnalog ("RT");

		LeftStick = new InputEventVector2Analog ("Horizontal", "Vertical");

		DPad = new InputEventVector2Analog ("DPadHorizontal", "DPadVertical");
	}

	void Update (){
		LeftTrigger.Update ();
		RightTrigger.Update ();

		LB.Update ();
		RB.Update ();

		X.Update ();
		Y.Update ();
		A.Update ();
		B.Update ();

		Start.Update ();
		Back.Update ();

		LeftStick.Update ();
		DPad.Update ();
	}
}

public class InputEventDigital{
	public delegate void VoidEvent(bool value);
	public delegate void SingleFireEvent();

	public VoidEvent changedState;
	public VoidEvent getValue;
	public SingleFireEvent becameActive;
	public SingleFireEvent becameInactive;

	private string buttonName;
	private bool previousState;

	public InputEventDigital(string buttonName){
		this.buttonName = buttonName;
	}

	public void Update(){
		bool activeState = Input.GetButtonDown (buttonName);

		if (getValue != null) {
			getValue (activeState);
		}

		if (previousState != activeState) {
			if (activeState) {
				if (becameActive != null) {
					becameActive ();
				}
			} else {
				if (becameInactive != null) {
					becameInactive ();
				}
			}

			if (changedState != null) {
				changedState (activeState);
			}
		}

		previousState = activeState;
	}

}

public class InputEventAnalog{
	public delegate void FloatEvent(float value);
	public delegate void SingleFireEvent();
	public delegate void BoolEvent(bool isPositive);

	public FloatEvent getValue;
	public BoolEvent becameActive;
	public SingleFireEvent becameInactive;

	private string axisName;
	private float maxThreshold;
	private float minThreshold;
	private bool isCurrentlyActive;

	private float delayBetweenEnables = 0.3f;
	private float currentTime = 0f;

	public InputEventAnalog(string axisName){
		this.axisName = axisName;
	}

	public void Update(){
		float value = Input.GetAxis (axisName);

		if (getValue != null) {
			getValue (value);
		}

		if (isCurrentlyActive) {
			if (Mathf.Abs (value) < minThreshold) {
				if (becameInactive != null) {
					becameInactive ();
				}
				isCurrentlyActive = false;
			} else {
				currentTime += Time.deltaTime;
				if (currentTime >= delayBetweenEnables) {
					if (becameActive != null) {
						becameActive (value > 0f);
					}
					currentTime = 0f;
				}
			}
		} else {
			if (Mathf.Abs(value) > maxThreshold) {
				if (becameActive != null) {
					becameActive (value > 0f);
				}
				isCurrentlyActive = true;
				currentTime = 0f;
			}
		}
	}
}

public class InputEventVector2Analog{
	public delegate void FloatEvent(float value);
	public delegate void Vector2Event(Vector2 value);

	public Vector2Event getValue;

	public FloatEvent getHorizontalValue;
	public FloatEvent getVerticalValue;

	private string horizontalAxisName;
	private string verticalAxisName;

	private Vector2 input;

	private float horizontalValue;
	private float verticalValue;

	public InputEventAnalog horizontalAnalogEvent;
	public InputEventAnalog verticalAnalogEvent;

	public InputEventVector2Analog(string horizontalAxisName, string VerticalAxisName){
		this.horizontalAxisName = horizontalAxisName;
		this.verticalAxisName = VerticalAxisName;

		horizontalAnalogEvent = new InputEventAnalog (horizontalAxisName);
		verticalAnalogEvent = new InputEventAnalog (verticalAxisName);
	}

	public void Update(){
		horizontalValue = Input.GetAxis (horizontalAxisName);
		verticalValue = Input.GetAxis (verticalAxisName);

		input = new Vector2 (horizontalValue, verticalValue);

		if (getValue != null) {
			getValue (input);
		}
		if (getHorizontalValue != null) {
			getHorizontalValue (horizontalValue);
		}
		if (getVerticalValue != null) {
			getVerticalValue (verticalValue);
		}

		horizontalAnalogEvent.Update ();
		verticalAnalogEvent.Update ();
	}
}