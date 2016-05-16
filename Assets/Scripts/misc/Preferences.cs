using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System;

[System.Serializable]
public struct Prefs
{
    public int ScoresToWin;
    public int FoodCount;
    public int ObstacklesCount;
    public int Speed;
    public string NotepadText;

    public Prefs(int toWin, int FC, int obsCount, int speed, string nText)
    {
        ScoresToWin = toWin;
        FoodCount = FC;
        ObstacklesCount = obsCount;
        Speed = speed;
        NotepadText = nText;

    }
}




public static class Preferences {
    public static Prefs current;

    public static int ScoresToWin = 10;
    public static int FoodCont = 3;
    public static int ObstacklesCount = 38;
    public static int Speed = 2;
    public static string NotepadText = "Coffee - large, medium sugar, no cream.";



    public static void Save()
    {

        using (FileStream fs = new FileStream("data", FileMode.OpenOrCreate))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, current);
            fs.Close();
            Debug.Log("Saved");
        }
    }


    public static void Load()
    {
            using (FileStream fs = new FileStream("data", FileMode.OpenOrCreate))
            {
                    BinaryFormatter bf = new BinaryFormatter();
                    current = (Prefs)bf.Deserialize(fs);

                    ScoresToWin = current.ScoresToWin;
                    FoodCont = current.FoodCount;
                    ObstacklesCount = current.ObstacklesCount;
                    Speed = current.Speed;
                    NotepadText = current.NotepadText;
                
                fs.Close();
                Debug.Log("Loaded");
            }
        }
    }



