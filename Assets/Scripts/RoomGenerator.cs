// <copyright file="RoomGenerator.cs" company="ECYSL">
//     Copyright (c) ECYSL. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// The generator of the room.
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    private Vector2 scale = new Vector2(1f, 1f);

    /// <summary>
    /// Gets or sets the wall prefab array.
    /// </summary>
    [field: SerializeField]
    public GameObject[] Wall { get; set; }

    /// <summary>
    /// Gets or sets the floor prefab array.
    /// </summary>
    [field: SerializeField]
    public GameObject[] Floor { get; set; }

    /// <summary>
    /// Gets or sets the point at which the room begins.
    /// </summary>
    [field: SerializeField]
    public Vector2 StartPosition { get; set; }

    /// <summary>
    /// Gets or sets the map data.
    /// <para>0: floor.</para>
    /// <para>1: wall.</para>
    /// </summary>
    [field: SerializeField]
    public int[,] MapData { get; set; } =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        this.GenerateFloor();
        this.GenerateWall();
    }

    private void GenerateFloor()
    {
        for (int i = 0; i < this.MapData.GetLength(0); i++)
        {
            for (int j = 0; j < this.MapData.GetLength(1); j++)
            {
                if (this.MapData[i, j] != 0)
                {
                    continue;
                }

                GameObject curFloor = this.RandomInstantiate(this.Floor);
                curFloor.transform.SetParent(this.transform);
                curFloor.transform.localScale = new Vector3(curFloor.transform.localScale.x * this.scale.x, curFloor.transform.localScale.y * this.scale.y, curFloor.transform.localScale.z);
                float width = curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.x * curFloor.transform.localScale.x;
                float height = curFloor.GetComponent<SpriteRenderer>().sprite.bounds.size.y * curFloor.transform.localScale.y;
                curFloor.transform.position = new Vector3(this.StartPosition.x + ((j - 1) * width), this.StartPosition.y - ((i - 1) * height));
            }
        }
    }

    private void GenerateWall()
    {
        for (int i = 0; i < this.MapData.GetLength(0); i++)
        {
            for (int j = 0; j < this.MapData.GetLength(1); j++)
            {
                if (this.MapData[i, j] != 1)
                {
                    continue;
                }

                GameObject curWall = this.RandomInstantiate(this.Wall);
                curWall.transform.SetParent(this.transform);
                curWall.transform.localScale = new Vector3(curWall.transform.localScale.x * this.scale.x, curWall.transform.localScale.y * this.scale.y, curWall.transform.localScale.z);
                float width = curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.x * curWall.transform.localScale.x;
                float height = curWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y * curWall.transform.localScale.y;
                curWall.transform.position = new Vector3(this.StartPosition.x + ((j - 1) * width), this.StartPosition.y - ((i - 1) * height));
            }
        }
    }

    /// <summary>
    /// To randomly use one of the prefabs when generating walls, floors, etc.
    /// </summary>
    /// <param name="gameObjects">The prefab array.</param>
    /// <returns>The randomly generated prefab.</returns>
    private GameObject RandomInstantiate(GameObject[] gameObjects)
    {
        return Instantiate<GameObject>(gameObjects[Random.Range(0, gameObjects.Length)]);
    }
}
