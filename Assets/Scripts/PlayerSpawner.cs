using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] Players;

    private PlayerController player;
    private SceneController sceneController;

    [Header("Right Controller")]
    [SerializeField] private GameObject rightController;

    [Header("Left Controller")]
    [SerializeField] private GameObject leftController;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        player = Instantiate(Players[PlayerPrefs.GetInt("MC")], transform.position, Quaternion.identity).GetComponent<PlayerController>();
        sceneController = GetComponent<SceneController>();
        player.OnPlayerDied += HandlePlayerDeath;
    }

    private void Start()
    {
        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        yield return null; 

        int index = PlayerPrefs.GetInt("ControllerIndex");

        Joystick joystick = FindJoystick(index);
        player.InitUI(joystick, healthBar);

        FindButton(index);
    }

    #region Joystick
    private Joystick FindJoystick(int index)
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            return null;
        switch (index)
        {
            case 0:
                return GetJoystick(leftController);
            case 1:
                return GetJoystick(rightController);
            default:
                Debug.Log("Joystick is not found.");
                break;
        }

        return null;
    }

    private Joystick GetJoystick(GameObject controller)
    {
        if (controller == null)
        {
            return null;
        }

        Joystick joystick = controller.GetComponentInChildren<Joystick>();
        return joystick;
    }
    #endregion

    #region Attack Buttons
    private void FindButton(int index)
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            return;
        switch (index)
        {
            case 0:
                GetAttackButtons(rightController);
                break;
            case 1:
                GetAttackButtons(leftController);
                break;
            default:
                Debug.Log("Attack Buttons are not found.");
                break;
        }
    }

    private void GetAttackButtons(GameObject controller)
    {
        if (controller == null)
        {
            return;
        }

        Transform attackButtons = controller.transform.Find("AttackButtons(Clone)");

        if (attackButtons == null)
        {
            Debug.LogError($"Не найден объект AttackButtons в {controller.name}");
            return;
        }


        Button up = attackButtons.Find("AttackUp")?.GetComponent<Button>();
        Button down = attackButtons.Find("AttackDown")?.GetComponent<Button>();
        Button left = attackButtons.Find("AttackLeft")?.GetComponent<Button>();
        Button right = attackButtons.Find("AttackRight")?.GetComponent<Button>();

        BindButtons(up, down, right, left);
    }

    private void BindButtons(Button up, Button down, Button right, Button left)
    {
        if (up != null)
        {
            SetupButton(up, player.StartAttack, player.StopAttack, "IsAttackUp");
        }
        if (down != null)
        {
            SetupButton(down, player.StartAttack, player.StopAttack, "IsAttackDown");
        }
        if (right != null)
        {
            SetupButton(right, player.StartAttack, player.StopAttack, "IsAttackRight");
        }
        if (left != null)
        {
            SetupButton(left, player.StartAttack, player.StopAttack, "IsAttackLeft");
        }
    }

    private void SetupButton(Button button,
                        Action<string> startAction,
                        Action stopAction,
                        string direction)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

        var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener(_ => startAction(direction));

        var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener(_ => stopAction());

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }
    #endregion

    private void HandlePlayerDeath()
    {
        if (!sceneController.GameOverPanel.activeSelf)
        {
            sceneController.ActivatePanel(sceneController.GameOverPanel);
        }
    }


}
