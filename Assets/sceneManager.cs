using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

	public int lvlIndex;
	public string[] objectsPlaces;
	public PodiumController[] podiumsLevel;

	public GameObject startPlayer;
	public GameObject player;

	private GameManager gM;
	private GameObject playerAct;

	// Use this for initialization
	void Start () {
		gM = GameObject.FindObjectOfType<GameManager> ();

	}
	
    public void initGame()
    {
        playerAct = Instantiate(Resources.Load<GameObject>("FPSControllerKevin/Prefabs/Player"));
        playerAct.transform.SetPositionAndRotation(startPlayer.transform.position, startPlayer.transform.rotation);
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
			//TODO:
			//gM.SaveLvl (lvlIndex);
		}
	}
}
