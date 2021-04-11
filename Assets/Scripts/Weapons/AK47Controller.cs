using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Controller : WeaponController
{
    protected override void FireWeapon()
    {
        if (stats.BulletsInClip > 0 && !Reloading && !weaponHolder.Controller.isRunning)
        {
            base.FireWeapon();
            if (!fireFX)
                fireFX = Instantiate(firingAnimation, muzzle).GetComponent<ParticleSystem>();

            Ray ray = new Ray(muzzle.position, muzzle.forward);

            //Check for a hit
            if (Physics.Raycast(ray, out RaycastHit hit,
                stats.FireDistance, stats.WeaponHitLayers)) 
                OnBulletHit(hit);

        }
        else if(stats.BulletsInClip <= 0)
        {
            if (!weaponHolder) return; //no clip

            weaponHolder.StartReloading();
        }
    }

    protected void OnBulletHit(RaycastHit hit)
    {
        //TODO: put damage here
        var damageable = hit.collider.gameObject.GetComponent<ZombieController>();
        if (damageable != null)
            damageable.Damage(stats.Damage);
        Debug.Log("HIT!");
    }
}
