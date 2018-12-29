using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumController : MonoBehaviour {

	public GameObject target;
	public GameObject placed;

	private sceneManager scene;

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		scene = GameObject.FindObjectOfType<sceneManager> ();
		audio = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {}

	public void PlaceItem(GameObject obj){
		placed = obj;
		placed.transform.SetPositionAndRotation (target.transform.position, target.transform.rotation);
		audio.Play ();

		scene.PlacedItem ();

    }

    private void OnTriggerEnter(Collider other)
    {

        UIManager manager = FindObjectOfType<UIManager>();
        if (other.tag.Equals("Player") && placed != null)
        {
            manager.setMessageInteract();

        }
    }

    private void OnTriggerExit(Collider other)
    {

        UIManager manager = FindObjectOfType<UIManager>();
        if (other.tag.Equals("Player"))
        {
            manager.destroyMessageInteract();
        }
    }
    public GameObject TakeItem(){
		GameObject toReturn = placed;
		placed = null;

		audio.Play ();

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
