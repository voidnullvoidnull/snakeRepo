using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GuiSettings : MonoBehaviour {



    public Slider ToWinSlider;
    public Slider FoodSlider;
    public Slider ObstacklesSlider;
    public Slider SpeedSlider;
    public InputField NotepadTextInput;

    private Prefs tempPrefs;

    void Start()
    {
        
        tempPrefs = Preferences.current;

        ToWinSlider.value = tempPrefs.ScoresToWin;
        FoodSlider.value = tempPrefs.FoodCount;
        ObstacklesSlider.value = tempPrefs.ObstacklesCount;
        SpeedSlider.value = tempPrefs.Speed;
        NotepadTextInput.text = tempPrefs.NotepadText;

        
}

    public void OnWinScoresChanged(float value)
    {
        Preferences.current.ScoresToWin = (int)value;
    }

    public void OnFoodValueChanged(float value)
    {
        Preferences.current.FoodCount = (int)value;
    }

    public void OnObstacklesCountValueChanged(float value)
    {
        Preferences.current.ObstacklesCount = (int)value;
    }

    public void OnSpeedValueChanged(float value)
    {
        Preferences.current.Speed = (int)value;
    }

    public void OnNotepadTextValueChanged(string value)
    {
        Preferences.current.NotepadText = value;
    }

    public void Save()
    {
        SceneManager.LoadScene(0);
        Preferences.Save();
        
    }

    public void Exit()
    {
        Application.Quit();
    }

}
