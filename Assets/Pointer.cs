using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	private string tags = "LightPlaceHolderRight";
	private PodiumController pointing;

	private int collidings;

	// Use this for initialization
	void Start () {
		pointing = null;
		collidings = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		if (tags.Equals (other.tag)) {
			collidings++;
			pointing = other.gameObject.GetComponent<PodiumController> ();
		}
	}

	public void OnTriggerExit(Collider other){
		if (tags.Equals (other.tag)) {
			collidings--;

			if (collidings == 0)
				pointing = null;
		}
	}

	public PodiumController getPointing(){
		return pointing;
	}
}
