using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;

    private void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    //Using the quadratic formula predict the movement of the enemy tank by adjusting tank rotation
    private Vector3 CalculateTrajectory()
    {
        Vector3 p = enemy.transform.position - this.transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float s = bullet.GetComponent<MoveShell>().speed;

        float a = Vector3.Dot(v, v) - s * s;
        float b = Vector3.Dot(p, v);
        float c = Vector3.Dot(p, p);

        //Inside the square root
        float d = b * b - a * c;

        //d is too low to catch up with the target
        if (d < 0.1f)
        {
            return Vector3.zero;
        }

        float sqrt = Mathf.Sqrt(d);
        float t1 = (-b - sqrt) / c;
        float t2 = (-b + sqrt) / c;

        float t = 0;
        if (t1 <= 0 && t2 <= 0)
        {
            return Vector3.zero;
        }
        else if (t1 < 0) t = t2;
        else if (t2 < 0) t = t1;
        else
        {
            t = Mathf.Max(new float[]
                { t1, t2});
        }

        return t * p + v;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 aimAt = CalculateTrajectory();
            if (aimAt != Vector3.zero)
            {
                this.transform.forward = aimAt;
                CreateBullet();
            }
        }
    }
}
