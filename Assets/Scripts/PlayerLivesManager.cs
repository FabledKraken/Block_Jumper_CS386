using UnityEngine;

public class PlayerLivesManager : MonoBehaviour
{
    private int lives = 3;

    public static PlayerLivesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLives();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLives()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            lives = PlayerPrefs.GetInt("Lives");
        }
        else
        {
            SaveLives();
        }
    }

    private void SaveLives()
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.Save();
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int value)
    {
        lives = value;
        SaveLives();
    }
}