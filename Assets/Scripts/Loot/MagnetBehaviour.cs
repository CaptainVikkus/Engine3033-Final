using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    public float magnetSpeed = 1.0f;
    public float rotateSpeed = 45.0f;
    public GameObject lootToken;

    private bool follow = false;
    private GameObject followTarget;

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.root.LookAt(followTarget.transform);
            transform.root.position += (transform.root.forward * magnetSpeed) * Time.deltaTime;

            //speed up
            magnetSpeed += Time.deltaTime;
        }

        lootToken.transform.rotation *= Quaternion.Euler(rotateSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follow = true;
            followTarget = other.gameObject;
            GetComponent<Collider>().enabled = false;
        }
    }
}
