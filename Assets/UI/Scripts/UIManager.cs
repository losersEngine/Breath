using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Use this for initialization
    private SystemLanguage lang;
    public List<GameObject> es;
    public List<GameObject> en;
    private string scene;

    void Start () {

        SceneManager.sceneLoaded += OnSceneLoaded;
        scene = "main_menu"; // SE CAMBIA CADA VEZ QUE SE PASA A OTRA PANTALLA
        lang = Application.systemLanguage;
        InstantiateLanguage();

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene.name;
        if(!scene.name.Contains("Level"))
            InstantiateLanguage();
    }

    public void SetBrightness(GameObject obj)
    {
        Slider slider = obj.GetComponent<Slider>();
        RenderSettings.skybox.color = new Color(slider.value, slider.value, slider.value, 1.0f);
        Debug.Log("brillo:" + slider.value);//.GetComponent<Slider>().value);
    }

    private void InstantiateLanguage() {
        Debug.Log("Entra");
        GameObject g;
        GameObject instance;
        string language = this.lang.ToString();
        switch (language)
        {

            case "Spanish":
                g = es.Find(x => x.name.Equals(scene + "_es"));
                instance = Instantiate(g);
                setParent(instance);
                break;
            case "English":
                g = en.Find(x => x.name.Equals(scene + "_en"));
                instance = Instantiate(g);
                setParent(instance);
                break;

        }

    }


    public void setLanguage(string l)
    {
        lang = l.Equals("Spanish")?SystemLanguage.Spanish: SystemLanguage.English;
    }

    private void setParent(GameObject instance)
    {
        GameObject parent = GameObject.FindGameObjectWithTag("canvas");
        instance.transform.parent = parent.transform;
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update () {
		
	}


}
