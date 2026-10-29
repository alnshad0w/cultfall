using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public Vector3 Offset;

    private float smoothness = 0.125f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(player.transform.position, player.transform.position + Offset, smoothness);
    }
}
