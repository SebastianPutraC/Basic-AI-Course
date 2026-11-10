using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float speed = 0f;
    float yspeed = 0f;
    float mass = 10f;
    float force = 1f;
    float drag = 1f;
    float gravity = -9.8f;
    float gAccel;
    float acceleration;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Only pushed by acceleration at the start 
        acceleration = force / mass;
        speed += acceleration * 1f;
        gAccel = gravity / mass;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //this.transform.Translate(0, 0, speed * Time.deltaTime);


        //This bullet will keep accelerating
        //acceleration = force / mass;
        //speed += acceleration * Time.deltaTime;


        speed *= (1f - Time.deltaTime * drag);
        yspeed += gAccel * Time.deltaTime;
        this.transform.Translate(0f, yspeed, speed);
    }
}
