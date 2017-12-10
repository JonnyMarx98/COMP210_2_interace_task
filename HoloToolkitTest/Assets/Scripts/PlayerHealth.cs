using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float playerHealth;
    public float maxHealth = 100.0f;
    HoloToolkit.Unity.InputModule.GameManager gameManager;
    GameObject healthBar;

    // Use this for initialization
    private void Awake()
    {
        playerHealth = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<HoloToolkit.Unity.InputModule.GameManager>();
        healthBar = GameObject.Find("GreenBar");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (playerHealth <= 0.0f)
        {
            // Player Dead
            gameManager.OnReset();
        }
        if (playerHealth < 100.0f)
        {
            playerHealth += 2.0f * Time.deltaTime;
        }
        // prevents player from having more than the maximum health
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }

        healthBar.transform.localScale = new Vector3(playerHealth / 100.0f, 1, 1);
	}
}
