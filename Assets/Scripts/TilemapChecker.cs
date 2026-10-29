using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker : MonoBehaviour
{
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        if (tilemap != null)
        {
            tilemap.CompressBounds(); 
            Bounds bounds = tilemap.localBounds;

            Debug.Log("Размер Tilemap (ширина x длина): " + bounds.size);
        }
    }
}
