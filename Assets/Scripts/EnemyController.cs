using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    public float agentSpeed = 3.5f;
    public bool isSkullMinionEnemy = false;
    public GameObject player;

    public bool isFrozen = false;
    public bool isSleeping = false;
    bool isUnderSpecialCondition = false;
    float specialConditionCounter = 3f;
    public ParticleSystem enemySleepingPS;

    public Transform patrolPointsParent;
    public Transform[] patrolPoints;
    int totalNumberOfPatrolPoints = 0;

    public Transform targetPoint;
    bool isTargetPointSet = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (isSkullMinionEnemy)
        {
            SkullMinionEnemySettings();
        }

        agent.speed = agentSpeed;
        
        if (!isSkullMinionEnemy)
        {
            getPatrolPoints();
            SetTargetPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSkullMinionEnemy)
        {
            CheckIfReachedTargetPatrolPoint();
            if (!isTargetPointSet)
            {
                SetTargetPoint();
            }
        }
        Movement();

        
        if (isUnderSpecialCondition)
        {
            specialConditionCounter -= Time.deltaTime;
            if (specialConditionCounter <= 0)
            {
                NormalCondition();
            }
            if (isSleeping)
            {
                if (enemySleepingPS.isStopped)
                {
                    enemySleepingPS.Play();
                }
                SleepCondition();
                isUnderSpecialCondition = true;
            }
            if (isFrozen)
            {
                FrozenCondition();
                isUnderSpecialCondition = true;
            }
        }
    }

    void SkullMinionEnemySettings()
    {
        agentSpeed = 3f;
        targetPoint = player.transform;
        isTargetPointSet = true;
    }
    void getPatrolPoints()
    {
        totalNumberOfPatrolPoints = patrolPointsParent.childCount;
        patrolPoints = new Transform[totalNumberOfPatrolPoints];
        for (int i = 0; i < totalNumberOfPatrolPoints; i++)
        {
            patrolPoints[i] = patrolPointsParent.GetChild(i);
        }
    }
    void SetTargetPoint()
    {
        targetPoint = patrolPoints[Random.Range(0, totalNumberOfPatrolPoints)];
        isTargetPointSet = true;
    }
    void Movement()
    {
        agent.SetDestination(targetPoint.position);
    }
    
    void CheckIfReachedTargetPatrolPoint()
    {
        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance < 1)
        {
            isTargetPointSet = false;
        }
    }

    public void ToggleSpecialCondition(float counter)
    {
        isUnderSpecialCondition = true;
        specialConditionCounter = counter;
    }
    void SleepCondition()
    {
        agent.speed = 0;
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FrozenCondition()
    {
        agent.speed = 0;
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void NormalCondition()
    {
        
        agent.speed = agentSpeed;
        //rb.constraints = RigidbodyConstraints.None;
        isSleeping = false;
        isFrozen = false;
        isUnderSpecialCondition = false;
        if (enemySleepingPS.isPlaying)
        {
            enemySleepingPS.Stop();
        }
    }
}
