using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private AudioClip[] ballSounds = null;
    [SerializeField] private Paddle paddle;
    [SerializeField] private float xPush = 5f;
    [SerializeField] private float yPush = 5f;
    [SerializeField] private float randomFactor = 0.2f;

    private AudioSource audioSource;
    private Rigidbody2D ballRb;
    private Vector2 paddleToBallVector;
    private Vector2 paddlePos;
    private bool hasStarted = false;

    private void Start()
    {
        paddle = FindObjectOfType<Paddle>();
        ballRb = this.GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LockBallToPaddle()
    {
        paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        this.transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ballRb.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(
            Random.Range(0, randomFactor),
            Random.Range(0, randomFactor)
        );

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            ballRb.velocity += velocityTweak;
        }
    }
}