using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool levelCompleted = false;

    [SerializeField] private AudioSource endSoundEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            endSoundEffect.Play();
            Invoke("CompleteLevel", .75f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
