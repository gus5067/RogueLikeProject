using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Door_Type
{
    Start, Exit
}
public class DungeonDoor : MonoBehaviour
{

    public Door_Type doorType;

    private string dungeon = "DungeonScene";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch(doorType)
            {
                case Door_Type.Start:
                    GameManager.Instance.DungeonNum = 0;
                    LoadManager.LoadScene("TownScene");
                    break;
                case Door_Type.Exit:
                    GameManager.Instance.DungeonNum++;
                    LoadManager.LoadScene(dungeon);
                    break;
            }
        }
    }

}
