using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PoiterPosition { get; set; }

    public SpriteRenderer characterRender, weaponRender;

    private void Update()
    {
        Vector2 direction = (PoiterPosition-(Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            Vector2 a;
            a.y = Mathf.Abs(scale.y);
            scale.y = -1*a.y;
        }
        else
        {
            scale.y = Mathf.Abs(scale.y);
        }

        if(transform.eulerAngles.z>0 && transform.eulerAngles.z < 100)
        {
            weaponRender.sortingOrder = characterRender.sortingOrder - 1;
        }
        else
        {
            weaponRender.sortingOrder = characterRender.sortingOrder + 1;
        }

        transform.localScale = scale;
    }
}
