using UnityEngine;

public class Move : MonoBehaviour
{

    public GameObject goal;
    private Vector3 direction;
    public float speed = 2f;

    void Start()
    {
        //Teleport to goal
        //this.transform.Translate(goal.transform.position);


        //Teleport by adding original position with (6, 0, 2) as direction;
        //this.transform.Translate(6, 0, 2);


        //direction = goal.transform.position - this.transform.position;
        //this.transform.position = this.transform.position + direction;
        //Above and below will do the same thing
        //this.transform.Translate(direction);
    }

    private void LateUpdate()
    {
        direction = goal.transform.position - this.transform.position;

        //Dot product calculate (a.x * b.x ,a.y * b.y,. a.z * b.z) To get the angle between vectors
        //The two vectors are the direction of target, and the forward vector of the character
        //Assuming that this character can see if the dot product is higher than 0 (Less than 90 degrees)
        //This means that the vision can be adjusted by changing the magic number on the if statement
        /*if (Vector3.Dot(direction, this.transform.forward) > 0)
        {
            this.transform.LookAt(goal.transform.position);
            if (direction.sqrMagnitude > 4)
            {
                Vector3 velocity = direction.normalized * speed * Time.deltaTime;
                this.transform.position = this.transform.position + velocity;
            }
        }*/


        //Compared on calculating Dot Product, we can use Vector.Angle (return 0 to 180 degrees)
        //It calculate the degrees of the target and the forward vector of the character
        //In here if the target is moving closer to the forward vector, the angle is getting lower
        //Lower than 20 degrees and the character can detect it
        if (Vector3.Angle(direction, this.transform.forward) < 20)
        {
            this.transform.LookAt(goal.transform.position);
            if (direction.sqrMagnitude > 4)
            {
                Vector3 velocity = direction.normalized * speed * Time.deltaTime;
                this.transform.position = this.transform.position + velocity;
            }
        }
    }

    /* Vector
    void LateUpdate()
    {
        //Both pigs will reach the same location at the same time
        //This is because the direction is smaller for closer pig, so the speed is based on vector length
        //0.01 means that there's 100 iteration before the pig reach its goal (100 * 0.01 = 1)
        //this.transform.position = this.transform.position + direction * 0.01f;


        //Normalizing a vector is reducing the vector length to 1 but keep the same direction
        //This now will make that the closer pig will get to the goal first because of the same length
        //this.transform.position = this.transform.position + direction.normalized;


        //Adding Time.deltaTime means that the speed of the pig is not tied by frame, but by time
        //This means that the pig's speed will be consistent on different pc
        //Vector3 velocity = direction.normalized * speed * Time.deltaTime;
        //this.transform.position = this.transform.position + velocity;

        //We need to stop the pig when they reached the goal
        //Using the magnitude (root of (x*x+y*y+z*z)) we will get the hypotenuse (straight line (length) to the target)
        //As long as the magnitude is lower than 2, the pig is close enough to stop
        direction = goal.transform.position - this.transform.position;
        this.transform.LookAt(goal.transform.position);
        if (direction.magnitude > 2)
        {
            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
    }*/
}
