using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EngineGenerator : MonoBehaviour
{
    public Material powerOnMaterial;
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public void StartEngine()
    {
        MeshRenderer meshGenerator = GetComponent<MeshRenderer>();
        Material[] materials = new Material[5]; 
        meshGenerator.materials.CopyTo(materials,0);
        materials[2] = powerOnMaterial;
        materials[4] = powerOnMaterial;
        meshGenerator.materials = materials;
        audio.loop = true;
        audio.Play();
    }
}
