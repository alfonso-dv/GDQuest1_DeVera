using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damageAmount = 20f;
    [SerializeField] private float damageCooldown = 1f;

    private float cooldownTimer = 0f;

    private void Update()
    {
        this.cooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.cooldownTimer > 0f)
        {
            return;
        }

        Character character = other.GetComponentInParent<Character>();

        if (character != null)
        {
            character.InflictDamage(this.damageAmount);
            this.cooldownTimer = this.damageCooldown;
        }
    }
}