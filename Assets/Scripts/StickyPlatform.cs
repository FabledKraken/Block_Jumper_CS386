using UnityEngine;

public class StickyPlatform : MonoBehaviour, IDataPersistence
{
    // Trigger used so that the player does not get dragged from the sides of the platform
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(transform);
        }
    }
    
    // So the player can jump from the platform
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }  
    }
    
    public void LoadData(GameData data)
    {
        transform.position = data.platformPos;
    }

    public void SaveData(ref GameData data)
    {
        data.platformPos = transform.position;
    }
}
