using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;  // used for animations
    private Rigidbody2D rb; // used to control rigidbody2d of player
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rigidbody2d of player
        anim = GetComponent<Animator>();  // animations of strawberries
    }

    // Handles the traps and what happens to player when hit
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    // Function for handling what happens on death
    private void Die()
    {
        Debug.Log("Player Died");
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static; // change player to not be interactable anymore
    }

    // Used to restart the level once the player dies
    private void RestartLevel()
    {
        // gets the active scene name and load (restarts) the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
