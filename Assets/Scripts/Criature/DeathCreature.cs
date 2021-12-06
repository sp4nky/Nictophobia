using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCreature : MonoBehaviour
{
    public void CreatureDeath()
    {
        gameObject.SetActive(false);
    }
}
