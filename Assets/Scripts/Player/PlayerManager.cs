using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float speed = 5f;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;

    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        if (Input.GetKey(UpKey)) rb.linearVelocity = new Vector3(0, speed, 0);
        else if (Input.GetKey(DownKey)) rb.linearVelocity = new Vector3(0, -speed, 0);
        else if (Input.GetKey(LeftKey)) rb.linearVelocity = new Vector3(-speed, 0, 0);
        else if (Input.GetKey(RightKey)) rb.linearVelocity = new Vector3(speed, 0, 0);
        else rb.linearVelocity = new Vector3(0, 0, 0);
    }
}
