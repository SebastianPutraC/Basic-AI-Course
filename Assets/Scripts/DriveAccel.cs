using UnityEngine;

public class DriveAccel : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Transform turretTransform;
    public Transform gun;
    public GameObject bulletObj;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey(KeyCode.T))
        {
            turretTransform.RotateAround(turretTransform.position, turretTransform.right, -0.5f);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            turretTransform.RotateAround(turretTransform.position, turretTransform.right, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bulletObj, gun.position, gun.rotation);
        }
    }
}
