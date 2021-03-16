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
}
