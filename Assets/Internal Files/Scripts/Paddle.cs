using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float minPaddlePos = 1f;
    [SerializeField] private float maxPaddlePos = 15f;

    private Vector2 paddlePos;
    private GameStatus game;
    private Ball ball;

    private void Start()
    {
        game = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }
    private void Update()
    {
        paddlePos = new Vector2(GetXPos(), transform.position.y);

        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (game.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }

        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));
        return Mathf.Clamp(position.x, minPaddlePos, maxPaddlePos);

    }
}