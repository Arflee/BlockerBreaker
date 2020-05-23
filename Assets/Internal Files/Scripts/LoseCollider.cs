using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private SceneLoader loader;

    public static int lostScene;

    private void Start()
    {
        loader = GameObject.FindObjectOfType<SceneLoader>();
        lostScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        loader.LoadLoseScene();
    }
}