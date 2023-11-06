using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour, IDataPersistence
{
    private int strawberryCollected = 0; // used to keep track of points

    [SerializeField] private TextMeshProUGUI pointsText; // text for the points 
    [SerializeField] private AudioSource collectSoundEffect;

    public void LoadData(GameData data)
    {
        strawberryCollected = data.strawberryCollected;
        pointsText.text = "Points: " + strawberryCollected;
    }

    public void SaveData(ref GameData data)
    {
        data.strawberryCollected = strawberryCollected;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Strawberry"))
        {
            // if strawberry is collected accumulate proper points and destroy the strawberry object
            strawberryCollected++;
            Debug.Log("Strawberries: " + strawberryCollected);
            Destroy(col.gameObject);
            pointsText.text = "Points: " + strawberryCollected;
            collectSoundEffect.Play();
        }
    }
}
