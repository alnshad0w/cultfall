using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBooster : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private int booster;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        player.Damage *= booster;

        Debug.Log("Player damage: " + player.Damage.ToString());
    }
}
