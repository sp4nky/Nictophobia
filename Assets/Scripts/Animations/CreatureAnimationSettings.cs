using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class CreatureAnimationSettings : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private float maxSpeed;

    public GameObject CajaDeVoz;
    private AudioSource AS;
    private SoundBoard SB;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        AS = CajaDeVoz.GetComponent<AudioSource>();
        SB = CajaDeVoz.GetComponent<SoundBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = agent.velocity.magnitude / maxSpeed;
        anim.SetFloat("speed", speed);
    }

    public void StunAnimation()
    {
        anim.Rebind();
        anim.SetTrigger("stun");
    }

    public void WakeUpAnimation()
    {
        anim.SetTrigger("wake");
    }

    public void SpotAnimation()
    {
        anim.SetTrigger("spot");
    }

    public void SetMaxSpeed(float chaseSpeed) => maxSpeed = chaseSpeed;

    public void Play()
    {
        print("I scream");
        SB.source.Play();
    }
}
