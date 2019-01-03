using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoor : MonoBehaviour {

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

		if (other.tag.Equals("Player") && anim.GetBool("doorUp"))
		{
			sfx.clip = Resources.Load<AudioClip> ("Music/SFX/door_open");
			sfx.Play ();
			anim.SetBool ("doorUp", false);
		}
	}

	public void openDoor(){
		sfx.clip = Resources.Load<AudioClip> ("Music/SFX/door_close");
		sfx.Play ();
		anim.SetBool ("doorUp", true);
	}
}
