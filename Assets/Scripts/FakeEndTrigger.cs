using UnityEngine;

public class FakeEndTrigger : MonoBehaviour
{
    [SerializeField]
    private TriggerPlatform targetPlatform;

    [SerializeField]
    private GameObject[] objectsToShow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            // Move platform forward
            targetPlatform.ActivatePlatform();

            // Show objects
            foreach (GameObject obj in objectsToShow)
            {
                obj.SetActive(true);
            }
        }
    }
}