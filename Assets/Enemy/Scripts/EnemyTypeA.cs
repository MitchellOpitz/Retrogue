using UnityEngine;

public class EnemyTypeA : Enemy
{
    protected override void Move()
    {
        base.Move();
        Vector2 movementDirection = Vector2.zero;

        switch (spawnDirection)
        {
            case SpawnDirection.Top:
                movementDirection = Vector2.down;
                break;
            case SpawnDirection.Bottom:
                movementDirection = Vector2.up;
                break;
            case SpawnDirection.Left:
                movementDirection = Vector2.right;
                break;
            case SpawnDirection.Right:
                movementDirection = Vector2.left;
                break;
        }

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        CheckOutOfBounds();
    }
}
