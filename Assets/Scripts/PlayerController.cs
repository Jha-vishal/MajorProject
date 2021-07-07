using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Car
{
    public float horizontal;
    public float vertical;
    public float brake;

    public int maxCarHealth = 50;
    public int currentHealth;

    //public HealthBar healthBar;
    GameObject playerHealth;


    Car car;

    GameObject levelController;
    GameObject objective;


    void Awake()
    {

    }

    void Start()
    {
        car = GetComponent<Car>();

        playerHealth = GameObject.FindWithTag("Health");
        playerHealth.GetComponent<HealthBar>();
        currentHealth = maxCarHealth;
        playerHealth.GetComponent<HealthBar>().SetMaxHealth(maxCarHealth);
        objective = GameObject.FindWithTag("Destination");
        levelController = GameObject.FindWithTag("LevelController");

    }


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        brake = Input.GetAxis("Jump");




        if (currentHealth <= 0)
        {

            levelController.GetComponent<LevelController>().RestartLevel();

            if (!objective.GetComponent<Destination>().isLevelCleared || !objective.GetComponent<Tutorial>().isLevelCleared)
            {
                levelController.GetComponent<LevelController>().levelStatus.text = "Mission Failed";
                levelController.GetComponent<LevelController>().levelStatus.color = Color.red;
            }

        }

    }


    void LateUpdate()
    {
        car.Drive(horizontal, vertical, brake);
    }


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(40);
            levelController.GetComponent<LevelController>().RestartLevel();
            levelController.GetComponent<LevelController>().levelStatus.text = "Mission Failed";
            levelController.GetComponent<LevelController>().levelStatus.color = Color.red;
        }

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerHealth.GetComponent<HealthBar>().SetHealth(currentHealth);
    }

}


