using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesContinue : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
