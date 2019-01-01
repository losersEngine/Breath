using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler {
    private GameManager gManager;
    public string sceneInvoked;

    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().fillAmount = 240;//.color = new Color(200, 200, 200, 255);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (name == "new_game" || name.Contains("level"))
            gManager.changeScene(sceneInvoked);
        else if (name == "exit")
            gManager.exitGame();
        else
            gManager.changeUIScene(sceneInvoked);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

}
