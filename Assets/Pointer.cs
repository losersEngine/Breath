using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pointer : MonoBehaviour {

	private string[] tags = {"Light", "Statue", "Other", "Podium"};
	private GameObject pointing;

	// Use this for initialization
	void Start () {
		pointing = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		if (tags.Contains (other.tag))
			pointing = other.gameObject;
	}

	public void OnTriggerExit(){
		pointing = null;
	}

	public GameObject getPointing(){
		return pointing;
	}
}
