using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField]
    int healAmount = 5;
    public PlayerHealth addHealth;
    public HealthBar addHealthVisual;
    protected override void DoPickup(GameObject go)
    {
        addHealth.currentHealth += healAmount;
        addHealthVisual.SetHealth(addHealth.currentHealth += healAmount);

        base.DoPickup(go);

    }

}
