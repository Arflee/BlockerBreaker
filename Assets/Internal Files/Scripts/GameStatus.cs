using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    [SerializeField, Range(0.5f, 1.5f)] private float gameSpeed = 1f;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private int pointsPerBlock = 10;
    [SerializeField] private int currentScore = 0;
    [SerializeField] private bool isAutoPlayEnabled = false;

    private bool finalScene = false;
    private SceneLoader loader;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        SceneLoader.sceneChanged += CheckOnFinalScene;

        Screen.SetResolution(960, 720, FullScreenMode.Windowed);

        scoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        if (finalScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                loader.LoadStartScene();
            }
        }
        Time.timeScale = gameSpeed;
    }

    private void CheckOnFinalScene()
    {
        // final scene must always be penultimate
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2) 
        {
            finalScene = true;
            loader = FindObjectOfType<SceneLoader>();
        }
    }

    public void AddScore()
    {
        currentScore += pointsPerBlock;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("bestScore", currentScore);
        Destroy(this.gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public int GetScore()
    {
        return currentScore;
    }
}