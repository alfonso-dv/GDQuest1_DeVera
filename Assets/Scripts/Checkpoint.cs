using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            character.SetCheckpoint(this.transform);
        }
    }
}