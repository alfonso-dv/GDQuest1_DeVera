using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    private bool on = false;
    private bool characterInRange = false;
    private InputAction interactAction;

    [SerializeField]
    private Transform onPosition;

    [SerializeField]
    private Transform offPosition;

    [SerializeField]
    private GameObject leverHandle;

    [SerializeField]
    private MovingPlatform targetPlatform;

    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");
    }

    void ToggleLever()
    {
        this.on = !this.on;

        if (this.on)
        {
            this.leverHandle.transform.SetPositionAndRotation(
                this.onPosition.position,
                this.onPosition.rotation
            );

            this.targetPlatform.SetMoving(true);
        }
        else
        {
            this.leverHandle.transform.SetPositionAndRotation(
                this.offPosition.position,
                this.offPosition.rotation
            );

            this.targetPlatform.SetMoving(false);
        }
    }

    void FixedUpdate()
    {
        if (this.characterInRange && this.interactAction.WasPressedThisFrame())
        {
            this.ToggleLever();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            this.characterInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            this.characterInRange = false;
        }
    }
}