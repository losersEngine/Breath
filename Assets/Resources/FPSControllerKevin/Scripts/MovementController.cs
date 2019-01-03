using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	public VirtualJoystick leftJoystick;
	public VirtualJoystick rightJoystick;

	public Camera vista;

	public Rigidbody rigid;
	public Animator anim;

	private bool fixedAnim;

	private float rotBody;
	private float rotCamera;

	private bool mobile;

	// Use this for initialization
	void Start () {

		//mobile = FindObjectOfType<UIManager> ().getDevice ().Equals ("mobile");

		rigid = this.GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator> ();

		fixedAnim = false;

		rotBody = this.transform.localRotation.eulerAngles.y;
		rotCamera = vista.transform.localRotation.eulerAngles.x;
	}

	// Update is called once per frame
	void Update () {

		if (!fixedAnim) {
			movement ();
		}

	}

	public void setFixedAnim(bool fix){
		fixedAnim = fix;
		centerCamera ();
	}

	public bool getFixedAnim(){
		return fixedAnim;
	}

	public void centerCamera (){
		rotBody = this.transform.localEulerAngles.y;

		float auxCam = vista.transform.localEulerAngles.x;
		if (auxCam > 55) {
			auxCam -= 360;
		}
		rotCamera = auxCam;
	}

	private void movement(){
		float timer = Time.deltaTime * 4.5f;

		Vector3 velocity = Vector3.zero;
		//MOVEMENT
		float value = (mobile) ? leftJoystick.Vertical () : Input.GetAxis ("Vertical");
		value = value * timer;
		velocity += (Vector3.forward * value);

		value = (mobile) ? leftJoystick.Horizontal () : Input.GetAxis ("Horizontal");
		value = value * timer;
		velocity += (Vector3.right * value);

		anim.SetFloat ("vel", velocity.magnitude);

		velocity += Physics.gravity * Time.deltaTime / 5;
		velocity = this.transform.TransformVector (velocity) * 200;
		rigid.velocity = velocity;
		//END OF MOVEMENT
		//CAMERA

		rotBody += ((mobile) ? rightJoystick.Horizontal () : Input.GetAxis("Mouse X")) * 1.5f;
		Quaternion localRotation = Quaternion.Euler (0, rotBody, 0);
		this.transform.localRotation = localRotation;

		rotCamera += ((mobile) ? -rightJoystick.Vertical () : -Input.GetAxis("Mouse Y")) *1.5f;
		rotCamera = Mathf.Clamp (rotCamera, -55, 55);
		localRotation = Quaternion.Euler (rotCamera, 0, 0);
		vista.transform.localRotation = localRotation;

		//END OF CAMERA

	}
}
