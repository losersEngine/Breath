using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern int Load();
    [DllImport("__Internal")]
    private static extern void Save(int level);
    [DllImport("__Internal")]
    private static extern bool mobileAndTabletCheck();

    public GameObject uiManager;
    private UIManager manager;
    private bool playing;

    private void Awake()
    {
        if (!FindObjectOfType<UIManager>())
        {
            DontDestroyOnLoad(this);
            manager = Instantiate(uiManager).GetComponent<UIManager>();
            DontDestroyOnLoad(manager);
        }
        else
        {
            Destroy(this);
        }

    }

    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
        playing = false;

        /*string device = mobileAndTabletCheck()?"mobile":"desktop";
        manager.setDevice(device);*/
       // manager = FindObjectOfType<UIManager>();
        manager.setDevice("desktop");

    }

    public bool isPlaying() {

        return playing;
    }

    private int getActualLevel()
    {
        char[] arrayNameLevel = manager.scene.ToCharArray();
        int numberLevel = (int)char.GetNumericValue(arrayNameLevel[arrayNameLevel.Length - 1]);

        return numberLevel;

    }

    public void changeUIScene(string scene)
    {

        manager = FindObjectOfType<UIManager>();
        manager.DestroyWithTag("prefab");

        manager.scene = scene;
        manager.InstantiateLanguage();
        
        if (manager.scene.Equals("settings"))
        {
            setSettingsValues();
            GameObject.Find("back").GetComponent<Button>().onClick.AddListener(() => {
                changeUIScene("main_menu");
            });
        }
        if (manager.scene.Equals("level_selector"))
        {
            loadLevel();
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        manager.scene = scene.name;

        
        if (manager.scene.Contains("Level"))
        {

            manager.setLevel(getActualLevel());
            manager.instanceText();
        }
        else
        {
            manager.InstantiateLanguage();
        }

    }

    //////////////////////////////////////////// MENU CONFIGURACION /////////////////////////////////////////////////////////////////////////////////

    private void setSettingsValues() {

        //antialiasing
        Slider antialiasing = GameObject.Find("sliderAntialiasing").GetComponent<Slider>();
        antialiasing.value = QualitySettings.antiAliasing == 0?0: Mathf.Log(QualitySettings.antiAliasing,2);

        //volume
        Slider volume = GameObject.Find("sliderVolume").GetComponent<Slider>();
        volume.value = AudioListener.volume;

    }

    public void SetAntialiasing(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();

        if(slider.value == 0)
        {

            QualitySettings.antiAliasing = 0;

        }
        else
        {

            QualitySettings.antiAliasing = (int)Mathf.Pow(2, slider.value);

        }
    }

    public void SetVolume(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();
        AudioListener.volume = slider.value;
    }

    public void SetLanguage(int value)
    {

        manager = FindObjectOfType<UIManager>();
        Image l = GameObject.Find("languageOp").GetComponent<Image>();

        if (value == 0) //es
        {
            manager.setLanguage("Spanish");
            manager.loadSprite(l, "UI/Images","español");
        }
        else //en
        {
            manager.setLanguage("English");
            manager.loadSprite(l, "UI/Images", "english");
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////


    /// ///////////////////////// IN GAME TEXT ////////////////////////////////////////

    public void nextText()
    {
        manager = FindObjectOfType<UIManager>();
        this.playing = manager.nextText();
    
        if(playing)
            FindObjectOfType<sceneManager>().initGame();
    }

    ////////////////////////////////////////////////////////////////////////////////////
   
    public void saveGame() {
        
        //Save();

    }

    //////////////////////////// PAUSE MENU ////////////////////////////////////////
   
    public void setPauseMenu() {

        manager = FindObjectOfType<UIManager>();
        FindObjectOfType<sceneManager>().setPauseMenu();
        manager.setPauseMenu();

    }

    public void goToSettings() {

        manager = FindObjectOfType<UIManager>();
        manager.goToSettings();
        setSettingsValues();

    }

    public void backToGame() {

        manager = FindObjectOfType<UIManager>();
        manager.goToGame();
        FindObjectOfType<sceneManager>().setPauseMenu();

    }

    ////////////////////////////////////////////////////////////////////////////////////

    public void loadLevel()
    {
        int level = Load();
        manager = FindObjectOfType<UIManager>();
        manager.loadLevels(level);
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void changeScene(string newScene)
    {

        manager = FindObjectOfType<UIManager>();
        manager.SceneChanged();
        SceneManager.LoadScene(newScene);

    }

    // Update is called once per frame
    void Update()
    {

        playing = GameObject.FindGameObjectWithTag("Player") != null;
        if (playing && Input.GetKeyDown(KeyCode.Escape))
        {
            setPauseMenu();
        }

    }

}
