using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int strawberryCollected = 0; // used to keep track of points
    
    [SerializeField] private TextMeshProUGUI pointsText; // text for the points 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Strawberry"))
        {
            // if strawberry is collected accumulate proper points and destroy the strawberry object
            strawberryCollected++;
            Debug.Log("Strawberries: " + strawberryCollected);
            Destroy(col.gameObject);
            pointsText.text = "Points: " + strawberryCollected;
        }
    }
}
