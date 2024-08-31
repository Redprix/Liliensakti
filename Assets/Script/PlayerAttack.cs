using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 100.0f;
    public float knockback = 200.0f;

    public bool attacked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyHit") && !attacked)
        {
            Vector2 dir = other.transform.position - transform.position;
            dir.Normalize();
            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.AddForce(dir * knockback);
            }

            other.GetComponent<Enemy>().Health -= damage;

            if (other.TryGetComponent<SpriteFlasher>(out SpriteFlasher flasher))
            {
                flasher.Flash();
            }
            attacked = true;
        }
    }
}
