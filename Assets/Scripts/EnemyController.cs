using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health, Speed;
    public int Damage;
    public bool CanMove = false;

    [SerializeField] protected float distance = 0.8f;
    [SerializeField] protected float attackDistance = 1.5f;

    protected PlayerController player;
    protected Rigidbody2D rb;
    protected AnimationController animationController;
    protected WeaponCreator weaponCreator;

    protected bool canThrow = false;

    private float startDelay = 1f;
    private float elapsedTime = 0f;  

    protected virtual void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        animationController = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody2D>();
        weaponCreator = GetComponent<WeaponCreator>();

        StartCoroutine(ThrowAccess());
    }

    protected virtual void Update()
    {
        if (!CanMove)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= startDelay)
                CanMove = true;

            rb.linearVelocity = Vector2.zero;
            return;   
        }

        if (Health <= 0)
        {
            animationController.PlayFalling();
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            rb.linearVelocity = (player.transform.position - transform.position).normalized;

            if (attackDistance < distanceToPlayer && distanceToPlayer > distance)
            {
                if (canThrow)
                {
                    StartCoroutine(ThrowOrbs());
                }
            }
            if (distance < distanceToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            animationController.PlayMovement(rb.linearVelocity.y, rb.linearVelocity.x);
        }
    }

    protected virtual IEnumerator ThrowOrbs()
    {
        canThrow = false;
        string trigger = animationController.GetTriggerForAttack();
        Vector2 direction = animationController.GetAttackDirection(trigger);
        StartCoroutine(weaponCreator.CreateWeapon(direction));

        yield return new WaitForSeconds(weaponCreator.Weapon.Lifetime);
        canThrow = true;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private IEnumerator ThrowAccess()
    {
        yield return new WaitForSeconds(1.5f);
        canThrow = true;
    }

}
