using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [SerializeField] private GameObject weaponToEquip;
    [SerializeField] private Transform weaponEquipSocket;

    private WeaponController equippedWeapon;
    private Transform gripLocation;
    private bool wasFiring = false;
    private bool FirePressed = false;


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

    public void StartReloading()
    {

    }
}
