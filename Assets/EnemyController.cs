using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [Header("Enemies")]
    public List<GameObject> creatures = new List<GameObject>();
    [Space]
    public GameObject player;

    private bool chaseToDeath;
    private GameObject activedCreature;


    private void FixedUpdate()
    {
        if (activedCreature && !activedCreature.activeSelf)
            activedCreature = null;

        if (!activedCreature) FindActiveCreature();
        else
        {
            WhenNoGlowSticksChasePlayer();
        }
    }

    private void WhenNoGlowSticksChasePlayer()
    {
        CriatureController creatureController = activedCreature.GetComponent<CriatureController>();
        ThrowLight throwLight = player.GetComponent<ThrowLight>();
        if (throwLight.LightQuantity <= 0)
        {

            creatureController.ForceDestination(player.transform.position);
            chaseToDeath = true;
        }
        else if (chaseToDeath)
        {
            creatureController.PlayerLost();
            creatureController.ForceDestination(activedCreature.transform.position);
            chaseToDeath = false;

        }
    }

    private void FindActiveCreature()
    {
        foreach (GameObject creature in creatures)
            if (creature.activeSelf) activedCreature = creature;
    }

}

