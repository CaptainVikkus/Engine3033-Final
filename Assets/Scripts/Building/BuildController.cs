using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildController : MonoBehaviour
{
    [SerializeField] private GameObject WoodWall;
    [SerializeField] private GameObject BrickWall;
    [SerializeField] private GameObject SteelWall;
    [SerializeField] private Vector3 placeOffset = new Vector3(2, 1, 0);
    public InventoryController.Material buildMat;

    private GameObject placingWall;
    public bool isBuilding { get; private set; }

    void StartBuilding(InventoryController.Material material)
    {
        Vector3 placement = transform.position +
            transform.right * placeOffset.x +
            transform.up * placeOffset.y +
            transform.forward * placeOffset.z;

        switch (material)
        {
            case InventoryController.Material.Wood:
                placingWall = Instantiate(WoodWall, placement, transform.rotation, transform);
                break;
            case InventoryController.Material.Brick:
                placingWall = Instantiate(BrickWall, placement, transform.rotation, transform);
                break;
            case InventoryController.Material.Steel:
                placingWall = Instantiate(SteelWall, placement, transform.rotation, transform);
                break;
            default:
                placingWall = Instantiate(WoodWall, placement, transform.rotation, transform);
                break;
        }

        placingWall.GetComponent<WallBehaviour>().Place();
        buildMat = material;
        isBuilding = true;
    }

    void PlaceBuilding()
    {
        WallBehaviour build = placingWall.GetComponent<WallBehaviour>();

        //Build does not intersect anything
        if (build.CanBuild())
        {

            //check if resources are available
            if (InventoryController.RemoveResource(buildMat, build.cost))
            {
                build.Build();
                placingWall.transform.parent = null;
                placingWall = null;

                isBuilding = false;
            }
        }
    }

    public void CancelBuilding()
    {
        Destroy(placingWall);
        isBuilding = false;
    }

    private void OnFire(InputValue value)
    {
        if (enabled == false) return;

        if (value.isPressed)
        {
            if (isBuilding)
                PlaceBuilding();
            else
                StartBuilding(buildMat);
        }
    }

}
