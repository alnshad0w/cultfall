using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChanger : MonoBehaviour
{
    public GameObject[] Characters;

    [SerializeField] private Image lockImage;
    [SerializeField] private Button choiceButton;
    [SerializeField] private TextMeshProUGUI choiceText;

    private CharacterLocker locker;
    private int currentIndex = 0;

    private void Start()
    {
        locker = GetComponent<CharacterLocker>();
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex + 1) % Characters.Length;
        UpdateCharacterDisplay();
    }

    public void ShowPrevious()
    {
        currentIndex = (currentIndex - 1 + Characters.Length) % Characters.Length;
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            bool isActive = (i == currentIndex);
            Characters[i].SetActive(isActive);
        }

        bool isUnlocked = locker.IsCharacterUnlocked(currentIndex + 1);

        lockImage.gameObject.SetActive(!isUnlocked);
        choiceButton.interactable = isUnlocked;
        
        if (!isUnlocked)
        {
            ChangeButtonText("Заблокировано", "Locked");
        }
        else
        {
            ChangeButtonText("Выбрать", "Confirm");
        }
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("MC", currentIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved MC " + currentIndex);
    }

    private void ChangeButtonText(string ru, string eng)
    {
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                choiceText.text = ru;
                break;
            case 1:
                choiceText.text = eng;
                break;
        }
    }
}
