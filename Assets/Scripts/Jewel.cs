using UnityEngine;

public class Jewel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            character.DisableMovement();
        }

        UIManager.Instance.ShowVictoryScreen();

        gameObject.SetActive(false);
    }
}