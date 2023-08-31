using UnityEngine;

public class EnemyTypeB : Enemy
{
    public float sineFrequency = 1f; // Frequency of the sine wave
    public float sineAmplitude = 1f; // Amplitude of the sine wave

    protected override void Move()
    {
        base.Move();

        Vector2 linearMovement = Vector2.zero;
        Vector2 sineMovement = Vector2.zero;

        switch (spawnDirection)
        {
            case SpawnDirection.Top:
                linearMovement = Vector2.down;
                sineMovement = Vector2.right;
                break;
            case SpawnDirection.Bottom:
                linearMovement = Vector2.up;
                sineMovement = Vector2.left;
                break;
            case SpawnDirection.Left:
                linearMovement = Vector2.right;
                sineMovement = Vector2.up;
                break;
            case SpawnDirection.Right:
                linearMovement = Vector2.left;
                sineMovement = Vector2.down;
                break;
        }

        // Calculate the sine wave movement
        float sineOffset = Mathf.Sin(Time.time * sineFrequency) * sineAmplitude;
        Vector2 totalMovement = linearMovement * moveSpeed * Time.deltaTime + sineMovement * sineOffset;

        transform.Translate(totalMovement);
        CheckOutOfBounds();
    }
}
