using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayAreaClamp playAreaClamp; // Reference to the PlayAreaClamp script

    private float _playerHalfWidth;
    private float _playerHalfHeight;

    private void Start()
    {
        // Calculate half of the player's width and height
        _playerHalfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _playerHalfHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2f;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        if (playAreaClamp != null)
        {
            float clampedX = Mathf.Clamp(newPosition.x,
                                         playAreaClamp.minClamp.x + _playerHalfWidth,
                                         playAreaClamp.maxClamp.x - _playerHalfWidth);

            float clampedY = Mathf.Clamp(newPosition.y,
                                         playAreaClamp.minClamp.y + _playerHalfHeight,
                                         playAreaClamp.maxClamp.y - _playerHalfHeight);

            newPosition = new Vector3(clampedX, clampedY, newPosition.z);
        }

        transform.position = newPosition;
    }
}
