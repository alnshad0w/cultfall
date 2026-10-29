using UnityEngine;

[System.Serializable]
public class FoodItem 
{
    public int ID;
    public Sprite Image;
    public string[] Name;
    public string Description;
    public int Price;
    public int HP;
    public int Type; //0 - vending machine, 1 - ice cream, 2 - tea

    public string GetLocalizedName(int languageIndex)
    {
        if (languageIndex >= 0 && languageIndex < Name.Length)
            return Name[languageIndex];
        return Name[0]; 
    }
}
