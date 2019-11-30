using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    float speed;
    float rotateSpeed;

    float horizontal;
    float vertical;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        speed = GetComponent<PlayerModel>().Speed;
        rotateSpeed = GetComponent<PlayerModel>().RotateSpeed;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if(horizontal != 0 || vertical != 0)
        {
            Move();
        }
    }

    void Move()
    {
        rigidBody.velocity = new Vector3(horizontal, 0f, vertical) * speed;
    }
}
