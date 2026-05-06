using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float moveDistance = 3f;

    [Header("Audio")]
    [SerializeField] private AudioSource walkingAudio;

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        this.startPosition = transform.position;

        if (this.walkingAudio != null)
        {
            this.walkingAudio.loop = true;

            if (!this.walkingAudio.isPlaying)
            {
                this.walkingAudio.Play();
            }
        }
    }

    void Update()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;

        transform.position += direction * speed * Time.deltaTime;

        if (movingRight && transform.position.x >= startPosition.x + moveDistance)
        {
            movingRight = false;
            transform.forward = Vector3.left;
        }
        else if (!movingRight && transform.position.x <= startPosition.x - moveDistance)
        {
            movingRight = true;
            transform.forward = Vector3.right;
        }
    }
}