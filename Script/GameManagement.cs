
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton instance (global access)
    public static GameManager instance;

    // ---------------- UI ----------------
    public TextMeshProUGUI winText;     // Text that shows winner
    public TextMeshProUGUI timerText;   // Timer UI text
    public GameObject winPanel;         // Background panel when someone wins

    // ---------------- AREA ----------------
    public Transform area; // Plane where treasure can spawn

    // ---------------- TIMER ----------------
    private bool gameEnded = false; // Prevents multiple wins
    private float timer = 0f;       // Game timer

    void Awake()
    {
        // Assign singleton instance
        instance = this;
        Debug.Log("GameManager created");
    }

    void Start()
    {
        // Hide win UI at the start
        if (winPanel != null)
            winPanel.SetActive(false);

        if (winText != null)
            winText.text = "";
    }

    void Update()
    {
        // Stop timer if game has ended
        if (gameEnded) return;

        // Increase timer
        timer += Time.deltaTime;

        // Update timer UI in MM:SS format
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // ---------------- WIN SYSTEM ----------------
    public void PlayerWins(string playerName)
    {
        if (gameEnded) return;

        gameEnded = true;

        Debug.Log("PLAYER WINS FUNCTION CALLED");

        // Show win panel
        if (winPanel != null)
            winPanel.SetActive(true);

        // Format final time
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // Show winner text
        if (winText != null)
        {
            winText.text = "YOU WIN\n" +
                           playerName +
                           "\nTime: " +
                           string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("winText NOT assigned in inspector");
        }

        // Restart scene after delay
        StartCoroutine(RestartGame());
    }

    // ---------------- RESTART ----------------
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ---------------- RANDOM POSITION ----------------
    public Vector3 GetRandomPosition()
    {
        // Get center of the area (plane)
        Vector3 center = area.position;

        // Calculate size based on scale
        float sizeX = area.localScale.x * 5f;
        float sizeZ = area.localScale.z * 5f;

        // Generate random position inside the plane
        float x = Random.Range(center.x - sizeX, center.x + sizeX);
        float z = Random.Range(center.z - sizeZ, center.z + sizeZ);

        // Return position slightly above ground
        return new Vector3(x, 1f, z);
    }
}