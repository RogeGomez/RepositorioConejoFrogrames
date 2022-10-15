using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    [SerializeField] private float jumpForce = 6.0f;
    [SerializeField] private float runningSpeed = 2.5f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector3 startPosition;
    private string highScoreKey = "highScore";

    void Awake()
    {
        animator.SetBool("isAlive", true);

        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        this.transform.position = startPosition;
        rigidBody.velocity = new Vector2(0, 0);
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Jump();
                animator.SetBool("isGrounded", IsOnTheFloor());
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }
    }

    void Jump()
    {
        if (IsOnTheFloor())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsOnTheFloor()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);

        if (PlayerPrefs.GetFloat(highScoreKey, 0) < this.GetDistance())
        {
            PlayerPrefs.SetFloat(highScoreKey, this.GetDistance());
        }
    }

    public float GetDistance()
    {
        float distanceTraveled = Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x, 0));
        return distanceTraveled;
    }
}
