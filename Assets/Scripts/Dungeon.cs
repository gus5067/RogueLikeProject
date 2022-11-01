using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dungeon : MonoBehaviour
{

    //1은 시작 지점 
    //[SerializeField] Tilemap tileMap;

    //private Room[,] rooms = new Room[4, 4];
    private int[,] rooms = new int[4, 4] { { 3, 3, 3, 3 }, { 3, 3, 3, 3 }, { 3, 3, 3, 3 }, { 3, 3, 3, 3 } };

    private void Start()
    {

        MakePath();
    }
    public void MakePath()
    {
        int x = Random.Range(0, 4);
        Debug.Log(x);
        //rooms[0, x].roomState = Room.RoomState.Start;
        rooms[0, x] = 0;
        for (int y = 0; y < 4;)
        {
            int rando = Random.Range(0, 2);
            if (rando == 0 && !CheckPath(y, x + 1))
            {
                x++;
                rooms[y, x] = 1;
            }
            else if (rando == 1 && !CheckPath(y, x - 1))
            {
                x--;
                rooms[y, x] = 1;
            }
            else
            {
                if(y == 3)
                {
                    rooms[y, x] = 4;
                }
                rooms[y, x] = 1;
                y++;
            }

        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(i+" , "+ j + " : " + rooms[i, j]);
            }
        }
    }

    public bool CheckPath(int y, int x)
    {
        //return rooms[y, x].roomState == Room.RoomState.Start || rooms[y, x].roomState == Room.RoomState.Path;
        if(y<0 || x<0|| y>3||x>3)
        {
            return true;
        }
        if (rooms[y, x] == 1 || rooms[y,x] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Room
{
    public enum RoomState
    {
        Start, Path, Random, Exit
    }

    public RoomState roomState = RoomState.Random;
    //0이 땅 1이 벽
    //[SerializeField] private Tile tile;

    //private int[,] map = new int[16,16];

    //public void FillRoom()
    //{
    //    for (int x = 0; x < 16; x++)
    //    {
    //        for (int y = 0; y < 16; y++)
    //        {
    //            if (x == 0 || x==15 || y == 0 || y ==15)
    //            {
    //                map[x, y] = 1;
    //            }
    //            else
    //            {
    //                map[x, y] = 0;
    //            }
    //        }
    //    }
    //}


}
