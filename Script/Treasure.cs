using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Audio clip to play when treasure is collected
    public AudioClip collectSound;

    void Start()
    {
        // Spawn treasure in a random position using GameManager
        if (GameManager.instance != null)
        {
            transform.position = GameManager.instance.GetRandomPosition();
        }
        else
        {
            Debug.LogError("GameManager NOT found in scene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player touches the treasure
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Play sound at treasure position (no AudioSource needed)
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position, 1.5f);
            }

            // Notify GameManager who won
            if (GameManager.instance != null)
            {
                GameManager.instance.PlayerWins(other.tag);
            }

            // Disable collider to avoid multiple triggers
            GetComponent<Collider>().enabled = false;
        }
    }
}