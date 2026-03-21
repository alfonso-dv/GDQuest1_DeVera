using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float platformSpeed;

    [SerializeField]
    private Vector3 start;

    [SerializeField]
    private Vector3 end;

    [SerializeField]
    private bool isMoving = true;

    private Vector3 lastPosition;
    private Vector3 platformVelocity;

    private float timer = 0f; // own timer bc otherwise the platform will jump as the "universal timer" still runs

    void Start()
    {
        this.lastPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        if (!this.isMoving)
        {
            this.platformVelocity = Vector3.zero;
            this.lastPosition = this.transform.position;
            return;
        }

        // ✅ advance timer ONLY when moving
        this.timer += Time.fixedDeltaTime * this.platformSpeed;

        float pingPong = Mathf.PingPong(this.timer, 1.0f);
        Vector3 newPosition = Vector3.Lerp(this.start, this.end, pingPong);

        this.transform.localPosition = newPosition;

        this.platformVelocity = (this.transform.position - this.lastPosition) / Time.fixedDeltaTime;
        this.lastPosition = this.transform.position;
    }

    public Vector3 GetVelocity()
    {
        return this.platformVelocity;
    }

    public void SetMoving(bool moving)
    {
        this.isMoving = moving;
    }
}