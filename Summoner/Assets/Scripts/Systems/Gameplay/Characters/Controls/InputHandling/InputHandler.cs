using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	#region Bumpers
	private static InputEventDigital LB;
	private static InputEventDigital RB;

	public static InputEventDigital LBEvent(){
		if (LB == null) {
			LB = new InputEventDigital ("LB");
		}
		return LB;
	}

	public static InputEventDigital RBEvent(){
		if (RB == null) {
			RB = new InputEventDigital ("RB");
		}
		return RB;
	}
	#endregion

	#region ActionButtons
	private static InputEventDigital X;
	private static InputEventDigital Y;
	private static InputEventDigital A;
	private static InputEventDigital B;

	public static InputEventDigital XEvent(){
		if (X == null) {
			X = new InputEventDigital ("X");
		}
		return X;
	}
	public static InputEventDigital YEvent(){
		if (Y == null) {
			Y = new InputEventDigital ("Y");
		}
		return Y;
	}
	public static InputEventDigital AEvent(){
		if (A == null) {
			A = new InputEventDigital ("A");
		}
		return A;
	}
	public static InputEventDigital BEvent(){
		if (B == null) {
			B = new InputEventDigital ("B");
		}
		return B;
	}
	#endregion

	#region CenterButtons
	private static InputEventDigital Start;
	private static InputEventDigital Back;

	public static InputEventDigital StartEvent(){
		if (Start == null) {
			Start = new InputEventDigital ("Start");
		}
		return Start;
	}
	public static InputEventDigital BackEvent(){
		if (Back == null) {
			Back = new InputEventDigital ("Back");
		}
		return Back;
	}
	#endregion

	#region Triggers
	private static InputEventAnalog LeftTrigger;
	private static InputEventAnalog RightTrigger;

	public static InputEventAnalog LeftTriggerEvent(){
		if (LeftTrigger == null) {
			LeftTrigger = new InputEventAnalog ("LT", 0.2f, 0.1f);
		}
		return LeftTrigger;
	}
	public static InputEventAnalog RightTriggerEvent(){
		if (RightTrigger == null) {
			RightTrigger = new InputEventAnalog ("RT", 0.2f, 0.1f);
		}
		return RightTrigger;
	}
	#endregion

	#region LeftStick
	private static InputEventVector2Analog LeftStick;

	public static InputEventVector2Analog LeftStickEvent(){
		if (LeftStick == null) {
			LeftStick = new InputEventVector2Analog ("Horizontal", "Vertical");
		}
		return LeftStick;
	}
	#endregion

	#region DPad
	private static InputEventVector2Analog DPad;

	public static InputEventVector2Analog DPadEvent(){
		if (DPad == null) {
			DPad = new InputEventVector2Analog ("DPadHorizontal", "DPadVertical");
		}
		return DPad;
	}
	#endregion

	void Awake(){
		if (LB == null) {
			LB = new InputEventDigital ("LB");
		}
		if (RB == null) {
			RB = new InputEventDigital ("RB");
		}
		if (X == null) {
			X = new InputEventDigital ("X");
		}
		if (Y == null) {
			Y = new InputEventDigital ("Y");
		}
		if (A == null) {
			A = new InputEventDigital ("A");
		}
		if (B == null) {
			B = new InputEventDigital ("B");
		}

		if (Start == null) {
			Start = new InputEventDigital ("Start");
		}
		if (Back == null) {
			Back = new InputEventDigital ("Back");
		}

		if (LeftTrigger == null) {
			LeftTrigger = new InputEventAnalog ("LT", 0.2f, 0.1f);
		}
		if (RightTrigger == null) {
			RightTrigger = new InputEventAnalog ("RT", 0.2f, 0.1f);
		}

		if (LeftStick == null) {
			LeftStick = new InputEventVector2Analog ("Horizontal", "Vertical");
		}

		if (DPad == null) {
			DPad = new InputEventVector2Analog ("DPadHorizontal", "DPadVertical");
		}
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
	public SingleFireEvent becameActive;
	public SingleFireEvent becameInactive;

	private string axisName;
	private float maxThreshold;
	private float minThreshold;
	private bool isCurrentlyActive;

	private float delayBetweenEnables = 0.3f;
	private float currentTime = 0f;

	public InputEventAnalog(string axisName, float valueForActivation, float valueForDeactivation){
		this.axisName = axisName;
		maxThreshold = valueForActivation;
		minThreshold = valueForDeactivation;
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
						becameActive ();
					}
					currentTime = 0f;
				}
			}
		} else {
			if (Mathf.Abs(value) > maxThreshold) {
				if (becameActive != null) {
					becameActive ();
				}
				isCurrentlyActive = true;
				currentTime = 0f;
			}
		}
	}
}

public class InputEventAnalogAxis{
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

	public InputEventAnalogAxis(string axisName, float valueForActivation, float valueForDeactivation){
		this.axisName = axisName;
		maxThreshold = valueForActivation;
		minThreshold = valueForDeactivation;
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

	public Vector2 input;

	private float horizontalValue;
	private float verticalValue;

	public InputEventAnalogAxis horizontalAnalogEvent;
	public InputEventAnalogAxis verticalAnalogEvent;

	public InputEventVector2Analog(string horizontalAxisName, string verticalAxisName){
		this.horizontalAxisName = horizontalAxisName;
		this.verticalAxisName = verticalAxisName;

		horizontalAnalogEvent = new InputEventAnalogAxis (horizontalAxisName, 0.2f, 0.1f);
		verticalAnalogEvent = new InputEventAnalogAxis (verticalAxisName, 0.2f, 0.1f);
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