using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jetpack : MonoBehaviour
{
    [Header("[Scene Particles/UI]")]
    public GameObject PlayerPS;
    public ParticleSystem flameWarm;
    public ParticleSystem flameHot;
    public Text fuelUI;

    [Header("[number bits]")]
    public string horizontalInput = "Horizontal_P1";
    public string verticalInput = "Vertical_P1";

    [Header("[number bits]")]
    Rigidbody rb;
    public float fuelLevel;
    public bool jetpackAvailable;
    public bool jetpackRefuel;

	void Start ()
    {
        fuelUI.text = "";

        jetpackAvailable = true;
        jetpackRefuel = false;
        fuelLevel = 100;
        rb = GetComponent<Rigidbody>();

        flameWarm.Stop();
        flameHot.Stop();
    }

    void Update()
    {
        if ((Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticalInput) != 0))
        {
            jetpackRefuel = false;
            fuelLevel--;
        }

        if (jetpackAvailable)
        {
            rb.AddForce(Input.GetAxis(horizontalInput), Input.GetAxis(verticalInput), 0);
        }

        if (jetpackAvailable == false)
        {
            StartCoroutine("JetpackCooldown");
        }

        if (jetpackRefuel)
        {
            fuelLevel++;
        }

        if (fuelLevel < 1)
        {
            fuelLevel = 0;
            jetpackAvailable = false;
        }
        else
        {
            jetpackAvailable = true;
        }

        if (fuelLevel > 100)
             fuelLevel = 100;


        if ((Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticalInput) != 0) && jetpackAvailable)
        {
            Vector2 stickDirection = new Vector2(Input.GetAxis(horizontalInput), Input.GetAxis(verticalInput));
            PlayerPS.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(stickDirection.y / stickDirection.x) * Mathf.Rad2Deg);

            Debug.Log(PlayerPS.transform.rotation);

            flameWarm.Play();
            flameHot.Play();
        }
        else
        {
            flameWarm.Stop();
            flameHot.Stop();
        }


        fuelUI.text = "FUEL: " + fuelLevel + "%".ToString();
    }

    IEnumerator JetpackCooldown()
    {
        yield return new WaitForSeconds(2f);
        jetpackRefuel = true;
    }
}
