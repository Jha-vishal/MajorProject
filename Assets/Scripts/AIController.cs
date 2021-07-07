using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[RequireComponent(typeof(Car))]
public class AIController : MonoBehaviour
{

    Car car;
    public GameObject playerController;
    float steer;
    float move;
    float brake;

    void Awake()
    {

        //playerController = GameObject.FindWithTag("Player");
        //playerController.GetComponent<PlayerController>();
    }

    void Start()
    {
        car = GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        steer = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");
        brake = Input.GetAxis("Jump");
    }


    void LateUpdate()
    {
        car.Drive(steer, move, brake);
    }


}
