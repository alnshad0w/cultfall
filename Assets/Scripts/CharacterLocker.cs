using UnityEngine;

public class CharacterLocker : MonoBehaviour
{
    private const string YUKI_KEY = "Yuki";
    private const string REI_KEY = "Rei";
    private const string KYOKO_KEY = "Kyoko";
    private const string SHUU_KEY = "Shuu";

    private const string GAMES_KEY = "Games played";

    private void Start()
    {
        InitializeDefaultLocks();
    }

    private void InitializeDefaultLocks()
    {
        if (!PlayerPrefs.HasKey(YUKI_KEY))
            PlayerPrefs.SetInt(YUKI_KEY, 1);

        if (!PlayerPrefs.HasKey(REI_KEY))
            PlayerPrefs.SetInt(REI_KEY, 0);

        if (!PlayerPrefs.HasKey(KYOKO_KEY))
            PlayerPrefs.SetInt(KYOKO_KEY, 0);

        if (!PlayerPrefs.HasKey(SHUU_KEY))
            PlayerPrefs.SetInt(SHUU_KEY, 0);

        PlayerPrefs.Save();
    }

    public void UnlockCharacter(int characterIndex)
    {
        switch (characterIndex)
        {
            case 1: PlayerPrefs.SetInt(YUKI_KEY, 1); break;
            case 2: PlayerPrefs.SetInt(REI_KEY, 1); break;
            case 3: PlayerPrefs.SetInt(KYOKO_KEY, 1); break;
            case 4: PlayerPrefs.SetInt(SHUU_KEY , 1); break;
        }

        PlayerPrefs.Save();
    }

    public bool IsCharacterUnlocked(int characterIndex)
    {
        return characterIndex switch
        {
            1 => PlayerPrefs.GetInt(YUKI_KEY, 0) == 1,
            2 => PlayerPrefs.GetInt(REI_KEY, 0) == 1,
            3 => PlayerPrefs.GetInt(KYOKO_KEY, 0) == 1,
            4 => PlayerPrefs.GetInt(SHUU_KEY, 0) == 1,
            _ => false
        };
    }

    public void SaveProgress()
    {
        if (!PlayerPrefs.HasKey(GAMES_KEY))
        {
            PlayerPrefs.SetInt(GAMES_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(GAMES_KEY, PlayerPrefs.GetInt(GAMES_KEY) + 1);
        }

        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt(GAMES_KEY) == 1)
        {
            UnlockCharacter(3);
        }
        else if (PlayerPrefs.GetInt(GAMES_KEY) == 2)
        {
            UnlockCharacter(4);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(YUKI_KEY);
        PlayerPrefs.DeleteKey(REI_KEY);
        PlayerPrefs.DeleteKey(KYOKO_KEY);
        PlayerPrefs.DeleteKey(SHUU_KEY);
        InitializeDefaultLocks(); 
    }
}
