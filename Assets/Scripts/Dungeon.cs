using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
        int x = 0;
        int y = 0;

        y = Random.Range(0, 4); // 0~3

        rooms[x, y] = 0;
        while(x != 4)
        {
            int num = Random.Range(0,2);
            if(num == 0 && !CheckPath(x, y - 1))//왼쪽으로
            {
                y--;
                rooms[x, y] = 1;
            }
            else if (num == 1 && !CheckPath(x, y + 1))//오른쪽으로
            {
                y++;
                rooms[x, y] = 1;
            }
            else
            {
                x++;
                if (x == 4)
                {
                    rooms[x - 1, y] = 4;
                    break;
                }
                rooms[x, y] = 1;
                
            }

        }

        for(int i = 0; i< 4; i++)
        {
            for(int j = 0; j<4; j++)
            {
                Debug.Log("rooms " + i + " , " + j + " : " + rooms[i, j]);
            }
        }
    }

    public bool CheckPath(int x, int y)
    {
        //return rooms[y, x].roomState == Room.RoomState.Start || rooms[y, x].roomState == Room.RoomState.Path;
        if(y<0 || x<0|| y>3||x>3)
        {
            return true;
        }
        if (rooms[x, y] == 1 || rooms[x,y] == 0)
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
