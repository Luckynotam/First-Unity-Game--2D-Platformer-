using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1.75f;
    [SerializeField] private int startDirection = 1;
    [SerializeField] private Animator EnemyController;
    [SerializeField] public Collider2D EnemyColider;
    [SerializeField] private SemaphoreSlim transParencyDuration = new SemaphoreSlim(0, 1);
    private int currentDirection;

    private float halfWidth;

    private Vector2 movement; 




     private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;
        spriteRenderer.flipX = true;

        


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetDirection();
        movement.x = speed * currentDirection;
        movement.y = rigidBody.linearVelocityY;
        rigidBody.linearVelocity = movement;
        if (currentDirection == 1 || currentDirection == -1)
        {
            EnemyController.SetBool("enemyIsRunning", true);
        }
        


    }

    private void SetDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth +0.2f, LayerMask.GetMask("Ground")) && 
            currentDirection == 1)
        {
            currentDirection *= -1;
            spriteRenderer.flipX = startDirection == 1 ? false : true;

        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.2f, LayerMask.GetMask("Ground")) && 
            currentDirection == -1)
        {
            currentDirection *= -1;
            spriteRenderer.flipX = startDirection == 1 ? true : false;
        }

        if (currentDirection == 1)
            Debug.DrawRay(transform.position, Vector2.right * (halfWidth * 0.2f), Color.red);
        if (currentDirection == -1)
            Debug.DrawRay(transform.position, Vector2.left * (halfWidth * 0.2f), Color.red);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyColider.enabled = false;
        }
    }
}
