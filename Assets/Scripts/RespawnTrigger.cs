using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            character.RespawnAtCheckpoint();
        }
    }
}