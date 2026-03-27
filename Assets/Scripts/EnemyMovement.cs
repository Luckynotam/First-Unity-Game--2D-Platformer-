using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;

    private int currentDirection;

    private float halfWidth; 




     private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.linearVelocity = Vector2.right * speed * currentDirection;

    }
}
