using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerDownHandler {

	public Pointer pointer;
	public GameObject target;

	private GameObject carry;

	// Use this for initialization
	void Start () {
		carry = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (carry != null)
			carry.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);

		if (Input.GetKeyDown (KeyCode.E))
			controlClick();
	}

	public virtual void OnPointerDown(PointerEventData ped){
		controlClick ();
	}

	private void controlClick(){
		if (carry == null) {
			pickup ();
		} else {
			drop();
		}
	}

	private void pickup (){
		PodiumController pointed = pointer.getPointing ();

		if (pointed != null)
			carry = pointed.TakeItem();
	}

	private void drop(){
		PodiumController pointed = pointer.getPointing ();

		if (pointed != null) {
			GameObject aux = carry;
			carry = null;
			pointed.PlaceItem (aux);
		}
	}
}
