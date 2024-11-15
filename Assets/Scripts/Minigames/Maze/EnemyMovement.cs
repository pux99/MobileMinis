using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private List<Vector3> _pathPositions;
    private int currentTargetIndex = 0;

    public void SetPath(List<Vector3> path)
    {
        _pathPositions = new List<Vector3>(path);
        currentTargetIndex = 0;
    }

    private void Update()
    {
        if (_pathPositions == null || currentTargetIndex >= _pathPositions.Count)
        {
            return;
        }
        
        // Move towards the current target position
        Vector3 targetPosition = _pathPositions[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        

        // Check if the enemy reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentTargetIndex++;
        }
    }
    
}
