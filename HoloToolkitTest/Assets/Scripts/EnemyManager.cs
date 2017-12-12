using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour {

    public GameObject enemy;
    public GameObject UItext;
    private GameObject ins;
    public float spawnTime;
    public Transform[] spawnPoints;
    HoloToolkit.Unity.InputModule.TapToPlace tapPlace;
    private bool startedSpawning;
    public float waveTime = 10.0f;
    private float initWaveTime;
    private int wave = 1;
    public bool waveMode = false;

    // Use this for initialization
    void Start ()
    {
        tapPlace = GameObject.Find("Environment").GetComponent<HoloToolkit.Unity.InputModule.TapToPlace>();
        startedSpawning = false;
        initWaveTime = waveTime;
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

    void DisplayWave(int waveNumber)
    {
        ins = Instantiate(UItext);
        ins.GetComponent<Text>().text = "Wave " + waveNumber + "!";
        ins.transform.SetParent(GameObject.Find("Canvas").transform);
        ins.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        if (!tapPlace.IsBeingPlaced && !startedSpawning)
        {
            StartSpawning();
        }
        waveTime -= Time.deltaTime;
        if (waveTime <= 0.0f && waveMode && !tapPlace.IsBeingPlaced)
        {
            StartSpawning();
            waveTime = initWaveTime;
            wave++;
            print("Wave" + wave + " more enemies >:)");
            DisplayWave(wave);
        }
        ins.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
        if(ins.transform.localScale.x > 1.5f)
        {
            Destroy(ins.gameObject);
        }

    }
}
