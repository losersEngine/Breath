using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Use this for initialization
    public string lang = "Spanish";
    public List<GameObject> es;
    public List<GameObject> en;
    public string scene;
    private GameObject instance;
    private int actualLevel = 1;
	private string device;
    private int[] numTextsByLevel = { 2, 1, 1, 1, 2 };


    public void setDevice(string device)
    {
        this.device = device;
    }

    public string getDevice()
    {
        return device;
    }
    public void setLevel(int n)
    {
        actualLevel = n;

    }
    private void Awake()
    {

        scene = "main_menu"; // SE CAMBIA CADA VEZ QUE SE PASA A OTRA PANTALLA
        InstantiateLanguage();

    }

    public void SceneChanged()
    {
        Destroy(GameObject.FindGameObjectWithTag("prefab"));
    }

    public void setLanguage(string v)
    {
        lang = v;
    }

    public string getLanguage()
    {
        return lang;
    }

    public void InstantiateLanguage() {

        GameObject g;

        switch (lang)
        {

            case "Spanish":
                g = es.Find(x => x.name.Equals(scene + "_es"));
                instance = Instantiate(g);
                setParent(instance, "canvas");
                break;
            case "English":
                g = en.Find(x => x.name.Equals(scene + "_en"));
                instance = Instantiate(g);
                setParent(instance, "canvas");
                break;

        }

    }

    public void setActiveJoysticks(bool a, string tag)
    {
        if (device.Equals("desktop"))
        {
            
            Image[] joysticks = GameObject.FindGameObjectWithTag(tag).GetComponentsInChildren<Image>();

            foreach (Image i in joysticks)
            {
                i.enabled = false;
            }

        }
        else //boton de pausa
        {
            GameObject b = Resources.Load<GameObject>("UI/Prefabs/pauseButton");
            b = Instantiate(b);
        }
    }

    public void loadLevels(int actualLevel) {

        GameObject[] niveles = GameObject.FindGameObjectsWithTag("level");
        for(int i= 0; i < niveles.Length; i++)
        {
            GameObject button = niveles[i];
            Image buttonImage = niveles[i].GetComponent<Image>();

            if (i < actualLevel)
            {
                loadSprite(buttonImage, "UI/Buttons", niveles[i].name + "_enable");
            }
            else
            {
                loadSprite(buttonImage, "UI/Buttons", niveles[i].name + "_disable");
                button.GetComponent<Button>().interactable = false;

            }
        }

    }

    public void DestroyWithTag(string tag)
    {
        DestroyImmediate(GameObject.FindGameObjectWithTag(tag));
    }

    public void DestroyPrefab(string name)
    {
        GameObject obj = GameObject.Find(name);
        Destroy(obj);
    }

    public void loadSprite(Image image, string path, string fileName)
    {
        switch (lang)
        {
            case "Spanish":
                image.sprite = Resources.Load<Sprite>(path + "/es/" + fileName);
                break;
            case "English":
                image.sprite = Resources.Load<Sprite>(path + "/en/" + fileName);
                break;
        }

    }


    public void instanceText()
    {
        string language = lang.Equals("Spanish") ? "es" : "en";
        GameObject canvas = Resources.Load<GameObject>("UI/Prefabs/Level_" + language);
        Instantiate(canvas);
        GameObject nextText = Resources.Load<GameObject>("UI/Text/" + language + "/" + scene + "_1");
        GameObject obj = Instantiate(nextText);
        setParent(obj, "canvas");
    }

    public bool nextText()
    {

        int numberNext = 1;
        string language = lang.Equals("Spanish") ? "es" : "en";
        GameObject text1 = GameObject.FindGameObjectWithTag("text");
        char[] arrayNameText = text1.name.Split('(')[0].ToCharArray();

        numberNext += (int) char.GetNumericValue(arrayNameText[arrayNameText.Length - 1]);
        Destroy(text1);

        if(numberNext <= numTextsByLevel[actualLevel - 1])
        {
            GameObject nextText = Resources.Load<GameObject>("UI/Text/" + language + "/" + scene + "_" + numberNext);
            if (!nextText)
            {
                nextText = Resources.Load<GameObject>("UI/Text/" + language + "/" + scene + "_" + device + "_" +  numberNext); //hay textos concretos para movil y otros para ordenador
            }
            GameObject obj = Instantiate(nextText);
            setParent(obj, "canvas");
            return false;
        }
        else
        {
            DestroyPrefab("Level_" + language + "(Clone)");
            return true;
        }

    }

    public void setPauseMenu() {

        if (GameObject.Find("pauseButton(Clone)"))
        {
            GameObject.Find("pauseButton(Clone)").GetComponentInChildren<Image>().enabled = false;
        }
        string language = lang.Equals("Spanish") ? "es" : "en";
        GameObject menuPause = GameObject.Find("pause_" + language + "(Clone)");
        if (!menuPause) {

            GameObject menu = Resources.Load<GameObject>("UI/Prefabs/pause_" + language);
            GameObject obj = Instantiate(menu);

        }else
        {
            Destroy(menuPause);
        }

    }

    public void goToSettings() {

        string language = lang.Equals("Spanish") ? "es" : "en";
        DestroyPrefab("buttons");
        GameObject menu = Resources.Load<GameObject>("UI/Prefabs/settings_" + language);
        GameObject obj = Instantiate(menu);
        setParent(obj, "prefab");
        obj.transform.localScale = new Vector3(0.6f,0.6f,0.6f);

        GameObject.Find("back").GetComponent<Button>().onClick.AddListener(() => {

            DestroyImmediate(GameObject.Find("pause_" + language + "(Clone)"));
            setPauseMenu(); //volvemos al menu de pausa
        });

    }

    public void goToGame(){

        Destroy(GameObject.FindGameObjectWithTag("canvas"));
        if (GameObject.Find("pauseButton(Clone)"))
        {
            Image button = GameObject.Find("pauseButton(Clone)").GetComponentInChildren<Image>();
            button.enabled = true;

        }

    }

    public void setMessageInteract()
    {

        GameObject message = Resources.Load<GameObject>("UI/Prefabs/message");
        GameObject obj = Instantiate(message);
        Image img = obj.GetComponentInChildren<Image>();
        loadSprite(img, "UI/Images", "press_" + device);

    }

    public void instantiateGO()
    {
        string language = lang.Equals("Spanish") ? "es" : "en";
        GameObject buttons = Resources.Load<GameObject>("UI/Prefabs/game_over_" + language);
        Instantiate(buttons);

    }
    public void destroyMessageInteract()
    {
        Destroy(GameObject.FindGameObjectWithTag("text"));
    }

    private void setParent(GameObject instance, string tag)
    {
        GameObject parent = GameObject.FindGameObjectWithTag(tag);
        instance.transform.SetParent(parent.transform,false);
        instance.transform.localEulerAngles = Vector3.zero;
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update () {
		
	}

    


}
