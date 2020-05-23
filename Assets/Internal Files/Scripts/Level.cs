using UnityEngine;

public class Level : MonoBehaviour
{
    private int breakableBlocks;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}