using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    private bool on = false;
    private bool characterInRange = false;
    private InputAction interactAction;

    [SerializeField]
    private Transform Open;

    [SerializeField]
    private Transform Closed;

    [SerializeField]
    private GameObject chestLid;

    [SerializeField]
    private ParticleSystem openParticlesPrefab;

    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");
    }

    void ToggleChest()
    {
        this.on = !this.on;

        if (this.on)
        {
            this.chestLid.transform.SetPositionAndRotation(
                this.Open.position,
                this.Open.rotation
            );

            if (this.openParticlesPrefab != null)
            {
                Instantiate(
                    this.openParticlesPrefab,
                    this.chestLid.transform.position,
                    Quaternion.identity
                );
            }
        }
        else
        {
            this.chestLid.transform.SetPositionAndRotation(
                this.Closed.position,
                this.Closed.rotation
            );
        }
    }

    void FixedUpdate()
    {
        if (this.characterInRange && this.interactAction.WasPressedThisFrame())
        {
            this.ToggleChest();
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