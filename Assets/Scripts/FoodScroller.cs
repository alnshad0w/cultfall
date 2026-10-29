using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class FoodScroller : MonoBehaviour
{
    [SerializeField] protected GameObject foodItemPrefab;
    [SerializeField] protected Transform contentPanel;
    [SerializeField] protected List<FoodItem> foodList;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private int foodType;

    private void Start()
    {
        if (InventoryManager.Instance != null)
        {
            foodList = InventoryManager.Instance.AllItems.Where(f => f.Type == foodType).ToList();
        }
        PopulateScrollView();
    }

    protected void PopulateScrollView()
    {
        foreach (Transform child in contentPanel.transform) 
        {
            Destroy(child.gameObject);
        }

        foreach (FoodItem item in foodList)
        {
            CreateFoodCard(item);
        }

        UpdateCoinsText();  
    }

    protected virtual void CreateFoodCard(FoodItem item)
    {
        GameObject newItem = Instantiate(foodItemPrefab, contentPanel);

        newItem.transform.Find("image").GetComponent<Image>().sprite = item.Image;
        newItem.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.GetLocalizedName(PlayerPrefs.GetInt("Language", 0));
        newItem.transform.Find("info").GetComponent<TextMeshProUGUI>().text = item.Description;

        var price = newItem.transform.Find("price")?.GetComponent<TextMeshProUGUI>();
        if (price != null)
        {
            price.text = item.Price.ToString();
        }
        var price1 = newItem.transform.Find("BuyButton/price")?.GetComponent<TextMeshProUGUI>();
        if (price1 != null)
        {
            price1.text = item.Price.ToString();
        }

        Debug.Log("Vending machine: " + price);
        Debug.Log("Ice cream: " + price1);


        Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
        if (buyButton != null)
        {
            TextMeshProUGUI buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null && buttonText.gameObject.name != "price")
            {
                string[] buttonTexts = { "Купить", "Buy" };
                buttonText.text = buttonTexts[PlayerPrefs.GetInt("Language", 0)];
            }
        }

        buyButton.onClick.AddListener(() => ActionForItem(item));
    }

    protected virtual bool ActionForItem(FoodItem item) //покупка еды
    {
        if (PlayerPrefs.GetInt("Coins") >= item.Price)
        {
            int currentCoins = PlayerPrefs.GetInt("Coins");
            currentCoins -= item.Price;
            PlayerPrefs.SetInt("Coins", currentCoins);

            InventoryManager.Instance.AddItem(item);

            UpdateCoinsText();
        }
        else
        {
            Debug.Log("Недостаточно монет!");
        }

        return true;
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }

        Debug.Log(coinsText);
    }
}
