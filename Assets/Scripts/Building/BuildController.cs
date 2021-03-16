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
    [SerializeField] private float placeAngle = 20f;
    public InventoryController.Material buildMat;
    public LayerMask groundLayer;

    private GameObject placingWall;
    private WallBehaviour wall;
    public bool isBuilding { get; private set; }


    private void Update()
    {
        if (isBuilding)
        {
            Vector3 rayStart = Quaternion.AngleAxis(placeAngle, transform.right) * transform.forward;
            if (Physics.Raycast(transform.position + transform.up, rayStart, out RaycastHit hit, 10f, groundLayer))
            {
                placingWall.transform.position = hit.point + placingWall.transform.up * (placingWall.transform.localScale.y / 1.9f);
            }
        }
    }

    void StartBuilding(InventoryController.Material material)
    {
        switch (material)
        {
            case InventoryController.Material.Wood:
                placingWall = Instantiate(WoodWall, transform);
                break;
            case InventoryController.Material.Brick:
                placingWall = Instantiate(BrickWall, transform);
                break;
            case InventoryController.Material.Steel:
                placingWall = Instantiate(SteelWall, transform);
                break;
            default:
                placingWall = Instantiate(WoodWall, transform);
                break;
        }

        wall = placingWall.GetComponent<WallBehaviour>();
        wall.Place();
        buildMat = material;
        isBuilding = true;
    }

    void PlaceBuilding()
    {
        //Build does not intersect anything
        if (wall.CanBuild())
        {
            Debug.Log("Placing...");
            //check if resources are available
            if (InventoryController.RemoveResource(buildMat, wall.cost))
            {
                wall.Build();
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
