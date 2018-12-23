using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

	public string[] objectsPlaces;
	public PodiumController[] podiumsLevel;

	public GameObject startPlayer;
	public GameObject player;

	// Use this for initialization
	void Start () {
		GameObject aux = Instantiate (player);
		aux.transform.SetPositionAndRotation (startPlayer.transform.position, startPlayer.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlacedItem(){
		bool correct = true;
		int i = 0;

		while (correct && i < objectsPlaces.Length) {
			correct = podiumsLevel [i].getItemTag ().Equals (objectsPlaces[i]);
			i++;
		}

		if (correct) {
			Debug.Log ("Ganaste");
		}
	}
}
