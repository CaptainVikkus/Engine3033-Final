using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public enum Material
    {
        Wood,
        Brick,
        Steel
    }

    public TextMeshProUGUI[] uiValues;
    public static int[] resourceValues = { 0, 0, 0 };

    // Update is called once per frame
    void Update()
    {
        _UpdateUI();
    }

    private void _UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            uiValues[i].text = resourceValues[i].ToString();
        }
    }

    public static bool AddResource(Material resource, int value)
    {
        //value is negative
        if (value < 0) return false;

        //Add resource
        resourceValues[(int)resource] += value;
        return true;
    }

    public static bool RemoveResource(Material resource, int value)
    {
        //value is negative or not enough resource
        if (value < 0 ||
            resourceValues[(int)resource] - value < 0) 
            return false;

        //Subtract resource
        resourceValues[(int)resource] -= value;
        return true;
    }

    public static void ClearResources()
    {
        for (int i = 0; i < 3; i++)
        {
            resourceValues[i] = 0;
        }
    }
}
