using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangePatrolPointsCreature : MonoBehaviour
{
    public GameObject creatureAntes;
    public GameObject creatureDespues;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeCreature();
            gameObject.SetActive(false);
        }
    }

    private void ChangeCreature()
    {
        creatureAntes.SetActive(false);
        creatureDespues.SetActive(true);
    }

}
