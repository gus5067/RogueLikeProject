using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomState
{
    Start = 0, Path = 1, Random = 3, Exit = 4
}

public class Room : MonoBehaviour
{

    public RoomState roomState;

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] GameObject startPrefab;

    [SerializeField] GameObject exitPrefab;

    [SerializeField] GameObject pathPrefab;

    [SerializeField] GameObject randomPrefab;


    public GameObject SetRoom(RoomState roomState)
    {
        switch (roomState)
        {
            case RoomState.Start:
                return Instantiate(startPrefab);
            case RoomState.Path:
                return Instantiate(pathPrefab);
            case RoomState.Exit:
                return Instantiate(exitPrefab);
            case RoomState.Random:
                return Instantiate(randomPrefab);
        }
        return null;
    }

    private int[,] rooms = new int[4, 4] { { 3, 3, 3, 3 }, { 3, 3, 3, 3 }, { 3, 3, 3, 3 }, { 3, 3, 3, 3 } };

    private void Start()
    {
        MakePath();
        SetTile();
    }

    public void SetTile()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch(rooms[i,j])
                {
                    case 0:
                        GameObject startObj = SetRoom(RoomState.Start);
                        startObj.transform.position = new Vector3(width * j, height * -i, 0);
                        break;
                    case 1:
                        GameObject pathObj = SetRoom(RoomState.Path);
                        pathObj.transform.position = new Vector3(width * j, height * -i, 0);
                        break;
                    case 4:
                        GameObject exitObj = SetRoom(RoomState.Exit);
                        exitObj.transform.position = new Vector3(width * j, height * -i, 0);

                        break;
                    default:
                        GameObject randomObj = SetRoom(RoomState.Random);
                        randomObj.transform.position = new Vector3(width * j, height * -i, 0);
                        break;
                }
            }
        }
    }
    public void MakePath()
    {
        int x = 0;
        int y = 0;

        y = Random.Range(0, 4); // 0~3

        rooms[x, y] = 0;
        while (x != 4)
        {
            int num = Random.Range(0, 2);
            if (num == 0 && !CheckPath(x, y - 1))//왼쪽으로
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

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log("rooms " + i + " , " + j + " : " + rooms[i, j]);
            }
        }
    }

    public bool CheckPath(int x, int y)
    {
        //return rooms[y, x].roomState == Room.RoomState.Start || rooms[y, x].roomState == Room.RoomState.Path;
        if (y < 0 || x < 0 || y > 3 || x > 3)
        {
            return true;
        }
        if (rooms[x, y] == 1 || rooms[x, y] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
