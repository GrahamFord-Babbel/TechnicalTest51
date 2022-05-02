using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    public Transform ballStart;
    public float timer;
    public Vector3 forceVector;

    private Rigidbody ballRigid;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        ballRigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //a timer to return the ball to its origanl position to reproduce physics wall hit
        if (timer > 2)
        {
            if (gameObject.transform.position != ballStart.position)
            {
                gameObject.transform.position = ballStart.position;
                ballRigid.velocity = Vector3.zero;
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

                //then launch itself into wall

                if(timer > 4)
                {
                    ballRigid.AddRelativeForce(forceVector);

                    //reset timer to loop
                    timer = 0;
                }
            }
        }
    }
}
