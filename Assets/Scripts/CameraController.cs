using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player object
    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update()
    {
        // grabs the cameras transform to get the value of z, uses the players transform for x and y to follow player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
