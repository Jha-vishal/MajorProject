using System.Net.Mime;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SelectCar : MonoBehaviour
{
    public GameObject[] cars;
    int currentCar = 0;


    void Start()
    {

    }

    void Update()
    {
        SelectCarBtn();
    }


    void SelectedCar(int playerCar)
    {
        PlayerPrefs.SetInt("SelectedCar", playerCar);
    }


    public void SelectCarBtn()
    {
        cars[currentCar].gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCar++;
            cars[currentCar - 1].gameObject.SetActive(false);

            if (currentCar > cars.Length - 1)
            {
                currentCar = 0;
            }

            SelectedCar(currentCar);
        }

    }
}


