using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalPointsText;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming you have a reference to the DataPersistenceManager
        int totalPoints = DataPersistenceManager.Instance.getPoints();

        // Display total points in the UI
        totalPointsText.text = "Final Score: " + totalPoints;
    }
}