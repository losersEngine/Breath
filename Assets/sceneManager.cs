using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

	public string[] objectsPlaces;
	public PodiumController[] podiumsLevel;

	public GameObject startPlayer;
	public GameObject finalExit;

	private GameManager gM;
	private MovementController playerAct;

	private bool endGame;

	private AudioSource sfxSource;

	// Use this for initialization
	void Start () {
		gM = GameObject.FindObjectOfType<GameManager> ();
		endGame = false;
		sfxSource = GetComponent<AudioSource> ();
		//initGame ();
	}

    public void initGame()
    {
		GameObject aux = Instantiate(Resources.Load<GameObject>("FPSControllerKevin/Prefabs/Player"));
        aux.transform.SetPositionAndRotation(startPlayer.transform.position, startPlayer.transform.rotation);

		playerAct = aux.GetComponent<MovementController> ();
    }

	// Update is called once per frame
	void Update () {

		if (endGame) {
			Transform transformPlayer = playerAct.transform;
			Transform transformCamera = playerAct.vista.transform;

			Vector3 dirBody = finalExit.transform.position - transformPlayer.position;
			dirBody.y = 0;

			Vector3 dirCamera = finalExit.transform.position - transformCamera.position;
			//dirCamera.x = 0;

			Quaternion rotBody = Quaternion.LookRotation (dirBody);
			Quaternion rotCamera = Quaternion.LookRotation (dirCamera);

			transformPlayer.rotation = Quaternion.Slerp (transformPlayer.rotation, rotBody, 3.5f * Time.deltaTime);
			transformCamera.rotation = Quaternion.Slerp (transformCamera.rotation, rotCamera, 3.5f * Time.deltaTime);
		} 
		
	}

    public void setPauseMenu() {

        playerAct.setFixedAnim(!playerAct.getFixedAnim());

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

			playerAct.setFixedAnim(true);
			endGame = true;

			Invoke ("stopAnimation", 5.5f);
			//TODO:
			//gM.saveGame ();
			GameObject.FindGameObjectWithTag("AnimatedDoor").GetComponent<Animator>().SetTrigger("victory");
			sfxSource.Play();
		}
	}

	private void stopAnimation(){
		endGame = false;
		playerAct.setFixedAnim(false);
	}
}
