using UnityEngine;

public class ReturnTrigger : MonoBehaviour
{
    [SerializeField]
    private TriggerPlatform targetPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            targetPlatform.ReturnPlatform();
        }
    }
}