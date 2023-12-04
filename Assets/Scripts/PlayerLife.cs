using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLife : MonoBehaviour, IDataPersistence
{
    private Animator anim;
    private Rigidbody2D rb;
    
    private bool hasCollided = false;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource deathSoundEffectHelper;
    [SerializeField] private TextMeshProUGUI livesText; // Reference to the UI Text for lives display

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + PlayerLivesManager.Instance.GetLives();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!hasCollided && col.gameObject.CompareTag("Trap"))
        {
            hasCollided = true; // Set the flag to true to avoid multiple collisions in the same frame
            Die();
        }
    }

    private void Die()
    {
        int remainingLives = PlayerLivesManager.Instance.GetLives();
        
        Debug.Log("Lives: " + remainingLives);

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            remainingLives--;
        }
        
        
        Debug.Log("Lives: " + remainingLives);
        PlayerLivesManager.Instance.SetLives(remainingLives);

        UpdateLivesText();
        Debug.Log("Player Died. Lives remaining: " + remainingLives);

        deathSoundEffect.Play();
        deathSoundEffectHelper.Play();
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;

        if(remainingLives > 0)
        {
            Invoke("RestartLevel", 1f);
        }
        else
        {
            SceneManager.LoadScene(7);
        }
}

    private void RestartLevel()
    {
        Debug.Log("Restarting level");
        hasCollided = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loading Data");
        
        transform.position = data.playerPos;
        UpdateLivesText(); // Update lives text when loading data
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving Data");
        // Save total points from ItemCollector
        Finish finish = FindObjectOfType<Finish>();
        if (finish != null)
        {
            data.totalPoints += finish.getPoints();
        }

        data.playerPos = transform.position;
        data.playerLives = PlayerLivesManager.Instance.GetLives();
        data.activeScene = SceneManager.GetActiveScene().buildIndex;
    }
}
