using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera cam;
    private Rigidbody2D rb;
    private float rad = .15f;

    [SerializeField] private float speed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) + offset;
            
            Vector2 direction = (mousePosition - transform.position).normalized;
            float moveDistance = Vector2.Distance(transform.position, mousePosition);
            
            if (CanMoveTowards(direction, moveDistance))
            {
                rb.MovePosition(rb.position + direction * Mathf.Min(moveDistance, speed));
            }
            Debug.DrawRay(transform.position, direction * moveDistance, Color.black);
        }
    }
    private bool CanMoveTowards(Vector2 direction, float distance)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, rad, direction, distance, LayerMask.GetMask("Wall"));
            return hit.collider == null;
        }
}