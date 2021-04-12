using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Controller : WeaponController
{
    public GameObject hitFX;

    protected override void FireWeapon()
    {
        if (stats.BulletsInClip > 0 && !Reloading && !weaponHolder.Controller.isRunning)
        {
            base.FireWeapon();
            if (!fireFX)
                fireFX = Instantiate(firingAnimation, muzzle).GetComponent<ParticleSystem>();

            Ray ray = new Ray(muzzle.position, muzzle.forward);
            Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward * stats.FireDistance, Color.yellow, .5f);
            //Check for a hit
            if (Physics.Raycast(ray, out RaycastHit hit, stats.FireDistance, stats.WeaponHitLayers)) 
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
        ZombieController damageable;
        if (hit.collider.gameObject.TryGetComponent(out damageable))
        {
            damageable.Damage(stats.Damage);
            Debug.Log("Zombie Hit");
        }
        else
        {
            Debug.Log($"HIT! {hit.collider.gameObject.name}");
        }

        //Spawn Flash
        Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
