using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject uiManager;
    private UIManager manager;
    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("UIManager"))
        {
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GameManager"));
        }

    }

    // Use this for initialization
    void Start () {

        if (!GameObject.FindGameObjectWithTag("UIManager"))
        {
            GameObject ins = Instantiate(uiManager);
            DontDestroyOnLoad(ins);
        }

        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

    }


    public void changeSettings()
    {

    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void changeScene(string newScene)
    {

        SceneManager.LoadScene(newScene);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
