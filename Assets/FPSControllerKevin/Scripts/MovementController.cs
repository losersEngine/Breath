using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	public VirtualJoystick leftJoystick;
	public VirtualJoystick rightJoystick;

	public Camera vista;

	public Rigidbody rigid;
	public Animator anim;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		float timer = Time.deltaTime * 4.5f;

		Vector3 velocity = Vector3.zero;
		//MOVEMENT
		float value = (leftJoystick.Vertical () != 0) ? leftJoystick.Vertical () : Input.GetAxis ("Vertical");
		value = value * timer;
		velocity += (Vector3.forward * value);

		value = (leftJoystick.Horizontal () != 0) ? leftJoystick.Horizontal () : Input.GetAxis ("Horizontal");
		value = value * timer;
		velocity += (Vector3.right * value);

		anim.SetFloat ("vel", velocity.magnitude);

		velocity += Physics.gravity * Time.deltaTime / 6;
		velocity = this.transform.TransformVector (velocity) * 200;
		rigid.velocity = velocity;
		//END OF MOVEMENT
		//CAMERA
		value = (rightJoystick.Horizontal () != 0) ? rightJoystick.Horizontal () : Input.GetAxis("Mouse X");
		value = value * timer * 25;
		this.transform.Rotate (Vector3.up * value, Space.World);

		value = (rightJoystick.Vertical () != 0) ? rightJoystick.Vertical () : Input.GetAxis("Mouse Y");
		value = value * timer * 25;
		vista.transform.Rotate (Vector3.left * value);

		Vector3 limit = vista.transform.localEulerAngles;
		float rot = vista.transform.rotation.x;

		if (rot > 0 && limit.x > 55) {
			vista.transform.localEulerAngles = new Vector3(55, limit.y, limit.z);
		}

		if (rot < 0 && limit.x < 305) {
			vista.transform.localEulerAngles = new Vector3(-55, limit.y, limit.z);
		}
		//END OF CAMERA
	}
}
