using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class TileData : ScriptableObject
{
    [Header("0=∂•,1=∫Æ,3,4πÃ¡§")]
    [SerializeField] public Tile[] tiles = new Tile[4];
}
