using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    private int baseLayer = 0;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float offset;
    [SerializeField] private bool isStatic;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(baseLayer - transform.position.y + offset);

        if (isStatic)
        {
            Destroy(this);
        }
    }
}
