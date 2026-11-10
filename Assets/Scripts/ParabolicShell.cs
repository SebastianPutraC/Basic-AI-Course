using UnityEngine;

public class ParabolicShell : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rb;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        this.transform.forward = rb.linearVelocity;
    }
}
