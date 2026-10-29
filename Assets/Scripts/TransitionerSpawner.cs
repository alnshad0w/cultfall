using UnityEngine;

public class TransitionerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject transitonObj;
    private bool hasSpawned;

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemies), 0f, 1f);
    }

    private void CheckEnemies()
    {
        if (hasSpawned) return;

        if (GameObject.FindGameObjectWithTag("Enemy") != null) return;

        Instantiate(transitonObj);
        hasSpawned = true;

        CancelInvoke(nameof(CheckEnemies));
        Destroy(this);
    }
}
