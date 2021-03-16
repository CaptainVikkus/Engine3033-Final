using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehaviour : MonoBehaviour
{
    public InventoryController.Material material;
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Do Collect
            InventoryController.AddResource(material, value);

            //Delete
            Destroy(gameObject);
        }
    }
}
