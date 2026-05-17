using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        UIManager.Instance.CollectCoin();

        if (this.pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                this.pickupSound,
                transform.position
            );
        }

        Destroy(gameObject);
    }
}