using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuable : MonoBehaviour {

    private bool isPicked;
	// Use this for initialization
	void Start () {
        isPicked = false;
	}


    public bool isItemPicked()
    {
        return isPicked;
    }

    public void setItemPicked(bool b)
    {
        isPicked = b;
    }

    private void OnTriggerStay(Collider other)
    {

        UIManager manager = FindObjectOfType<UIManager>();
        if (other.tag.Equals("Player") && !isPicked)
        {

        }

    }

    private void OnTriggerExit(Collider other)
    {
        UIManager manager = FindObjectOfType<UIManager>();
        if (other.tag.Equals("Player"))
        {

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
