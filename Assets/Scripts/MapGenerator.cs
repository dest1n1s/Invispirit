using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Floor;
    public Vector2 StartPosition;
    public Vector2 FloorSize;
    public Vector2 WallSize;
    public Vector2 WallAmount;
    // Start is called before the first frame update
    void Start()
    {
        GenerateFloor();
        GenerateWall();
    }

    void GenerateFloor()
    {
        for (int i = 2; i <= WallAmount.x - 1; i++)
            for (int j = 2; j <= WallAmount.y - 1; j++)
            {
                GameObject curFloor = Instantiate<GameObject>(Floor);
                curFloor.transform.localScale = new Vector3(FloorSize.x / curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                    FloorSize.y / curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
                curFloor.transform.position = new Vector3(StartPosition.x + (i - 1) * FloorSize.x, StartPosition.y - (j - 1) * FloorSize.y);
            }
    }
    void GenerateWall()
    {
        for(int i = 1; i <= WallAmount.x; i++)
        {
            GameObject curWall = Instantiate<GameObject>(Wall);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (i - 1) * WallSize.x, StartPosition.y);
        }
        for (int i = 2; i <= WallAmount.y - 1; i++)
        {
            GameObject curWall = Instantiate<GameObject>(Wall);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x, StartPosition.y - (i - 1) * WallSize.x);
        }
        for (int i = 2; i <= WallAmount.y - 1; i++)
        {
            GameObject curWall = Instantiate<GameObject>(Wall);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (WallAmount.x - 1) * WallSize.x, StartPosition.y - (i - 1) * WallSize.x);
        }
        for (int i = 1; i <= WallAmount.x; i++)
        {
            GameObject curWall = Instantiate<GameObject>(Wall);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (i - 1) * WallSize.x, StartPosition.y - (WallAmount.y - 1) * WallSize.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
