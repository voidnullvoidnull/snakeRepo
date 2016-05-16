using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotePad : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Text>().text = Preferences.NotepadText;

	}
	
}
