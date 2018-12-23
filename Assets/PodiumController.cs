using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumController : MonoBehaviour {

	public GameObject target;
	public GameObject placed;

	private sceneManager scene;

	// Use this for initialization
	void Start () {
		scene = GameObject.FindObjectOfType<sceneManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaceItem(GameObject obj){
		placed = obj;
		placed.transform.SetPositionAndRotation (target.transform.position, target.transform.rotation);

		scene.PlacedItem ();
	}

	public GameObject TakeItem(){
		GameObject toReturn = placed;
		placed = null;

		return toReturn;
	}

	public string getItemTag(){
		if (placed != null) {
			return placed.tag;
		} else {
			return "none";
		}
	}
}
