using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector3 direction, float force)
    {
        moveDirection = direction;
        speed = force;

        if (animator != null)
            animator.SetTrigger("Arrow");
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerDeath(other.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandlePlayerDeath(Vector3 deathPosition)
    {
        PlayerDeathHandler handler = PlayerDeathHandler.Instance;
        if (handler != null)
        {
            handler.Die(deathPosition, false);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}