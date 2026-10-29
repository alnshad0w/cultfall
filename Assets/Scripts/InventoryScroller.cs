using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScroller : FoodScroller
{
    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Awake()
    {
        if (contentPanel == null)
        {
            contentPanel = GameObject.Find("Content")?.transform;
        }
    }

    public void InitializeInventory()
    {
        foodList = InventoryManager.Instance.Inventory;
        PopulateScrollView();
    }

    protected override void CreateFoodCard(FoodItem item)
    {
        GameObject newItem = Instantiate(foodItemPrefab, contentPanel);

        newItem.transform.Find("image").GetComponent<Image>().sprite = item.Image;
        newItem.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.GetLocalizedName(PlayerPrefs.GetInt("Language", 0));
        newItem.transform.Find("info").GetComponent<TextMeshProUGUI>().text = item.Description;
        
        Destroy(newItem.transform.Find("price").gameObject);
        Destroy(newItem.transform.Find("money").gameObject);

        Button applyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
        if (applyButton != null)
        {
            TextMeshProUGUI buttonText = applyButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                string[] buttonTexts = { "ѕрименить", "Apply" };
                buttonText.text = buttonTexts[PlayerPrefs.GetInt("Language", 0)];
            }
        }

        applyButton.onClick.AddListener(() =>
        {
            bool isApplied = ActionForItem(item);

            if (isApplied)
            {
                Destroy(newItem.gameObject);
                InventoryManager.Instance.RemoveItem(item);
            }
        });
    }

    protected override bool ActionForItem(FoodItem item) //прибавка к здоровью игрока
    {
        if (player.Health < player.MaxHealth)
        {
            player.Health += item.HP;

            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }

            return true;
        }

        return false;
    }

}
