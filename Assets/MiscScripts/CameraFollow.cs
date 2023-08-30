using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform

    private void Update()
    {
        if (target != null)
        {
            // Keep the camera's Z position constant and directly set its X and Y positions to match the player's position
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
