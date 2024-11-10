using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private List<Vector3> _pathPositions;
    private int currentTargetIndex = 0;
    private Vector3 lastDirection = Vector3.zero;

    public void SetPath(List<Vector3> path)
    {
        _pathPositions = new List<Vector3>(path);
        currentTargetIndex = 0;
    }

    private void Update()
    {
        if (_pathPositions == null || currentTargetIndex >= _pathPositions.Count)
        {
            if (lastDirection != Vector3.zero)
            {
                transform.position += lastDirection * (speed * Time.deltaTime);
            }
            return;
        }
            

        // Move towards the current target position
        Vector3 targetPosition = _pathPositions[currentTargetIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Update last direction when moving
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            lastDirection = direction;
        }

        // Check if the enemy reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentTargetIndex++;
        }
    }
}
