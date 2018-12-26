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
                setParent(instance);
                break;
            case "English":
                g = en.Find(x => x.name.Equals(scene + "_en"));
                instance = Instantiate(g);
                setParent(instance);
                break;

        }

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
