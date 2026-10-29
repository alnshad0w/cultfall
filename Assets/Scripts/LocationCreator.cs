using UnityEngine;

public class LocationCreator : MonoBehaviour
{
    [SerializeField] private bool isActive = true; 
    [SerializeField] private GameObject[] locations;

    private void Awake()
    {
        if (!isActive) return;

        var index = Random.Range(0, locations.Length);
        PlayerPrefs.SetInt("LvTrack", index);
        PlayerPrefs.Save();

        Instantiate(locations[index], transform);
    }
}
