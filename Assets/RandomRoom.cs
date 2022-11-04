using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomRoom : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField, Range(0, 100)]
    private int randomPercent;

    private int[,] map;

    private void Awake()
    {
        tileMap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        map = new int[width, height];
        FillRandomRoom();
    }

    public void FillRandomRoom()
    {

        System.Random random = new System.Random(Random.Range(0, 500).ToString().GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x==0||x==width-1||y==0||y==height-1)//¸Ê Å×µÎ¸®
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (random.Next(0, 100) < randomPercent) ? 1 : 0;
                }
                if(map[x, y] == 1)
                {
                    SetTile(x, y);
                }
            }
        }
    }

    public void SetTile(int x, int y)
    {
        Vector3Int pos = new Vector3Int(-width / 2 + x, -height / 2 + y, 0);
        tileMap.SetTile(pos, wallTile);
    }
}
