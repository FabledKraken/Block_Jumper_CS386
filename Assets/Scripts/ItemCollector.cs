using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour, IDataPersistence
{
    private int strawberryCollected = 0;
    private List<GameObject> strawberries = new List<GameObject>();
    public ItemCollector instance;

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private int currentLevel;

    public void LoadData(GameData data)
    {
        strawberries.Clear();

        foreach (var position in data.destroyedStrawberryPositions)
        {
            GameObject strawberry = Instantiate(Resources.Load<GameObject>("StrawberryPrefab"), position, Quaternion.identity);
            strawberries.Add(strawberry);
        }

        currentLevel = data.activeScene;

        if (data.levelScores.ContainsKey(currentLevel))
        {
            strawberryCollected = data.levelScores[currentLevel];
            UpdatePointsText();
        }
    }

    public void SaveData(ref GameData data)
    {
        data.strawberryCollected = strawberryCollected;
        data.destroyedStrawberryPositions = strawberries.ConvertAll(strawberry => strawberry.transform.position);

        if (!data.levelScores.ContainsKey(currentLevel))
        {
            data.levelScores.Add(currentLevel, strawberryCollected);
        }
        else
        {
            data.levelScores[currentLevel] = strawberryCollected;
        }

        data.totalPoints += strawberryCollected; // Accumulate total points
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Strawberry"))
        {
            collectSoundEffect.Play();
            Destroy(col.gameObject);
            strawberryCollected++;
            UpdatePointsText();
        }
    }
    
    public int GetTotalPoints()
    {
        return strawberryCollected;
    }

    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + strawberryCollected;
    }
}