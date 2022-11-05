using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    protected override void Start()
    {
        base.Start();

    }
    public override float ChangeHealth(float num)
    {
        float currentHealth = base.ChangeHealth(num);
        animator.SetFloat("BloodPercent",currentHealth/health);
        return currentHealth;
    }
}
