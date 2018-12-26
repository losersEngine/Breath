using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /*[DllImport("__Internal")]
    private static extern int Load();
    [DllImport("__Internal")]
    private static extern void Save();*/
    public GameObject uiManager;
    private UIManager manager;

    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("UIManager"))
        {
            DontDestroyOnLoad(this);
            GameObject ins = Instantiate(uiManager);
            DontDestroyOnLoad(ins);
        }
        else
        {
            Destroy(this);
        }

        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        manager.scene = scene.name;
        if (!scene.name.Contains("Level"))
        {
            manager.InstantiateLanguage();
        }
    }

    // Use this for initialization
    void Start () {

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //////////////////////////////////////////// MENU CONFIGURACION /////////////////////////////////////////////////////////////////////////////////

    public void SetAntialiasing(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, slider.value);
    }

    public void SetVolume(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();
        AudioListener.volume = slider.value;
    }

    public void SetLanguage(int value)
    {

        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        Image l = GameObject.Find("languageOp").GetComponent<Image>();

        if (value == 0) //es
        {
            manager.setLanguage("Spanish");
            l.sprite = Resources.Load<Sprite>("UI/Images/es/español");
        }
        else //en
        {
            manager.setLanguage("English");
            l.sprite = Resources.Load<Sprite>("UI/Images/en/english");
        }

    }


    ////////////////////////////////////////////////////////////////////////////////////
    public void exitGame()
    {
        Application.Quit();
    }

    public void changeScene(string newScene)
    {

        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        manager.SceneChanged();
        SceneManager.LoadScene(newScene);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
