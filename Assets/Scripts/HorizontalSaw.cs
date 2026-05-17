using UnityEngine;

public class HorizontalSaw : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        this.startPosition = transform.position;
    }

    void Update()
    {
        if (this.movingRight)
        {
            transform.position += Vector3.right * this.moveSpeed * Time.deltaTime;

            if (transform.position.x >= this.startPosition.x + this.moveDistance)
            {
                this.movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * this.moveSpeed * Time.deltaTime;

            if (transform.position.x <= this.startPosition.x - this.moveDistance)
            {
                this.movingRight = true;
            }
        }
    }
}