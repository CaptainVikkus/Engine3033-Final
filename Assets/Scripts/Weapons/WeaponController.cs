using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

[Serializable]
public struct WeaponStats
{
    public WeaponType WeaponType;
    public string WeaponName;
    public float Damage;
    public int BulletsInClip;
    public int ClipSize;
    public int BulletsAvailable;
    public float FireStartDelay;
    public float FireRate;
    public float FireDistance;
    public bool Repeating;
    public float ReloadSpeed;
    public LayerMask WeaponHitLayers;
}

public class WeaponController : MonoBehaviour
{
    public Transform gripLocation => gripIKLocation;
    [SerializeField] private Transform gripIKLocation;
    [SerializeField] protected Transform muzzle;

    public WeaponStats WeaponStat => stats;

    [SerializeField] protected GameObject firingAnimation;
    [SerializeField] protected WeaponStats stats;


    protected WeaponEquip weaponHolder;
    protected ParticleSystem fireFX;

    public bool Firing { get; private set; }
    public bool Reloading { get; private set; }

    public void Initialize(WeaponEquip holder)
    {
        weaponHolder = holder;
    }
    public virtual void StartFiringWeapon()
    {
        Firing = true;
        if (stats.Repeating)
        {
            InvokeRepeating(nameof(FireWeapon), stats.FireStartDelay, stats.FireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        Firing = false;
        if (fireFX) Destroy(fireFX.gameObject);
        CancelInvoke(nameof(FireWeapon));
    }

    //Default Firing, no bullet effects
    protected virtual void FireWeapon()
    {
        Debug.Log("Firing Weapon");
        stats.BulletsInClip--;
    }

    public virtual void StartReloading()
    {
        Reloading = true;
        if (fireFX) Destroy(fireFX.gameObject);
        Invoke(nameof(ReloadWeapon), stats.ReloadSpeed);
    }

    public virtual void StopReloading()
    {
        Reloading = false;
        CancelInvoke(nameof(ReloadWeapon));
    }

    protected virtual void ReloadWeapon()
    {

        int bulletsToReload = stats.ClipSize - stats.BulletsAvailable;
        if (bulletsToReload < 0)
        {
            bulletsToReload = stats.ClipSize - stats.BulletsInClip;
            stats.BulletsInClip = stats.ClipSize;
            stats.BulletsAvailable -= bulletsToReload;
        }
        else
        {
            stats.BulletsInClip = stats.BulletsAvailable;
            stats.BulletsAvailable = 0;
        }

        Reloading = false;
    }

    public void AddAmmo(int value)
    {
        if (value <=0) { return; } //do nothing

        stats.BulletsAvailable += value;
    }
}
