using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float MaxHealth = 80f;
    public float Health;
    public int Damage = 8;
    public bool IsWalking;

    private Joystick joystick;
    private Slider healthBar;

    private Rigidbody2D rb;
    private AnimationController animationController;
    private WeaponCreator weaponCreator;
    private AttackController attackController;

    private string currentDirection;
    private int control;

    public event Action OnPlayerDied;

    public void InitUI(Joystick joystickRef, Slider healthBarRef)
    {
        if (healthBarRef == null)
        {
            healthBarRef = FindAnyObjectByType<Slider>();
        } 
        
        healthBar = healthBarRef;

        healthBar.maxValue = Health / 100;

        if (joystickRef == null) return;
        joystick = joystickRef;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();
        weaponCreator = GetComponent<WeaponCreator>();
        attackController = GetComponent<AttackController>();
        control = PlayerPrefs.GetInt("ControllerIndex", 0);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        Health = MaxHealth;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            OnPlayerDied.Invoke();
        }
        animationController.PlayMovement(rb.linearVelocity.y, rb.linearVelocity.x);

        if (healthBar == null)
        {
            return;
        }

        healthBar.value = Health / 100;
    }

    private void FixedUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (joystick != null)
            {
                moveX = joystick.Horizontal;
                moveY = joystick.Vertical;
            }
        }
        else 
        {
            moveX = Input.GetAxisRaw(control == 0 ? "Horizontal AD" : "Horizontal");
            moveY = Input.GetAxisRaw(control == 0 ? "Vertical WS" : "Vertical");
        }

        rb.linearVelocity = new Vector2(moveX * MoveSpeed, moveY * MoveSpeed);
        IsWalking = Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveY) > 0.01f;

        Debug.Log("PLAYER CONTROLLER...... " + IsWalking);

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            string direction = string.Empty;
            switch (control)
            {
                case 0:
                    direction = GetDirectionFromKeys(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow);
                    break;
                case 1:
                    direction = GetDirectionFromKeys(KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A);
                    break;
            }
            if (direction != string.Empty)
                StartAttack(direction);
            else
            {
                StopAttack();
            }
        }
    }

    public void StartAttack(string direction)
    {
        Attack(direction);

        currentDirection = direction;

        InvokeRepeating(nameof(RepeatAttack), 0.6f, 1f);
    }

    public void StopAttack()
    {
        CancelInvoke(nameof(RepeatAttack));
    }

    private void RepeatAttack()
    {
        Attack(currentDirection);
    }


    private void Attack(string trigger)
    {
        Vector2 direction = animationController.GetAttackDirection(trigger);

        if (weaponCreator != null)
        {
            StartCoroutine(weaponCreator.CreateWeapon(direction));
        }
        else
        {
            attackController.CloseAttack(direction);
        }
    }

    private string GetDirectionFromKeys(KeyCode up, KeyCode down, KeyCode right, KeyCode left)
    {
        if (Input.GetKeyDown(up))
            return "IsAttackUp";
        if (Input.GetKeyDown(down))
            return "IsAttackDown";
        if (Input.GetKeyDown(right))
            return "IsAttackRight";
        if (Input.GetKeyDown(left))
            return "IsAttackLeft";
        return string.Empty;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

}
