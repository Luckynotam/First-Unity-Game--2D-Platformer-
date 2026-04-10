using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1.75f;
    [SerializeField] private int startDirection = 1;

    private int currentDirection;

    private float halfWidth;

    private Vector2 movement; 




     private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;
        spriteRenderer.flipX =  startDirection == 1 ? false : true;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = speed * currentDirection;
        movement.y = rigidBody.linearVelocityY;
        rigidBody.linearVelocity = movement;
        


    }

    private void SetDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && 
            rigidBody.linearVelocity.x > 0)
        {
            currentDirection *= -1;
            spriteRenderer.flipX = true; 

        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && 
            rigidBody.linearVelocityX < 0)
        {
            currentDirection *= -1;
        }
            Debug.DrawRay(transform.position, Vector2.right * (halfWidth * 0.1f), Color.red);
            Debug.DrawRay(transform.position, Vector2.left * (halfWidth * 0.1f), Color.red);

    }   
}
