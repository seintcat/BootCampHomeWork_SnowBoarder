using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float torqueAmount = 10f;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float torqueDamp = 10f;
    [SerializeField]
    private SurfaceEffector2D _effector;
    [SerializeField]
    private float boostSpeed = 20f;
    [SerializeField]
    private ParticleSystem boost;

    private float baseSpeed;
    private bool ground;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = _effector.speed;
        ground = true;
    }

    // Update is called once per frame
    void Update()
    {
        JumpPlayer();
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            boost.Stop();
        }
    }
    private void FixedUpdate()
    {
        RotatePlayer();
        BoostPlayer();
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.AddTorque(torqueAmount * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.AddTorque(-torqueAmount * Time.fixedDeltaTime);
        }

        if (
                !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.LeftArrow) &&
                !Input.GetKey(KeyCode.D) &&
                !Input.GetKey(KeyCode.RightArrow)
            )
        {
            _rigidbody.angularVelocity = Mathf.Lerp(0, _rigidbody.angularVelocity, 1 - (Time.fixedDeltaTime * torqueDamp));
        }
    }

    private void BoostPlayer()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _effector.speed = boostSpeed;
        }
        else
        {
            _effector.speed = baseSpeed;
        }
    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            _rigidbody.AddForce(Vector2.up * 500);
            ground = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                boost.Play();
            }
        }
    }
}
