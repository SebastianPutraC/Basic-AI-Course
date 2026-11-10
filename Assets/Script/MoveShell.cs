using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        //Used for an example of velocity
        //this.transform.Translate(0, speed * Time.deltaTime * 0.5f, speed * Time.deltaTime);

        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
