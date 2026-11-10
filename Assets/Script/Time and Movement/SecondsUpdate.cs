using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    float timeStartOffset = 0;
    bool gotStartTime = false;

    public float speed = 1f;

    void Update()
    {

        //Consistent with other computer this will run based on the time past
        //So the velocity(Z value) keep increasing. (Time.realtimeSinceStartup - timeStartOffset(Starting Time)
        /*if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        this.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y,
            Time.realtimeSinceStartup - timeStartOffset);*/


        //With times speed, the movement now can be adjusted
        //If the speed float value is the same with other update, this will make all walk at the same pace / speed
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        this.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y,
            (Time.realtimeSinceStartup - timeStartOffset) * speed);
    }
}
