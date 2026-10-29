using System;
using UnityEngine;

public class ObjectActivity : MonoBehaviour
{
    [SerializeField] private bool isUI;
    void Start()
    {
        if (isUI)
        {
            gameObject.SetActive(SystemInfo.deviceType == DeviceType.Handheld);
            return;
        }
        int status = UnityEngine.Random.Range(0, 2);
        gameObject.SetActive(Convert.ToBoolean(status));
    }
}
