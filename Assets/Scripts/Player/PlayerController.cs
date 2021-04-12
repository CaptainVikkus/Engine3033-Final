using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(BuildController), typeof(WeaponEquip))]
public class PlayerController : MonoBehaviour
{
    private MotionController walker;
    private CameraController viewer;
    private WeaponEquip equipper;
    private BuildController builder;
    public HUDBehaviour hud;
    public Canvas pauseScreen;

    private bool isPaused;

    private void Awake()
    {
        walker = GetComponent<MotionController>();
        viewer = GetComponent<CameraController>();
        equipper = GetComponent<WeaponEquip>();
        builder = GetComponent<BuildController>();

        //Make sure builder is off
        builder.enabled = false;
    }

    private void OnSwapMode(InputValue value)
    {
        if (value.isPressed)
        {
            if (builder.isBuilding)
                builder.CancelBuilding();

            equipper.enabled = !equipper.isActiveAndEnabled;
            builder.enabled = !builder.isActiveAndEnabled;
        }
    }

    //Convert Steel to Ammo
    private void OnJump(InputValue value)
    {
        //Check for available steel
        if (InventoryController.RemoveResource(InventoryController.Material.Steel, 1))
        {
            //Add 20 bullets to ammo bag
            equipper.equippedWeapon.AddAmmo(20);
        }
    }

    private void OnPause(InputValue value)
    {
        Pause();
    }

    public void Pause()
    {
        isPaused = !isPaused;

        viewer.lockCamera = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : viewer.cursorMode;

        hud.gameObject.SetActive(!isPaused);
        pauseScreen.gameObject.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
    }
}
