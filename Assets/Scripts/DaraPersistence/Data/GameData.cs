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
    public List<Vector3> destroyedStrawberryPositions;
    public Dictionary<int, int> levelScores;
    public int playerLives;
    public int totalPoints;

    public GameData()
    {
        strawberryCollected = 0;
        activeScene = 1;
        playerPos = Vector3.zero;
        platformPos = new Vector3(4f, 2f, 19f);
        destroyedStrawberryPositions = new List<Vector3>();
        levelScores = new Dictionary<int, int>();
        playerLives = 3;
        totalPoints = 0;
    }
}

