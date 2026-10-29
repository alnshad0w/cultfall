using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramController : WeaponController
{
    public float activationDelay = 0.5f;
    private float timer;
    private bool isActive = false;


    private void Update()
    {
        if (!isActive)
        {
            timer += Time.deltaTime;
            if (timer >= activationDelay)
            {
                isActive = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!isActive) return;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(player.Damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(player.Damage);
        }
    }
}
