using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandController : MonoBehaviour
{
    public GameObject wrench;
    public Transform boltTransform;
    public BoltEventHandler eventHandler;
    public float handSpeed;

    private Rigidbody handRb;
    private float timedVelocity;
    private float heightModFactor;
    private bool canAlterBolt = true;


    // Start is called before the first frame update
    void Start()
    {
        //getting the rigidbody on start to improve perforance / not repeatedly grab later
        handRb = GetComponent<Rigidbody>();

        eventHandler.OnUpdateAllowBoltChange += UpdateAllowBoltChange;
    }

    private void UpdateAllowBoltChange(bool canAlter)
    {
        //to restrict bolt from being adjusted until user has the wrench in correct position
        canAlterBolt = canAlter;
    }

    // Update is called once per frame
    void Update()
    {
        //setting velocity factor to be reusable in later code
        timedVelocity = handSpeed * Time.deltaTime;
        heightModFactor = timedVelocity / 10;

        //movement controls for hand position
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(timedVelocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(timedVelocity, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, timedVelocity);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, timedVelocity);
        }

        //rotation controls for rotating wrench and bolt insync
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (canAlterBolt)
            {
                transform.RotateAround(boltTransform.position, -Vector3.up, timedVelocity);
                //boltTransform.Rotate(new Vector3(5, 0, 0));
                boltTransform.RotateAround(boltTransform.position, -Vector3.up, timedVelocity);

                transform.position += new Vector3(0, heightModFactor, 0);
                boltTransform.position += new Vector3(0, heightModFactor, 0);
            }
        }

        //opposite of above
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (canAlterBolt)
            {
                transform.RotateAround(boltTransform.position, Vector3.up, timedVelocity);
                boltTransform.RotateAround(boltTransform.position, Vector3.up, timedVelocity);

                transform.position += new Vector3(0, -heightModFactor, 0);
                boltTransform.position += new Vector3(0, -heightModFactor, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tool")
        {
            //parenting the wrench so that the hand controlls its position / rotation
            wrench.transform.parent = gameObject.transform;
        }
    }

    private void OnDestroy()
    {
        eventHandler.OnUpdateAllowBoltChange -= UpdateAllowBoltChange;
    }
}
