using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public List<GameObject> items;
    [SerializeField]
    public float minSpawnDelay;
    [SerializeField]
    public float maxSpawnDelay;
    [SerializeField]
    ItemSpawn[] itemSpawns;

    public float spawnTimer;

    ItemSpawn currentSpawn;
    GameObject currentWeapon;

    HoloToolkit.Unity.InputModule.TapToPlace tapPlace;

    // Use this for initialization
    void Start()
    {
        spawnTimer = 0;
        tapPlace = GameObject.Find("Environment").GetComponent<HoloToolkit.Unity.InputModule.TapToPlace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tapPlace.Playing)  // Checks if the character has been placed and game has started
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= minSpawnDelay)
            {
                SpawnWeapon();
            }
        }
    }

    void SpawnWeapon()
    {
        float rand = Random.Range(minSpawnDelay, maxSpawnDelay);

        if (spawnTimer >= rand)
        {
            ChooseSpawn();
            ChooseWeapon();
            GameObject ins = Instantiate(currentWeapon, currentSpawn.gameObject.transform.position, Quaternion.identity);
            spawnTimer = 0f;
        }
    }

    void ChooseSpawn()
    {
        currentSpawn = null;
        int rand = Random.Range(0, itemSpawns.Length);
        currentSpawn = itemSpawns[rand];
    }

    void ChooseWeapon()
    {
        currentWeapon = null;
        int rand = Random.Range(0, items.Count);
        float chance = Random.Range(0, 1);
        currentWeapon = items[rand];
    }
}
