using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initDoor : MonoBehaviour {

	private Animator anim;
	private AudioSource sfx;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		sfx = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag.Equals("Player"))
		{
			sfx.Play ();
			anim.SetTrigger ("close");
		}
	}
}
