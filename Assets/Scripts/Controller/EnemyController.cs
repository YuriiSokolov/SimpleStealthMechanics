using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    float speed;
    float viewAngle;
    float viewDistance;
    NavMeshAgent nav;
    Light eyes;

    float distanceToTarget;
    Transform target;
    Vector3 lastTargetPos; //Последняя позиция цели
    bool lastTargetPosWasChecked = true;
    [SerializeField]
    float pursuitTime = 1.5f; //Время которое противник будет искать цель, после того как заметил, даже если противник скрылся.
    float currentPursuitTime = 0f;

    [SerializeField]
    Vector3[] patrolPath;
    int wayPointId = 0;

    public LayerMask viewMask;

    Color patrolColor = Color.green;
    Color foundTargetColor = Color.red;

    void Start()
    {
        speed = GetComponent<EnemyModel>().Speed;
        viewAngle = GetComponent<EnemyModel>().ViewAngle;
        viewDistance = GetComponent<EnemyModel>().ViewDistance;
        eyes = transform.GetChild(0).GetComponent<Light>();
        eyes.spotAngle = viewAngle;
        eyes.range = viewDistance;
        eyes.color = patrolColor;

        StartCoroutine(FindTarget());

        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;

        nav.enabled = true;
    }

    void Update()
    {
        Move();
        if(currentPursuitTime > 0)
        {
            currentPursuitTime -= Time.deltaTime;
        }
    }

    IEnumerator FindTarget()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        yield return null;
    }

    bool CanSeeTarget()
    {
        if (Vector3.Distance(transform.position, target.position) <= viewDistance)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float angelBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToTarget);
            if (angelBetweenEnemyAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, target.position, viewMask))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void Move()
    {
        if (CanSeeTarget())
        {     
            eyes.color = foundTargetColor;
            nav.SetDestination(target.position);
            lastTargetPos = target.position;
            lastTargetPosWasChecked = false;
            currentPursuitTime = pursuitTime;
        }
        else if (currentPursuitTime > 0)
        {
            nav.SetDestination(target.position);
        }
        else if (lastTargetPosWasChecked == false)
        {
            Vector3 direction = transform.position - lastTargetPos;
            direction.y = 0;
            if (direction.magnitude <= 0)
            {
                lastTargetPosWasChecked = true;
            }
            nav.SetDestination(lastTargetPos);
        }
        else
        {
            eyes.color = patrolColor;
            if (patrolPath != null && patrolPath.Length > 0)
            {
                Vector3 direction = transform.position - patrolPath[wayPointId];
                direction.y = 0;
                if (direction.magnitude <= 0)
                {
                    wayPointId++;
                    if(wayPointId == patrolPath.Length)
                    {
                        wayPointId = 0;
                    }
                }
                nav.SetDestination(patrolPath[wayPointId]);
            }
        }
    }
}
