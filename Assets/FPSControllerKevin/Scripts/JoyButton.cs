using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class JoyButton : MonoBehaviour, IPointerDownHandler {

	public Pointer pointer;
	public GameObject target;

	private string[] pickable = {"Light", "Statue"};
	private string[] dropZone = {"Podium"};
	private GameObject carry;

	// Use this for initialization
	void Start () {
		carry = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (carry != null)
			carry.transform.SetPositionAndRotation(target.transform.position, target.transform.localRotation);
	}

	public virtual void OnPointerDown(PointerEventData ped){
		if (carry == null) {
			pickup ();
		} else {
			drop();
		}
	}

	private void pickup (){
		GameObject pointed = pointer.getPointing ();

		if (pickable.Contains (pointed.tag))
			carry = pointed;
	}

	private void drop(){
		GameObject pointed = pointer.getPointing ();

		if (dropZone.Contains (pointed.tag)) {
			GameObject aux = carry;
			carry = null;
			//TODO: Colocar objeto en el pedestal
		}
	}
}
