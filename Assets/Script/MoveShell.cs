using UnityEngine;

public class MoveShell : MonoBehaviour
{
    float speed = 1f;
    void Update()
    {
        this.transform.Translate(0, speed * Time.deltaTime * 0.5f, speed * Time.deltaTime);
    }
}
