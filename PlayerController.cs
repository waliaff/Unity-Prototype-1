using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float rpm;
    [SerializeField] private float horsePower = 0;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] private GameObject centerofMass;
    [SerializeField] TextMeshProUGUI speedometer;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    private int wheelCount;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerofMass.transform.position;
    }

    void FixedUpdate()
    {
        // We get player control inputs here
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // We'll move the vehicle forward by coding here
        
        if (isonGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput); // adds force 'in local' orientation
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f); // multiply my 2.237 for mph
            speedometer.SetText("Speed: " + speed + " km/h");
            rpm = Mathf.Round((speed % 30) * 150);
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool isonGround()
    {
        wheelCount = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelCount++;
            }
        }
        if (wheelCount == 4)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
