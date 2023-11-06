using UnityEngine;

public class EndMenu : MonoBehaviour
{
    // Simply will quit the running Application when the "QUIT" button is pressed
    public void Quit()
    {
        Debug.Log("QUIT APP CALLED");
        Application.Quit();
    }
}
