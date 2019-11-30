using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float viewAngle = 75f;
    [SerializeField]
    float viewDistance = 10f;

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public float ViewAngle
    {
        get
        {
            return viewAngle;
        }
    }

    public float ViewDistance
    {
        get
        {
            return viewDistance;
        }
    }
}
