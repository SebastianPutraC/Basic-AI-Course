using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateUpdateMove : MonoBehaviour
{
    public float speed = 1f;
    void LateUpdate()
    {
        //Speed is as fast as Update, based on framerate
        //this.transform.Translate(0, 0, 0.01f);


        //Speed will be the same as the SecondsUpdate
        //this.transform.Translate(0, 0, Time.deltaTime);


        //Movement is based on speed because Time.deltaTime already made it consistent using time
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
