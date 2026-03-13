using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // NEW: Import this

public class PlayerMovement : MonoBehaviour
{
    public int Coin;
    public int health = 100;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Animator _animator;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private LayerMask groundLayer;
    private SpriteRenderer spriteRenderer;
    bool isGrounded()
    {
        return GetComponent<Rigidbody2D>().linearVelocity.y == 0;
    }




    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {


        float horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalInput = -1f;
                _animator.SetBool("isRunning", true);

            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {

                horizontalInput = 1f;
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
            
        }
        if (Keyboard.current == null)
        {
            _animator.SetBool("isRunning", false);
        }


            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);


        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded())
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            }
            else
            {

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 25;
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());
            if (health <= 0)
            {
                Die();
            }
        }
    }
    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}