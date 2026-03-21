using UnityEngine;

public class ButtonSquare : MonoBehaviour
{
    [SerializeField]
    private Transform onPosition;

    [SerializeField]
    private Transform offPosition;

    [SerializeField]
    private GameObject buttonMesh;

    [SerializeField]
    private MovingPlatform targetPlatform;

    private int objectsOnButton = 0;

    void Start()
    {
        this.buttonMesh.transform.SetPositionAndRotation(
            this.offPosition.position,
            this.offPosition.rotation
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crate"))
        {
            this.objectsOnButton++;

            this.buttonMesh.transform.SetPositionAndRotation(
                this.onPosition.position,
                this.onPosition.rotation
            );

            if (this.targetPlatform != null)
            {
                this.targetPlatform.SetMoving(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crate"))
        {
            this.objectsOnButton--;

            if (this.objectsOnButton <= 0)
            {
                this.objectsOnButton = 0;

                this.buttonMesh.transform.SetPositionAndRotation(
                    this.offPosition.position,
                    this.offPosition.rotation
                );

                if (this.targetPlatform != null)
                {
                    this.targetPlatform.SetMoving(false);
                }
            }
        }
    }
}