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
    public event UnityAction<Door_Type> onNextDungeon;

    public Door_Type doorType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onNextDungeon.Invoke(doorType);
        }
    }

}
