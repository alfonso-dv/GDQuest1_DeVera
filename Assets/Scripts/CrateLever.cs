using UnityEngine;
using UnityEngine.InputSystem;

public class CrateLever : MonoBehaviour
{
    private bool characterInRange = false;
    private bool on = false;
    private InputAction interactAction;

    [SerializeField]
    private Transform onPosition;

    [SerializeField]
    private Transform offPosition;

    [SerializeField]
    private GameObject leverHandle;

    [SerializeField]
    private ResettableCrate targetCrate;

    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");
    }

    void FixedUpdate()
    {
        if (this.characterInRange && this.interactAction.WasPressedThisFrame())
        {
            this.ToggleLever();
        }
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
        }
        else
        {
            this.leverHandle.transform.SetPositionAndRotation(
                this.offPosition.position,
                this.offPosition.rotation
            );
        }

        if (this.targetCrate != null)
        {
            this.targetCrate.ResetCrate();
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