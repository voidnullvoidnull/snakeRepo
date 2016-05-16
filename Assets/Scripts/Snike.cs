using System;
using UnityEngine;
using System.Collections.Generic;

public class Snike : MonoBehaviour
{
    public delegate void snikeEvent();
    public static event snikeEvent AddFood;
    public static event snikeEvent SnikeDead;
     
    public GameObject segmentPrefab;
    Queue<GameObject> created = new Queue<GameObject>();

    public float SegmentOffset = 2f;
    public static int SegmentCount = 1;
    public static int snakeSensetivity = 25;

    float timer = 0f;
    public float tick = 1f;
    public float speedGrowCoefficient = 0.95f;

    Vector3 snakeCreenPos;
    Vector3 mouseScreenPos;
    SnakeDirection direction;


    public enum SnakeDirection
    {
        PosX,
        NegX,
        PosY,
        NegY
    }


    void Move(Vector3 offset)
    {
        if (created.Count >= SegmentCount)
        {
            created.Dequeue().GetComponent<GuiCell>().state = GuiCell.CellState.Hiding;
        }
        transform.position += offset;
        created.Enqueue((GameObject)Instantiate(segmentPrefab, transform.position, Quaternion.identity));


    }


    SnakeDirection CalcDirection()
    {
        SnakeDirection calcDirection = direction;

        float xAbsDist = Math.Abs(mouseScreenPos.x - snakeCreenPos.x);
        float yAbsDis = Math.Abs(mouseScreenPos.y - snakeCreenPos.y);

        if (xAbsDist>yAbsDis)
        {
            if (mouseScreenPos.x > snakeCreenPos.x && direction != SnakeDirection.NegX)
            { 
                    calcDirection = SnakeDirection.PosX;
                    return calcDirection;
            }
            else if(direction != SnakeDirection.PosX)
            {
                    calcDirection = SnakeDirection.NegX;
                    return calcDirection;
             }
        }

        else if (yAbsDis >= xAbsDist)
        {
            if (mouseScreenPos.y < snakeCreenPos.y && direction != SnakeDirection.PosY)
            {
                calcDirection = SnakeDirection.NegY;
                return calcDirection;
            }

            else if (direction != SnakeDirection.NegY)
            {
                calcDirection = SnakeDirection.PosY;
                return calcDirection;
            }
              
            
        }

        return calcDirection;

    }

    void ApplyFood(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, dir,10f);

        if (hit)
        {

            if (hit.distance <=0.1f && hit.transform.gameObject.layer == 8)
            {
                SegmentCount ++;
                tick *= speedGrowCoefficient;
                hit.transform.GetComponent<GuiCell>().state = GuiCell.CellState.Hiding;
                AddFood();
            }

            if (hit.transform.gameObject.layer == 9 && hit.distance < 0.01f)
            {
                SnikeDead();
            }
        }
    }

    void Start()
    {
        tick = (float)1 / Preferences.Speed;
        SegmentCount = 1;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tick)
        {

            snakeCreenPos = Camera.main.WorldToScreenPoint(transform.position);
            mouseScreenPos = Input.mousePosition;
            direction = CalcDirection();

            switch (direction)
            {
                case SnakeDirection.PosX:
                    Move(Vector3.right * SegmentOffset);
                    ApplyFood(Vector3.right);
                    break;
                case SnakeDirection.PosY:
                    Move(Vector3.up * SegmentOffset);
                    ApplyFood(Vector3.up);
                    break;
                case SnakeDirection.NegX:
                    Move(Vector3.left * SegmentOffset);
                    ApplyFood(Vector3.left);
                    break;
                case SnakeDirection.NegY:
                    Move(Vector3.down * SegmentOffset);
                    ApplyFood(Vector3.down);
                    break;

            }

            timer = 0;
        }

    }
}
