using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] Wall;
    public GameObject[] Floor;
    public Vector2 StartPosition;
    public Vector2 FloorSize;
    public Vector2 WallSize;
    public float WallHeight;
    public Vector2 WallAmount;
    private Vector2 scale = new Vector2(1f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GenerateFloor();
        GenerateWall();
    }

    void GenerateFloor()
    {
        for (int i = 2; i <= WallAmount.x - 1; i++)
            for (int j = 2; j <= WallAmount.y - 1; j++)
            {

                int r = Random.Range(0, Floor.Length);
                GameObject curFloor = RandomInstantiate(Floor);
                curFloor.transform.SetParent(transform);
                curFloor.transform.localScale = new Vector3(FloorSize.x / curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale.x,
                    FloorSize.y / curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale.y, 0);
                curFloor.transform.position = new Vector3(StartPosition.x + (i - 1) * FloorSize.x, StartPosition.y - WallHeight - (j - 1) * FloorSize.y);
            }
    }
    void GenerateWall()
    {
        for (int i = 1; i <= WallAmount.x; i++)
        {
            GameObject curWall = RandomInstantiate(Wall);
            curWall.transform.SetParent(transform);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (i - 1) * WallSize.x, StartPosition.y);
        }
        for (int i = 2; i <= WallAmount.y - 1; i++)
        {
            GameObject curWall = RandomInstantiate(Wall);
            curWall.transform.SetParent(transform);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x, StartPosition.y - (i - 1) * WallSize.x);
        }
        for (int i = 2; i <= WallAmount.y - 1; i++)
        {
            GameObject curWall = RandomInstantiate(Wall);
            curWall.transform.SetParent(transform);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (WallAmount.x - 1) * WallSize.x, StartPosition.y - (i - 1) * WallSize.x);
        }
        for (int i = 1; i <= WallAmount.x; i++)
        {
            GameObject curWall = RandomInstantiate(Wall);
            curWall.transform.SetParent(transform);
            curWall.transform.localScale = new Vector3(WallSize.x / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale.x,
                WallSize.y / curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale.y, 0);
            curWall.transform.position = new Vector3(StartPosition.x + (i - 1) * WallSize.x, StartPosition.y - (WallAmount.y - 1) * WallSize.x);
        }
    }

    private GameObject RandomInstantiate(GameObject[] gameObjects)
    {
        return Instantiate<GameObject>(gameObjects[Random.Range(0, gameObjects.Length)]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
