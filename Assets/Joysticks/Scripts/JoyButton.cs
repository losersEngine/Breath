using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	[HideInInspector]
	protected bool Pressed;

	// Use this for initialization
	void Start () {
		Pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void OnPointerDown(PointerEventData ped){
		Pressed = true;	
	}

	public virtual void OnPointerUp(PointerEventData ped){
		Pressed = false;	
	}


}
