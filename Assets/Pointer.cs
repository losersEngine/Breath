using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	private string tags = "LightPlaceHolderRight";
	private PodiumController pointing;

	// Use this for initialization
	void Start () {
		pointing = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		if (tags.Equals(other.tag))
			pointing = other.gameObject.GetComponent<PodiumController>();
	}

	public void OnTriggerExit(){
		pointing = null;
	}

	public PodiumController getPointing(){
		return pointing;
	}
}
