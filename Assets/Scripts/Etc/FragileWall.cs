using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FragileWall : MonoBehaviour
{
    private Tilemap tileMap;

    private void Awake()
    {
        tileMap = GetComponent<Tilemap>();
    }

    public void BreakWall(Vector3 pos)
    {
        Vector3Int tilePos = tileMap.WorldToCell(pos);

        tileMap.SetTile(tilePos, null);
    }

}
