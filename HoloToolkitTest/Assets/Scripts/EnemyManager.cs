using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour {

    public GameObject enemy;
    public float spawnTime;
    public Transform[] spawnPoints;
    HoloToolkit.Unity.InputModule.TapToPlace tapPlace;
    private bool startedSpawning;

    // Use this for initialization
    void Start ()
    {
        tapPlace = GameObject.Find("Environment").GetComponent<HoloToolkit.Unity.InputModule.TapToPlace>();
        startedSpawning = false;
    }
	
	// Update is called once per frame
	void Spawn ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    void StartSpawning()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        startedSpawning = true;
    }

    private void Update()
    {
        if (tapPlace.Playing && !startedSpawning)
        {
            StartSpawning();
        }
    }
}
