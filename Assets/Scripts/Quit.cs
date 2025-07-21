using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public Button quitButton; // Assign your button in the Inspector

    void Start()
    {
        // Add the QuitGame function to the button's onClick event
        quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // Log to the console that the quit button was pressed
        Debug.Log("Quit button pressed");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        Application.Quit(); // Quit the application in the build
#endif
    }
}