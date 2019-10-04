using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRaise : MonoBehaviour
{
    //movement speed in units per second
    private float movementSpeed = 0.45f;

    void Update()
    {

        //update the position
        transform.position = transform.position + new Vector3(0, movementSpeed * Time.deltaTime, 0);

        //output to log the position change
        Debug.Log(transform.position);
    }
}