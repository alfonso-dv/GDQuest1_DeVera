using UnityEngine;

public class ResettableCrate : MonoBehaviour
{
    [SerializeField]
    private Transform resetPoint;

    private Rigidbody rb;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    public void ResetCrate()
    {
        if (this.rb != null)
        {
            this.rb.linearVelocity = Vector3.zero;
            this.rb.angularVelocity = Vector3.zero;
        }

        this.transform.position = this.resetPoint.position;
        this.transform.rotation = this.resetPoint.rotation;
    }
}