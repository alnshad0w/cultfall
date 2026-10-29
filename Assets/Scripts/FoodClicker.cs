using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodClicker : MonoBehaviour
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private string panelName;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private string animName = "appearing panel";

    private SceneController sceneController;
    private Transform player;
    private BoxCollider2D clickCollider;

    private bool isInteracting = false;

    private void Awake()
    {
        sceneController = FindFirstObjectByType<SceneController>();
        clickCollider = GetComponent<BoxCollider2D>();

        if (panelToShow == null )
        {
            FindPanel(panelName);
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isInteracting && !panelToShow.activeSelf)
        {
            isInteracting = false;
            clickCollider.enabled = true;
        }
    }

    private void OnMouseDown()
    {
        if (isInteracting || !IsPlayerInRange()) return;

        if (panelToShow != null)
        {
            isInteracting = true;
            sceneController?.ActivatePanel(panelToShow, animName);
            clickCollider.enabled = false;
        }
    }


    private void FindPanel(string name)
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas != null)
        {
            Transform found = canvas.transform.Find(name);
            if (found != null)
                panelToShow = found.gameObject;
        }
    }

    private bool IsPlayerInRange()
    {
        if (player == null) return false;

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= interactionDistance;
    }
   
}
