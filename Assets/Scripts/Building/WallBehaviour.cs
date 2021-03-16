using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour, IKillable, IDamageable, IBuildable
{
    public Transform placePos;
    [SerializeField] private float health = 5.0f;
    public int cost = 5;

    private bool canPlace;
    private BoxCollider wallCollider;
    private int contacts;

    private void Awake()
    {
        wallCollider = GetComponent<BoxCollider>();
    }

    public void Damage(float damageTaken)
    {
        health += damageTaken;
        if (health <= 0) Kill();
    }

    public void Kill()
    {
        //TODO: Play particles

        Destroy(gameObject);
    }

    public bool CanBuild()
    {
        Debug.Log("Contacts: " + contacts.ToString());
        return contacts <= 0;
    }

    public void Place()
    {
        MeshRenderer matR = GetComponent<MeshRenderer>();
        Material mat = matR.material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);

        wallCollider.isTrigger = true;
    }

    public void Build()
    {
        MeshRenderer matR = GetComponent<MeshRenderer>();
        Material mat = matR.material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f);

        wallCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Contact Added");
        contacts++;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Contact Removed");
        contacts--;
    }
}
