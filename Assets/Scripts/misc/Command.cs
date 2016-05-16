using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Command : MonoBehaviour {
    
    string command = "";
    public string exitCommand = "556221";

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            command += Input.inputString;
            if (command.EndsWith(exitCommand))
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
