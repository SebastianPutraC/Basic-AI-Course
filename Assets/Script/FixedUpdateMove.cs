using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateMove : MonoBehaviour
{
    public float speed = 1f;
    void FixedUpdate()
    {
        //Speed is different from LateUpdate and Update
        //because its called every fixed framerate
        //this.transform.Translate(0, 0, 0.01f);


        //Speed will be the same as the SecondsUpdate
        //this.transform.Translate(0, 0, Time.deltaTime);


        //Movement is based on speed because Time.deltaTime already made it consistent using time
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
