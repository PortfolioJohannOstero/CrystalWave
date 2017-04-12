using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    public UiHandler uiHandler;
    public manager gameManager;

    [Header("Particles")]
    public ParticleSystem trailParticle;
    public ParticleSystem deathParticle;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip topBottomSound;

    private PlayerMovement movementRef;
    private SpriteRenderer spriteRender;

    private void Start()
    {
        movementRef = GetComponent<PlayerMovement>();
        spriteRender = GetComponent<SpriteRenderer>();
    }


    private int currentScore;

    public void AddPoint(int points)
    {
        currentScore += points;
        uiHandler.pointsLabel.text = currentScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.PlayOneShot(deathSound, 1.0f);

        deathParticle.transform.position = transform.position;
        deathParticle.Play();

        DisablePlayer();


        gameManager.highscore = currentScore;

        uiHandler.ShowGameOver(gameManager.highscore);
        gameManager.StopGame();
    }

    void DisablePlayer()
    {
        trailParticle.Stop();
        this.enabled = false;
        movementRef.enabled = false;
        spriteRender.enabled = false;
    }
}
