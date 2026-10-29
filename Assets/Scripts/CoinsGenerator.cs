using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    private int coins;
    private int minCoins = 300;
    private int maxCoins = 600;

    [SerializeField] private bool isLevelOne = false;

    void Awake()
    {
        if (isLevelOne)
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                PlayerPrefs.DeleteKey("Coins");
            }
            coins = Random.Range(minCoins, maxCoins + 1);
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
        }
    }
}
