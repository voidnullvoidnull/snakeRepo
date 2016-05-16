using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

[SerializeField]
public interface IGenerator
{
    void Generate();
}

public class GameController : MonoBehaviour {


    public int scores = 0;
    public int needScores = 10;

    [SerializeField] GameObject foodGenerator;
    [SerializeField] GameObject obstacklesGenerator;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    int deadSceneIndex;

    [SerializeField]
    int winSceneIndex;


    void Awake()
    {
        Snike.AddFood += AddScores;
        Snike.SnikeDead += GameOver;
        Preferences.Load();
    }

    void OnDestroy()
    {
        Snike.AddFood -= AddScores;
        Snike.SnikeDead -= GameOver;
    }

	void Start () {
        needScores = Preferences.ScoresToWin;

        obstacklesGenerator.GetComponent<IGenerator>().Generate();
        foodGenerator.GetComponent<IGenerator>().Generate();
	    scoreText.text = scores + " / " + needScores;
	}
	
    void AddScores()
    {
        scores++;
        if (scores >= needScores)
            SceneManager.LoadScene(winSceneIndex);

        scoreText.text = scores + " / " + needScores;
        foodGenerator.GetComponent<IGenerator>().Generate();
    }

    void GameOver()
    {
        SceneManager.LoadScene(deadSceneIndex);
    }
	
}
