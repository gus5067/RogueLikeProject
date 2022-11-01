using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dungeon : MonoBehaviour
{
    //1은 시작 지점 
    [SerializeField] Tilemap tileMap;

    private Room[,] rooms = new Room[4, 4];

}

public class Room
{
    //0이 땅 1이 벽
    [SerializeField] private Tile tile;

    private int[,] map = new int[16,16];

    public void FillRoom()
    {
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                if (x == 0 || x==15 || y == 0 || y ==15)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
    }


}
