using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour
{
    private const int LANG_AMOUNT = 2;

    [SerializeField] private int langIndex;
    [SerializeField] private string[] text;

    private Text textBlock;
    private TextMeshProUGUI textBlockTMP;

    private void Start()
    {
        Debug.Log($"language index: {PlayerPrefs.GetInt("Language")}");
        langIndex = PlayerPrefs.GetInt("Language", langIndex);

        UpdateText();
    }

    public void SwapLanguage()
    {
        langIndex = (langIndex + 1) % LANG_AMOUNT;

        PlayerPrefs.SetInt("Language", langIndex);
        PlayerPrefs.Save();

        UpdateText();
    }

    private void UpdateText()
    {
        if (textBlock == null)
            textBlock = GetComponent<Text>();
        if (textBlockTMP == null)
            textBlockTMP = GetComponent<TextMeshProUGUI>();

        if (textBlock != null)
        {
            textBlock.text = text[langIndex];
        }
        else if (textBlockTMP != null)
        {
            textBlockTMP.text = text[langIndex];
        }
    }
}
