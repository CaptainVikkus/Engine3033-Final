using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDBehaviour : MonoBehaviour
{
    public WeaponEquip weaponBag;
    public GameObject weaponHUD;
    public TextMeshProUGUI ammoText;

    public BuildController builder;
    public GameObject buildHUD;
    public Transform[] buildSelect;

    private bool wEquipped = true;
    private bool bEquipped = true;
    private InventoryController.Material lastMat;

    // Start is called before the first frame update
    void Start()
    {
        if (weaponBag == null)
        {
            wEquipped = false;
            weaponHUD.SetActive(false);
        }
        if (builder == null)
        {
            bEquipped = false;
            buildHUD.SetActive(false);
        }
        else
        {
            buildHUD.SetActive(builder.isActiveAndEnabled);
            lastMat = builder.buildMat;
            ToggleSelect(lastMat);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (wEquipped)
        {
            int ammo = weaponBag.equippedWeapon.WeaponStat.BulletsAvailable;
            int clip = weaponBag.equippedWeapon.WeaponStat.BulletsInClip;
            int clipMax = weaponBag.equippedWeapon.WeaponStat.ClipSize;

            ammoText.text = $"{clip} / {clipMax} \n {ammo}";
        }
        if (bEquipped)
        {
            if (builder.isActiveAndEnabled)
            {
                //make sure is on
                if (!buildHUD.activeSelf)
                    buildHUD.SetActive(true);

                //check for change
                if (lastMat != builder.buildMat)
                {
                    lastMat = builder.buildMat;
                    ToggleSelect(lastMat);
                }
            }
            else if (buildHUD.activeSelf)
            {
                buildHUD.SetActive(false);
            }
        }
    }

    private void ToggleSelect(InventoryController.Material mat)
    { //Hella Crashable Code
        buildHUD.transform.position = buildSelect[(int)mat].position;
    }
}
