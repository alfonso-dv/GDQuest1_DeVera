using UnityEngine;

public class EnemySquash : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip squashSound;
    [SerializeField] private float squashHeight = 0.25f;
    [SerializeField] private float destroyDelay = 0.3f;
    [SerializeField] private ParticleSystem deathParticlesPrefab;

    private bool isSquashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isSquashed)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Player must be above the enemy
        if (collision.transform.position.y > transform.position.y + 0.5f)
        {
            SquashEnemy();
        }
    }

    public void SquashEnemy()
    {
        isSquashed = true;

        if (audioSource != null && squashSound != null)
        {
            audioSource.PlayOneShot(squashSound);
        }

        if (deathParticlesPrefab != null)
        {
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }

        transform.localScale = new Vector3(
            transform.localScale.x,
            squashHeight,
            transform.localScale.z
        );

        Destroy(gameObject, destroyDelay);
    }
}