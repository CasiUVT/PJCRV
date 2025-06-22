using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerDeath(other.transform.position);
        }
    }

    private void HandlePlayerDeath(Vector3 deathPosition)
    {
        PlayerDeathHandler handler = PlayerDeathHandler.Instance;
        if (handler != null)
        {
            handler.Die(deathPosition, true);
        }
        else
        {
            // Fallback for levels without handler
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}