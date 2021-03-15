using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform cameraRoot;
    public bool lockCamera;
    public float xSensitivity = 1f;
    public float ySensitivity = 1f;
    public bool invertY = false;

    private Vector2 previousLook;

    private void OnLook(InputValue value)
    {
        if (lockCamera) //Ignore on camera lock
            return;

        int inversion = invertY ? 1 : -1;
        Vector2 aimValue = value.Get<Vector2>();
        previousLook.y += aimValue.y * ySensitivity * inversion;
        previousLook.x += aimValue.x * xSensitivity;
        //clamp Up/Down Rot
        previousLook.y = Mathf.Clamp(previousLook.y, -45.0f, 45.0f);

        cameraRoot.rotation = Quaternion.Euler(previousLook.y, previousLook.x, 0);
        transform.rotation = Quaternion.Euler(0, previousLook.x, 0);

        //cameraRoot.localEulerAngles = Vector3.zero;
    }
}
