using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRise : MonoBehaviour {

	private GameManager gM;
	private bool collided;

	// Use this for initialization
	void Start () {
		collided = false;
		gM = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (collided) {
			float trans = 0.013f * Time.deltaTime;
			this.transform.Translate (new Vector3(0.0f, trans, 0.0f));
		}
	}

	public void dead(){
		gM.changeScene("game_over");
	}

	public void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Player") && !collided) {
			collided = true;
            gM.playFinalWater();
			Invoke ("dead", 180.0f);
		}
	}
}
