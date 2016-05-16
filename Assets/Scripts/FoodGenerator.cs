using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FoodGenerator : MonoBehaviour,IGenerator {


    public GameObject FoodPrefab;
    public Transform rightTop;
    public Transform leftBottom;
    public int boardersMargin = 4;
    int seed = System.DateTime.Now.Second;
    int foodCount;

    System.Random rnd;

    Vector3 GetRandomCoordinates()
    {
        seed++;
        rnd = new System.Random(seed);
        Vector3 coord;

        int x = 1, y = 1;

        while (x%2 != 0)
        {
            x = rnd.Next((int)leftBottom.position.x + boardersMargin, (int)rightTop.position.x);
        }
        while (y % 2 != 0)
        {
            y = rnd.Next((int)leftBottom.position.y + boardersMargin, (int)rightTop.position.y);
        }
        coord = new Vector3(x, y);
        return coord;
    }

    void Start()
    {
   
    }


    public void Generate()
    {
        foodCount = Preferences.FoodCont;
        int childcount = transform.childCount;

        for(int i = 0; i<childcount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<GuiCell>().state = GuiCell.CellState.Hiding;
        }


        for (int i = 0; i < foodCount; i++)
        {
            Vector3 coord = GetRandomCoordinates();
            Collider2D hit = Physics2D.OverlapPoint(coord);

            while (hit)
            {
                coord = GetRandomCoordinates();
                hit = Physics2D.OverlapPoint(coord);
            }

            GameObject food = (GameObject)Instantiate(FoodPrefab, coord, Quaternion.identity);
            food.transform.SetParent(transform);
        }
    }
}
