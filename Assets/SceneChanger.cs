using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public string nextScene;

	private GameManager gM;
    private UIManager uiM;

	// Use this for initialization
	void Start () {
		gM = FindObjectOfType<GameManager> ();
        uiM = FindObjectOfType<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
        {
            if (uiM.scene.Equals("Level4"))
            {
                uiM.scene = "Leevl5";
                uiM.instanceText();
            }
            else
            {
                gM.changeScene(nextScene);

            }

        }
    }
}
