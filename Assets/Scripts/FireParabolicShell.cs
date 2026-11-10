using UnityEngine;

//Instead of calculating the physics ourself, this is using the Unity Physics.
public class FireParabolicShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;
    float bulletSpeed = 15f;
    float rotationSpeed = 5f;
    float moveSpeed = 1f;

    private void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = bulletSpeed * turretBase.forward;
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngel(false);
        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }

    //? means Can return null
    float? CalculateAngel(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;

        //-1 is not based on the parabolic calculation, its to make it easier to hit the other tank
        float x = targetDir.magnitude - 1;
        //float x = targetDir.magnitude;
        float gravity = 9.8f;
        float sSqr = bulletSpeed * bulletSpeed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
            return null;
    }

    private void Update()
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, 
            Time.deltaTime * rotationSpeed);

        float? angle = RotateTurret();
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }*/

        if (angle != null)
        {
            CreateBullet();
        }
        else
        {
            this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
    }
}
