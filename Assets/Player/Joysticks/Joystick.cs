using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Transform background; // Reference to the background image.
    [SerializeField] private Transform foreground; // Reference to the foreground image.
    [SerializeField] private float moveSpeed = 5f; // Adjust the move speed as needed.

    private Vector3 inputDirection;
    private Vector3 originalPosition;

    private void Start()
    {
        // Store the original position of the foreground joystick.
        originalPosition = foreground.position;
    }

    private void Update()
    {
        // Get the joystick input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the input direction
        inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Limit the movement to the area of the background image
        float radius = background.localScale.x / 2f; // Adjust for the size of your background image

        // Clamp the input direction within the radius
        inputDirection = Vector3.ClampMagnitude(inputDirection, radius);

        // Move the foreground joystick based on input
        foreground.position = background.position + inputDirection * radius;

        // Move the player or simulate mouse cursor direction
        // (Same as previous code)
        // ...

        // If no input, snap the foreground joystick back to the original position
        if (horizontalInput == 0f && verticalInput == 0f)
        {
            foreground.position = originalPosition;
        }
    }
}
