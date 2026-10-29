using UnityEngine;

public class ControllerCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] controllersObj;
    [SerializeField] private GameObject[] keyboardsObj;
    [SerializeField] private int index;

    private GameObject currentInstance;

    void Start()
    {
        index = PlayerPrefs.GetInt("ControllerIndex", 0);
        SpawnCurrent();
    }

    public void Swap()
    {
        if (currentInstance != null)
            Destroy(currentInstance);

        index = (index + 1) % controllersObj.Length;

        SpawnCurrent();
    }

    private void SpawnCurrent()
    {
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                if (controllersObj == null) return;
                if (controllersObj.Length == 0) return;

                currentInstance = Instantiate(controllersObj[index], transform);
                break;
            case DeviceType.Desktop:
                if (keyboardsObj == null) return;
                if (keyboardsObj.Length == 0) return;

                currentInstance = Instantiate(keyboardsObj[index], transform);
                break;
        }   
    }

    public void SaveControllers()
    {
        PlayerPrefs.SetInt("ControllerIndex", index);
        PlayerPrefs.Save();
    }
}
