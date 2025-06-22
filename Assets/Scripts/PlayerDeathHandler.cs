using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public static PlayerDeathHandler Instance;

    public bool useCorpseMechanic = false;
    public GameObject corpsePrefab;

    private Vector3 startPosition;
    private PlayerMovement playerMovement;
    private SpriteRenderer playerSprite;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private FollowPlayer cameraFollow;
    private float originalGravityScale;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        startPosition = transform.position;
        playerMovement = GetComponent<PlayerMovement>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalGravityScale = rb.gravityScale;

        cameraFollow = Camera.main.GetComponent<FollowPlayer>();
    }

    public void Die(Vector3 deathPosition, bool spawnCorpse)
    {
        if (spawnCorpse && useCorpseMechanic)
        {
            Instantiate(corpsePrefab, deathPosition, Quaternion.Euler(0, 0, 90));
        }

        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        playerMovement.enabled = false;
        playerSprite.enabled = false;
        playerCollider.enabled = false;

        if (cameraFollow != null)
        {
            cameraFollow.SetFollowing(false);
        }

        Invoke(nameof(Respawn), 0.5f);
    }

    private void Respawn()
    {
        transform.position = startPosition;

        rb.gravityScale = originalGravityScale;

        playerMovement.enabled = true;
        playerSprite.enabled = true;
        playerCollider.enabled = true;

        if (cameraFollow != null)
        {
            cameraFollow.SetFollowing(true);
        }
    }
}