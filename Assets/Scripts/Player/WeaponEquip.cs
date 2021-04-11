using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponEquip : MonoBehaviour
{
    [SerializeField] private GameObject weaponToEquip;
    [SerializeField] private Transform weaponEquipSocket;
    [SerializeField] private Transform aimLocation;
    [SerializeField] private Transform hipTwist;
    private Quaternion hipTwistStart;

    public WeaponController equippedWeapon;
    private Transform gripLocation;
    private bool FirePressed = false;
    public bool isFiring { get; private set; }
    public bool isReloading { get; private set; }

    public MotionController Controller => motController;
    private MotionController motController;
    private Animator animator;

    //Animators
    private static readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private static readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    private static readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
    private static readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");

    private void Awake()
    {
        motController = GetComponent<MotionController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        EquipWeapon();
        hipTwistStart = hipTwist.rotation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gripLocation.position);
    }

    //Equip the chosen weapon
    public void EquipWeapon()
    {
        GameObject wpn = Instantiate(weaponToEquip, weaponEquipSocket.position, weaponEquipSocket.rotation, weaponEquipSocket);
        if (!wpn) return;//Failed Instantiation

        equippedWeapon = wpn.GetComponent<WeaponController>();
        if (!equippedWeapon) return; //Not a weapon
        equippedWeapon.Initialize(this);

        gripLocation = equippedWeapon.gripLocation;
        animator.SetInteger(WeaponTypeHash, (int)equippedWeapon.WeaponStat.WeaponType);
        animator.SetFloat(AimHorizontalHash, 0.5f);
        animator.SetFloat(AimVerticalHash, 0.5f);
        //Teacher put event
    }

    private void OnFire(InputValue value)
    {
        if (enabled == false) return;

        FirePressed = value.isPressed;

        if (FirePressed)
            StartFiring();
        else
            StopFiring();

    }

    private void OnReload(InputValue value)
    {
        if (isReloading) return; //don't restart
        StartReloading();
    }

    //private void OnLook(InputValue value)
    //{
    //    hipTwist.rotation = Quaternion.Euler(
    //        hipTwistStart.eulerAngles.x,
    //        hipTwistStart.eulerAngles.y + aimLocation.rotation.eulerAngles.y,
    //        hipTwistStart.eulerAngles.z);
    //}

    private void StartFiring()
    {
        if (equippedWeapon.WeaponStat.BulletsAvailable <= 0 &&
            equippedWeapon.WeaponStat.BulletsInClip <= 0) return;

        isFiring = true;
        animator.SetBool(IsFiringHash, true);
        equippedWeapon.StartFiringWeapon();
    }

    private void StopFiring()
    {
        isFiring = false;
        animator.SetBool(IsFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }

    public void StartReloading()
    {
        StopFiring();
        if (equippedWeapon.WeaponStat.BulletsAvailable <= 0) return;

        isReloading = true;
        animator.SetBool(IsReloadingHash, isReloading);
        equippedWeapon.StartReloading();
        Invoke(nameof(StopReloading), equippedWeapon.WeaponStat.ReloadSpeed);
    }

    private void StopReloading()
    {
        isReloading = false;
        animator.SetBool(IsReloadingHash, isReloading);
    }
}
