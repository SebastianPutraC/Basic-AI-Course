using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        //Speed is as fast as LateUpdate, based on framerate
        //this.transform.Translate(0, 0, 0.01f);


        //Speed will be the same as the SecondsUpdate
        //this.transform.Translate(0, 0, Time.deltaTime);


        //Movement is based on speed because Time.deltaTime already made it consistent using time
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
