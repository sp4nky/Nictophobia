using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public partial class CriatureController : MonoBehaviour
{
    public SightSensor sensor;
    [Space]
    [Header("Patrol")]
    public Path path;
    public float patrolSpeed = 5f;

    public GameObject parentPatrolPoints;
    
    [Header("Chase")]
    public float chaseSpeed = 7f;
    public float timeBeforeChase = 1f;

    [Header("Stun")]
    public float stunedTime = 5f;
    public UnityEvent onCriatureStun;

    private NavMeshAgent agent;
    private State state;
    private CreatureAnimationSettings anim;

    public AudioSource footAudio;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<CreatureAnimationSettings>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetMaxSpeed(chaseSpeed);
        agent.autoBraking = false;
        state = new Patrol(this);
    }

    private void FixedUpdate()
    {
        state.Update();
    }

    public void SetState(State newState)
    {
        if (state != null) state.OnStateExit();
        this.state = newState;
        state.OnStateEnter();
    }

    private bool PlayerIsVisible() => sensor.playerIsVisible;
    private void SetCriatureDestination() => agent.SetDestination(sensor.playerLastPosition);

    private void SetDefaultDestination()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;
            agent.isStopped = false;
        }
    }

    void GotoNextPoint()
    {
        StartCoroutine(StopAnGoToNextPoint());
    }

    private IEnumerator StopAnGoToNextPoint()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(2);
        agent.destination = path.GetNextPoint();
        agent.isStopped = false;

    }

    private bool HavePatrolsPoints() => !agent.pathPending && agent.remainingDistance < 0.5f;


    private void StunCreature()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        PlayerLost();
        anim.StunAnimation();
    }

    private void WakeUpCreature()
    {
        anim.WakeUpAnimation();
        agent.isStopped = false;
    }

    public void ChangeToChaseVelocity()
    {
        StartCoroutine(WaitAndChase());
    }

    IEnumerator WaitAndChase()
    {
        agent.isStopped = true;
        anim.SpotAnimation();
        yield return new WaitForSeconds(timeBeforeChase);
        agent.isStopped = false;
        agent.speed = chaseSpeed;
    }

    public void ChangeToPatrolVelocity()
    {
        agent.speed = patrolSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("GlowStick") && !(state is RunAway))
        {
            SetState(new RunAway(this));
        }
    }

}
