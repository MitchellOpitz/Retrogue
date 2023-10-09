using UnityEngine;

public class RightJoystick : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f; // Adjust the sensitivity as needed.

    private Vector2 inputDirection;

    private void Update()
    {
        // Get the joystick input
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        // Calculate the input direction
        inputDirection = new Vector2(horizontalInput, verticalInput);

        // Rotate the right joystick GameObject to simulate mouse cursor direction
        Vector3 rotationEuler = transform.eulerAngles;
        rotationEuler.x += inputDirection.y * sensitivity;
        rotationEuler.y += inputDirection.x * sensitivity;
        transform.eulerAngles = rotationEuler;
    }
}
