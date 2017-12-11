using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float speed = 2;
    private Vector3 targetPosition;
    //private bool isMoving;
    public bool isWalking;
    private Vector3 playerPosition;
    GameObject player;
    HoloToolkit.Unity.InputModule.GameManager gameManager;
    Transform playerTransform;       // reference to player position
    private bool killed;
    private float time;
    private bool scoreAdd = false;
    private bool scoreAdded = false;

    private bool Attacked = false;
    public float attackRate = 0.5f; // lower number = faster attack
    public float AttackTime = 0.5f;
    public float enemyDamage = 10f;
    private float InitAttackTime;
    public AudioClip[] audioClips;

    AudioSource audioSource;
    public AudioClip deathSound;

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("Environment").GetComponent<HoloToolkit.Unity.InputModule.GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        float playerSpeed = player.GetComponent<PlayerMovement>().speed;
        playerTransform = player.transform;
        isWalking = true;
        InitAttackTime = AttackTime;
        time = 1.0f;
        killed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            audioSource.Play();
            DeleteEnemy();
            killed = true;
            scoreAdd = true;
        }       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Attacked)
        {
            player.GetComponent<PlayerHealth>().playerHealth -= enemyDamage;
            //GameObject.Find("PlayerHurtAudio").GetComponent<AudioSource>().Play();
            PlayerDamageAudio();
            print("ouch");
            Attacked = true;
        }
    }

    private void PlayerDamageAudio()
    {
        int ran = Random.Range(0, audioClips.Length);
        AudioSource audioSrc = GameObject.Find("PlayerHurtAudio").GetComponent<AudioSource>();
        audioSrc.clip = audioClips[ran];
        audioSrc.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        }
    }

    void DeleteEnemy()
    {
        Destroy(this.gameObject.GetComponent<MeshRenderer>());
        Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        Destroy(this.gameObject.GetComponent<BoxCollider>());
    }

    private void MovePlayer()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        targetPosition = playerPosition;
        targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed / 10f) * Time.deltaTime);
    }

    public void OnMove()
    {
        isWalking = true;
    }

    public void OnStop()
    {
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            MovePlayer();
        }

        // destroy enemy after 1 second to allow sound to be played
        if (killed){time -= Time.deltaTime;}
        if (time <= 0.0f){Destroy(this.gameObject);}

        // Attacking delay
        if (Attacked)
        {
            AttackTime -= Time.deltaTime;
        }
        if (AttackTime <= 0.0f)
        {
            Attacked = false;
            AttackTime = InitAttackTime;
        }

        if (scoreAdd && !scoreAdded)
        {
            gameManager.score += 1;
            scoreAdded = true;
        }

        // always look at player 
        playerPosition = GameObject.Find("Player").transform.position;
        targetPosition = playerPosition;
        transform.LookAt(targetPosition);

        // lock z and x rotation
        Quaternion lockRotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
        transform.rotation = lockRotation;
    }
}
