using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{

    [SerializeField]    
    public float speed = 5f;


    void Update()
    {
        this.transform.Rotate(0,1*Time.deltaTime * speed,0);
    }
}
