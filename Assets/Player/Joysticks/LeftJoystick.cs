using UnityEngine;

public class LeftJoystick : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjust the move speed as needed.

    private Vector3 inputDirection;

    private void Update()
    {
        // Get the joystick input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player based on input
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }
}
