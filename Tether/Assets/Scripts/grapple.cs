using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    [Header("[number bits]")]
    public string fireInput = "Fire1_P1";
    public string detachInput = "Fire2_P1";

    [Header("Player Objects")]
    public GameObject player;
    public Rigidbody playerrb;

    [Header("Grapple Tool Objects")]
    public GameObject hook;
    private Rigidbody hookrb;
    public GameObject hookHolster;
    public LineRenderer rope;

    [Header("Grapple Interactable Objects")]
    public GameObject hookedObject;
    public GameObject hookedPlayer;
    public GameObject grabbedObject;

    [Header("Speed Values")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;

    [Header("Grapple State Bools")]
    public bool isFired = false;
    public bool playerHooked = false;
    public bool objectHooked = false;
    public bool objectGrabbed = false;

    void Start ()
    {
        rope = hook.GetComponent<LineRenderer>();
        hookrb = hook.GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
    //launch grapple
        //if not fired, fire.
        if (Input.GetButtonDown(fireInput) && isFired == false)
            isFired = true;

        //if is fired, reset position and fire.
        if (Input.GetButtonDown(fireInput) && isFired)
        {
            RetractHook();
            isFired = true;
        }

        //reset hook position.
        if (Input.GetButtonDown(detachInput))
        {
            RetractHook();
        }


        //while fired.
        if (isFired)
        {
            //set line renderer vertices
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolster.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }



        //pull in object
        if (objectGrabbed)
        {
            grabbedObject.transform.parent = hook.transform;

            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolster.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHolster = Vector3.Distance(hook.transform.position, hookHolster.transform.position);

            if (distanceToHolster < 0.5f)
            {
                Destroy(grabbedObject);
                grabbedObject = null;
                objectGrabbed = false;
                isFired = false;
                rope.SetVertexCount(0);
                hook.transform.position = hookHolster.transform.position;
            }
        }
    }

    void RetractHook()
    {
        hook.transform.rotation = hookHolster.transform.rotation;
        hook.transform.position = hookHolster.transform.position;
        hook.transform.parent = hookHolster.transform;
        isFired = false;

        rope.SetVertexCount(0);
    }
}
