using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        
        ControllerTest controller = FindObjectOfType<ControllerTest>();

        controller.onChangeWeapon += OnChangeWeapon;
    }

    public void OnChangeWeapon(int num)
    {
        spriteRenderer.sprite = sprites[num];
    }
}
