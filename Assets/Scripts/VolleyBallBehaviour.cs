using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyBallBehaviour : MonoBehaviour
{
    [SerializeField] private float minHitForce = 8f;
    [SerializeField] private float maxHitForce = 20f;
    [SerializeField] private float bounceFactor = 0.8f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayerHit(Vector2 hitDirection, float hitPower)
    {
        hitPower = Mathf.Clamp01(hitPower); 
        float force = Mathf.Lerp(minHitForce, maxHitForce, hitPower);

        rb.linearVelocity = hitDirection.normalized * force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 playerToBall = transform.position - collision.transform.position;

            float playerSpeed = collision.rigidbody?.linearVelocity.magnitude ?? 1f;
            float hitPower = Mathf.Clamp(playerSpeed / 5f, 0.2f, 1f);

            PlayerHit(playerToBall.normalized, hitPower);
        }
        else 
        {
            rb.linearVelocity *= bounceFactor;
        }
    }
}
