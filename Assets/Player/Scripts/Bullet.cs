using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;       // Speed of the bullet
    public float destroyDistance = 5f; // Distance at which the bullet should be destroyed
    public float bufferDistance = 5f; // Set the buffer distance
    
    private int damage;
    private float damageMultiplier;

    private PlayerManager playerManager;
    private PlayAreaClamp clamp; // Reference to the PlayAreaClamp script

    private void Start()
    {
        // Find the PlayAreaClamp script in the scene
        clamp = FindObjectOfType<PlayAreaClamp>();
        playerManager = FindObjectOfType<PlayerManager>();
        damage = Mathf.RoundToInt(playerManager.baseDamage * (1 + playerManager.damageMultiplier));
    }

    private void Update()
    {
        MoveBullet();
        CheckOutOfBounds();
    }

    private void MoveBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        Vector3 bulletPosition = transform.position;
        Vector2 minClamp = clamp.minClamp;
        Vector2 maxClamp = clamp.maxClamp;

        if (bulletPosition.x < minClamp.x - bufferDistance || bulletPosition.x > maxClamp.x + bufferDistance ||
            bulletPosition.y < minClamp.y - bufferDistance || bulletPosition.y > maxClamp.y + bufferDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
