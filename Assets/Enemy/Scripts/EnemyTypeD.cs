using UnityEngine;

public class EnemyTypeD : Enemy
{
    protected override void Move()
    {
        base.Move();


        Vector2 movementDirection = Vector2.zero;

        switch (spawnDirection)
        {
            case SpawnDirection.Top:
                movementDirection = new Vector2(-1, -1).normalized;
                break;
            case SpawnDirection.Right:
                movementDirection = new Vector2(-1, 1).normalized;
                break;
            case SpawnDirection.Bottom:
                movementDirection = new Vector2(1, 1).normalized;
                break;
            case SpawnDirection.Left:
                movementDirection = new Vector2(1, -1).normalized;
                break;
        }

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        CheckOutOfBounds();
    }

    public void AdjustSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;

        Vector2 middleOfPlayArea = (playAreaClamp.minClamp + playAreaClamp.maxClamp) / 2f;

        switch (spawnDirection)
        {
            case SpawnDirection.Top:
                spawnPosition.x = Random.Range(middleOfPlayArea.x, playAreaClamp.maxClamp.x);
                spawnPosition.y = Random.Range(middleOfPlayArea.y, playAreaClamp.maxClamp.y);
                break;
            case SpawnDirection.Right:
                spawnPosition.x = Random.Range(middleOfPlayArea.x, playAreaClamp.maxClamp.x);
                spawnPosition.y = Random.Range(playAreaClamp.minClamp.y, middleOfPlayArea.y);
                break;
            case SpawnDirection.Bottom:
                spawnPosition.x = Random.Range(playAreaClamp.minClamp.x, middleOfPlayArea.x);
                spawnPosition.y = Random.Range(playAreaClamp.minClamp.y, middleOfPlayArea.y);
                break;
            case SpawnDirection.Left:
                spawnPosition.x = Random.Range(playAreaClamp.minClamp.x, middleOfPlayArea.x);
                spawnPosition.y = Random.Range(middleOfPlayArea.y, playAreaClamp.maxClamp.y);
                break;
        }

        spawnPosition = NearestPositionOutsideClamp(spawnPosition);
        transform.position = spawnPosition;
    }

    private Vector2 NearestPositionOutsideClamp(Vector2 position)
    {
        // Calculate the distance between the position and the clamp edges.
        float distanceToLeft = position.x - playAreaClamp.minClamp.x;
        float distanceToRight = playAreaClamp.maxClamp.x - position.x;
        float distanceToTop = position.y - playAreaClamp.minClamp.y;
        float distanceToBottom = playAreaClamp.maxClamp.y - position.y;
        
        float smallestDistance = Mathf.Min(distanceToLeft, distanceToRight, distanceToTop, distanceToBottom);

        // Check which variable is the smallest
        if (smallestDistance == distanceToLeft)
        {
            position.x = playAreaClamp.minClamp.x - 2;
        }
        else if (smallestDistance == distanceToRight)
        {
            position.x = playAreaClamp.maxClamp.x + 2;
        }
        else if (smallestDistance == distanceToTop)
        {
            position.y = playAreaClamp.minClamp.y - 2;
        }
        else
        {
            position.y = playAreaClamp.maxClamp.y + 2;
        }

        return position;
    }
}