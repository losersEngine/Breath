using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour {

    AsyncOperation async;
    private string sceneToChange;
    private UIManager manager;
    private GameObject screen;

    // Use this for initialization
    void Start () {

        manager = FindObjectOfType<UIManager>();
        screen = GameObject.FindGameObjectWithTag("loading");
        screen.SetActive(false);
    }
	
    public void showLoadScreen()
    {
        screen.SetActive(true);

    }
    public void startLoading(string newScene)
    {
        sceneToChange = newScene;
        StartCoroutine(loadingScreen());
    }

    IEnumerator loadingScreen()
    {

        Slider progress = GameObject.Find("Slider").GetComponent<Slider>();
        async = SceneManager.LoadSceneAsync(sceneToChange);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            progress.value = async.progress;
            if (async.progress == 0.9f)
            {
                progress.value = 1f;
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
