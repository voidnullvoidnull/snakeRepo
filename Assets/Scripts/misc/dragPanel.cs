using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class dragPanel : MonoBehaviour,IDragHandler {


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.Lerp(transform.position,eventData.position,Time.deltaTime*10);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
