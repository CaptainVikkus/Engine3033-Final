using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Min(1)] public float frequency = 5;
    [Min(1)] public int baseValue = 1;
    public GameObject lootPrefab;
    public Vector3 spawnBox = Vector3.one;
    public static LootSpawner Instance { get; private set; }
    private static LootSpawner _instance;
    private float clock = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Instance = _instance;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (clock > frequency)
        {
            SpawnLoot(5);
            clock = 0;
        }
        else
        {
            clock += Time.deltaTime;
        }
    }

    //Spawn loot based on a percentage (1-100) chance with random quality
    public void SpawnLoot(float chance)
    {
        if (chance >= Random.value * 100f)
        {
            //Instantiate pickup and set material
            var loot = Instantiate(lootPrefab, FindSpawnPoint(), Quaternion.identity, transform).GetComponent<LootBehaviour>();
            BuildLoot(loot);
        }
    }

    //Spawn loot based on a percentage (1-100) Chance with random quality around Point
    public static void SpawnLoot(float chance, Transform point)
    {
        //random chance
        if (chance >= Random.value * 100f)
        {
            //Get random point around
            Vector3 spawn = point.position + new Vector3(
                (Random.value - 0.5f) * 2,
                (Random.value - 0.5f) * 2,
                (Random.value - 0.5f) * 2
             );
            //Instantiate pickup and set material
            var loot = Instantiate(LootSpawner.Instance.lootPrefab, spawn, Quaternion.identity).GetComponent<LootBehaviour>();
            BuildLoot(loot);
        }
    }

    private Vector3 FindSpawnPoint()
    {
        return transform.position + new Vector3(
                (Random.value - 0.5f) * spawnBox.x,
                (Random.value - 0.5f) * spawnBox.y,
                (Random.value - 0.5f) * spawnBox.z
             );
    }

    private static void BuildLoot(LootBehaviour loot)
    {
        float quality = Random.value;
        if (quality > .5f)
        {
            if (quality > .8f)
            {
                loot.material = InventoryController.Material.Steel;
            }
            else
            {
                loot.material = InventoryController.Material.Brick;
            }
        }
        else
        {
            loot.material = InventoryController.Material.Wood;
        }

        loot.value = Random.Range(1, 5);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnBox);
    }
}
