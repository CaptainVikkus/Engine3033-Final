using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponEquip))]
public class CrosshairBehaviour : MonoBehaviour
{
    public Image crosshair;
    public Camera camera;
    
    private WeaponEquip holster;

    // Start is called before the first frame update
    void Start()
    {
        holster = GetComponent<WeaponEquip>();
    }

    // Update is called once per frame
    void Update()
    {
        crosshair.rectTransform.position =
            camera.WorldToScreenPoint(
                transform.position + holster.equippedWeapon.transform.forward * 
                holster.equippedWeapon.WeaponStat.FireDistance);
    }
}
