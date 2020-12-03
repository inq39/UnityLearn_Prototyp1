using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float _pS = 25000.0f;
   [SerializeField] private int _speed;
   [SerializeField] private int _rPm;
   [SerializeField] private float _turnSpeed = 1.0f;
   [SerializeField] private GameObject _centerOfMass;
   [SerializeField] private TextMeshProUGUI _speedoMeter;
   [SerializeField] private TextMeshProUGUI _rPmMeter;
   [SerializeField] private List<WheelCollider> wheels;
   private float _horizontalInput;
   private float _forwardInput;
   private Rigidbody _playerRb;
   
    


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.centerOfMass = _centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the vehicle forward
        if (PlayerOnGround())
        {                
        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");

        _playerRb.AddRelativeForce(Vector3.forward  * _pS * _forwardInput);
        transform.Rotate(Vector3.up * _turnSpeed * _horizontalInput * Time.deltaTime);

        _speed = Mathf.RoundToInt(_playerRb.velocity.magnitude * 3.6f);
        _speedoMeter.SetText("Speed: " + _speed + " km/h");

        _rPm = Mathf.RoundToInt(_speed % 30) * 40;
        _rPmMeter.SetText("RPM: " + _rPm);
        }
    }

    bool PlayerOnGround()
    {
        int wheelsOnGround = 0;
        foreach (WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
