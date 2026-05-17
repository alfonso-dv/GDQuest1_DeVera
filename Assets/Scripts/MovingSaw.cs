using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        this.startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = this.startPosition + new Vector3(0f, yOffset, 0f);
    }
}