using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();

        // Add ragdoll effect / death animation - добавить анимацию смерти

        Destroy(gameObject);
    }
}
