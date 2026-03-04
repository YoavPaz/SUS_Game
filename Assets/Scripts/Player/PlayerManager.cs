using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    public Rigidbody2D rb;
    [Header("Movement")]
    public float speed = 5f;
    public static float speedM = 1f; //speed multiplyer for effects and stuff

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;

    private Vector2 moveInput;
    public Animator animator;

    public PlayerSaveData playerSaveData;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerSaveData = SaveManager.Load();
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        movePlayer();
        handleAnimations();
    }

    void movePlayer()
    {
        /*if (Input.GetKey(UpKey)) rb.linearVelocity = new Vector3(0, speed, 0);
        else if (Input.GetKey(DownKey)) rb.linearVelocity = new Vector3(0, -speed, 0);
        else if (Input.GetKey(LeftKey)) rb.linearVelocity = new Vector3(-speed, 0, 0);
        else if (Input.GetKey(RightKey)) rb.linearVelocity = new Vector3(speed, 0, 0);
        else rb.linearVelocity = new Vector3(0, 0, 0);
        */
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * speed * speedM;
        velocity.y = moveInput.y * speed * speedM;

        rb.linearVelocity = velocity;
    }

    void handleAnimations()
    {
        if (Input.GetKeyDown(UpKey)) animator.SetTrigger("bwd");
        else if (Input.GetKeyDown(DownKey)) animator.SetTrigger("fwd");
        else if (Input.GetKeyDown(LeftKey) || Input.GetKeyDown(RightKey)) animator.SetTrigger("side");
        else if (!(Input.GetKey(UpKey) || Input.GetKey(DownKey) || Input.GetKey(LeftKey) || Input.GetKey(RightKey))) animator.SetTrigger("idle");

        if (moveInput.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
