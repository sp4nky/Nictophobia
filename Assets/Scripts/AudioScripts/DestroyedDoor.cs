using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedDoor : MonoBehaviour
{
    public SoundBoard SB;

    private void OnDestroy()
    {
        SB.source.Play();
    }

}
