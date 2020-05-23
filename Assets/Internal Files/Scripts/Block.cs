using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound = null;
    [SerializeField] private GameObject sparkleVFX = null;
    [SerializeField] private Sprite[] hitSprites = null;

    private Level level;
    private GameStatus gameStatus;
    private SpriteRenderer currentSprite;
    private int timesHit = 0;
    
    private void Start()
    {
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        currentSprite = GetComponent<SpriteRenderer>();
        CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.CompareTag("Breakable"))
        {
            HandleHit(collision);
        }
    }

    private void HandleHit(Collision2D collision)
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock(collision.GetContact(0).point);
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            currentSprite.sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Sprite is missing from array: " + gameObject.name);
        }
    }

    private void CountBreakableBlocks()
    {
        level = GameObject.FindObjectOfType<Level>();
        if (gameObject.CompareTag("Breakable"))
        {
            level.CountBlocks();
        }
    }

    private void DestroyBlock(Vector2 sparklesPos)
    {
        gameStatus.AddScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.5f);
        Destroy(this.gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX(sparklesPos);
    }

    private void TriggerSparklesVFX(Vector2 positonOfSparkles)
    {
        GameObject sparkles = Instantiate(sparkleVFX, positonOfSparkles, Quaternion.identity);
        Destroy(sparkles, 2f);
    }
}