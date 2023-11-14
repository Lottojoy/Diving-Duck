using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public float jumpForce;
    public float dashForce;

    public static int currentHp = 6;
    public static int maxHp = 6;

    private Vector3 direction;
    Rigidbody rb;

    bool moveLeft;
    bool moveRight;
    bool canJump = true;
    bool canDash = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            moveRight = false;
            direction = Vector3.left * strength;
            moveLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveLeft = false;
            direction = Vector3.right * strength;
            moveRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpCooldown());
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && moveLeft && canDash)
        {
            rb.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
            StartCoroutine(DashCooldown());
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && moveRight && canDash)
        {
            rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
            StartCoroutine(DashCooldown());
        }


        // Apply gravity and update the position
        //direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().HitObstruction();
            rb.AddForce(transform.up * 3f);
        }
        else if (collision.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(3);
        canJump = true;
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(5);
        canDash = true;
    }
}
