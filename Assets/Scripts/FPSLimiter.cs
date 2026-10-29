using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    private int fps = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
    }
}
