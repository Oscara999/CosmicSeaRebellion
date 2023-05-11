using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] Collider damageCollider;
    public int currentWeaponDamage;

    void Awake()
    {
        damageCollider = GetComponentInChildren<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void ChangeDamageCollider()
    {
        damageCollider.enabled = !damageCollider.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            if (Player.Instance.PlayerStats != null)
            {
                Player.Instance.PlayerStats.health.TakeDamage(currentWeaponDamage);
            }
        }

        if (other.CompareTag(Tags.ENEMY_TAG))
        {
            EnemyHealth enemyStats = other.GetComponent<EnemyHealth>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage,damageCollider.transform.position);
            }
        }
    }
}
