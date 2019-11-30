using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float rotateSpeed = 3.0f;

    public float Speed
    {
        get
        {
            return speed;
        }
    } 

    public float RotateSpeed
    {
        get
        {
            return rotateSpeed;
        }
    }
}
