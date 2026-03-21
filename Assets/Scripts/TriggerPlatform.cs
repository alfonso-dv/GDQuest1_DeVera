using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private float speed = 2f;

    private Vector3 startPosition;
    private Vector3 currentTarget;

    private bool isMoving = false;

    private void Start()
    {
        // Save original position
        startPosition = transform.position;
        currentTarget = startPosition;
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget,
            speed * Time.deltaTime
        );

        // Stop when reached
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            isMoving = false;
        }
    }

    public void ActivatePlatform()
    {
        currentTarget = targetPosition.position;
        isMoving = true;
    }

    public void ReturnPlatform()
    {
        currentTarget = startPosition;
        isMoving = true;
    }
}