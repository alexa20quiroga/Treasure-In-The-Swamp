using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Load game scene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Exit game
    public void QuitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}