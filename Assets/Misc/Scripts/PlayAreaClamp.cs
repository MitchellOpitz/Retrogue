using UnityEngine;

public class PlayAreaClamp : MonoBehaviour
{
    public Vector2 minClamp = new Vector2(-5f, -5f); // Minimum X and Y values
    public Vector2 maxClamp = new Vector2(5f, 5f);   // Maximum X and Y values
    public bool showClampArea = true; // Show the clamp area in the Scene view for testing

    private void OnDrawGizmos()
    {
        if (showClampArea)
        {
            // Draw a wire cube in the Scene view to visualize the clamp area
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3((minClamp.x + maxClamp.x) / 2f, (minClamp.y + maxClamp.y) / 2f, 0f);
            Vector3 size = new Vector3(maxClamp.x - minClamp.x, maxClamp.y - minClamp.y, 0f);
            Gizmos.DrawWireCube(center, size);
        }
    }

    public Vector3 ClampPosition(Vector3 position)
    {
        // Clamp the position within the specified range
        float clampedX = Mathf.Clamp(position.x, minClamp.x, maxClamp.x);
        float clampedY = Mathf.Clamp(position.y, minClamp.y, maxClamp.y);
        return new Vector3(clampedX, clampedY, position.z);
    }
}
