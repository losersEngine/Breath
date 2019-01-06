using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern bool mobileAndTabletCheck();

    public GameObject uiManager;
	private static UIManager manager;
    private bool playing;
    private VideoPlayer videoGO;
    private static AudioSource click;
    private static AudioSource gameOverMusic;

    private static bool mobile;

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
        click = GetComponent<AudioSource>();
        click.clip =  Resources.Load<AudioClip>("Music/SFX/click_button");
        SceneManager.sceneLoaded += OnSceneLoaded;
        playing = false;

        //manager = FindObjectOfType<UIManager>();

		//string device = "desktop";
        string device = mobileAndTabletCheck() ? "mobile" : "desktop";
        manager.setDevice(device);
        mobile = device.Equals("mobile");

    }

    public bool isPlaying()
    {

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

        //manager = FindObjectOfType<UIManager>();
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
        else if (manager.scene.Equals("level_selector"))
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
            unlockCursor();

            if (manager.scene.Equals("game_over"))
            {
                gameOverMusic = GameObject.FindGameObjectWithTag("video").GetComponent<AudioSource>();
                gameOverMusic.clip = Resources.Load<AudioClip>("Music/Music/GameOver_music");
                gameOverMusic.Play();
                videoGO = GameObject.FindGameObjectWithTag("video").GetComponent<VideoPlayer>();
                videoGO.url = System.IO.Path.Combine(Application.streamingAssetsPath, "game_over.mp4");
                videoGO.Play();
                videoGO.loopPointReached += setGO;

            }
            else
            {
                manager.InstantiateLanguage();
            }
        }

    }

    private void setGO(VideoPlayer vp)
    {

        manager.instantiateGO();
            
    }

    //////////////////////////////////////////// MENU CONFIGURACION /////////////////////////////////////////////////////////////////////////////////

    private void setSettingsValues()
    {

        //antialiasing
        Slider antialiasing = GameObject.Find("sliderAntialiasing").GetComponent<Slider>();
        antialiasing.value = QualitySettings.antiAliasing == 0 ? 0 : Mathf.Log(QualitySettings.antiAliasing, 2);

        //volume
        Slider volume = GameObject.Find("sliderVolume").GetComponent<Slider>();
        volume.value = AudioListener.volume;

    }

    public void SetAntialiasing(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();

        if (slider.value == 0)
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

    public void SetLanguage()
    {

        //manager = FindObjectOfType<UIManager>();
        Image l = GameObject.Find("languageOp").GetComponent<Image>();
        string actualLanguage = manager.getLanguage();

        if (actualLanguage.Equals("English")) // cambiar a es
        {
            manager.setLanguage("Spanish");
            manager.loadSprite(l, "UI/Images", "español");
        }
        else //en
        {
            manager.setLanguage("English");
            manager.loadSprite(l, "UI/Images", "english");
        }

    }

    /// ///////////////////////// IN GAME TEXT ////////////////////////////////////////

    public void nextText()
    {
        //manager = FindObjectOfType<UIManager>();
        this.playing = manager.nextText();

		if (manager.scene.Equals("Level5") && playing) {

            changeScene("main_menu");
        }
        else
        {

            if (playing){

                lockCursor();
                FindObjectOfType<sceneManager>().initGame();
                manager.setActiveJoysticks(false, "joysticks"); //si es ordenador se esconden, si es movil, se muestran

            }

        }
        
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void Click()
    {
        click.Play();
    }
   
	public int loadGame(){
		int aux = PlayerPrefs.GetInt ("LVL");
		int lvl = (aux != null) ? aux : -1;
		return lvl;
	}

	public void saveGame() {

        int lvl = getActualLevel() + 1;
		int lvlSaved = this.loadGame ();

		if (lvl > lvlSaved)
			PlayerPrefs.SetInt ("LVL", lvl);
    }

    //////////////////////////// PAUSE MENU ////////////////////////////////////////

    public void setPauseMenu()
    {

        unlockCursor();
        //manager = FindObjectOfType<UIManager>();
        FindObjectOfType<sceneManager>().setPauseMenu();
        manager.setPauseMenu();

    }

    public void goToSettings()
    {

        //manager = FindObjectOfType<UIManager>();
        manager.goToSettings();
        setSettingsValues();

    }

    public void backToGame()
    {

        lockCursor();
        //manager = FindObjectOfType<UIManager>();
        manager.goToGame();
        FindObjectOfType<sceneManager>().setPauseMenu();

    }
		
    ////////////////////////////////////////////////////////////////////////////////////

    public void loadLevel()
    {
        int level = loadGame();
        //manager = FindObjectOfType<UIManager>();
        manager.loadLevels(level);
    }

    public void exitGame()
    {
        Application.OpenURL("https://losersengine.github.io/losersEngineWeb/");
    }

    public void changeScene(string newScene)
    {

        unlockCursor();
        //manager = FindObjectOfType<UIManager>();
        manager.SceneChanged();
        SceneManager.LoadScene(newScene);

    }

    // Update is called once per frame
    void Update()
    {

        playing = GameObject.FindGameObjectWithTag("Player") != null;
        if (playing && Input.GetKeyDown(KeyCode.P))
        {
            setPauseMenu();
        }

    }

    ///////////////////////////////////////////////////////////////////////////////////////


	public void lockCursor(){
		if (!mobile) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void unlockCursor(){
		if (!mobile) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
