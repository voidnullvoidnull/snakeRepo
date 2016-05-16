using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class HelloScreen : MonoBehaviour,IPointerClickHandler {

    public int nextScene = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(nextScene);
    }

}
