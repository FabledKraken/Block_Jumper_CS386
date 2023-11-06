using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int strawberryCollected;
    public int activeScene;
    public Vector3 playerPos;
    public Vector3 platformPos;
    public Dictionary<string, bool> berryColl;
    

    // This will load the game with default values when there is no load data
    public GameData()
    {
        strawberryCollected = 0;
        activeScene = 1;
        playerPos = Vector3.zero;
        platformPos = new Vector3(4f, 2f, 19f);
        berryColl = new Dictionary<string, bool>();
        
    }

}
