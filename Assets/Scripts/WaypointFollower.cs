using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        // Ensuring there is distance between waypoints
        if (Vector2.Distance(waypoints[currWaypointIndex].transform.position,
                transform.position) < .1f)
        {
            // updating the index and wrapping if at the end (circular array)
            currWaypointIndex++;
            if (currWaypointIndex >= waypoints.Length)
            {
                currWaypointIndex = 0;
            }
        }

        // Move the platform frame by frame towards the waypoint(current position, pos moving towards, speed move)
        // Using deltaTime so that we are not dependant on the frames of the system and movement is the same on all
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[currWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
