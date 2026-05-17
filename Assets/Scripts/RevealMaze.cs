using UnityEngine;

public class RevealHiddenItems : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenObjects;

    private void Start()
    {
        foreach (GameObject hiddenObject in this.hiddenObjects)
        {
            hiddenObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        foreach (GameObject hiddenObject in this.hiddenObjects)
        {
            hiddenObject.SetActive(true);
        }
    }
}