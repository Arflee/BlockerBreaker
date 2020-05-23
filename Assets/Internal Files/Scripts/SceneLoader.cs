using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public delegate void OnSceneChanged();
    public static OnSceneChanged sceneChanged;

    private void Start()
    {
        sceneChanged?.Invoke();
    }

    public void LoadNextScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(buildIndex + 1, LoadSceneMode.Single);
    }

    // the lose scene always must be the last one
    public void LoadLoseScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1, LoadSceneMode.Single);
    }

    // the first scene always must be the "start" scene
    public void LoadStartScene()
    {
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}