using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	public VirtualJoystick leftJoystick;
	public VirtualJoystick rightJoystick;
	public JoyButton joyButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * 0.5f * leftJoystick.Vertical ());
		this.transform.Translate (Vector3.right * 0.5f * leftJoystick.Horizontal ());

		this.transform.Rotate (Vector3.left * 2f * rightJoystick.Vertical ());
		this.transform.Rotate (Vector3.up * 2f * rightJoystick.Horizontal (), Space.World);
	}
}
