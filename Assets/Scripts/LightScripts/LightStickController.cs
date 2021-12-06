using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStickController : MonoBehaviour
{
    public float DeadTimner= 3;
    public float CurrentTimer =0;
    public float StartingTimer = 0;

    public AnimationCurve Curva;

    public Light glowLight;
    public float Intensity;

    public SoundBoard SB;
    private AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTimer = StartingTimer;
        glowLight = this.GetComponentInChildren<Light>();
        AS = SB.source;


    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTimer < DeadTimner)
        {
            CurrentTimer += Time.deltaTime;
            float a = CurrentTimer / DeadTimner;
            glowLight.intensity = Curva.Evaluate(a) * Intensity;
           // glowLight.intensity -= Time.deltaTime *3;

        }
        if (CurrentTimer >= DeadTimner)
        {
            glowLight.intensity = 0;
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (!AS.isPlaying)
            {
                AS.Play();
            }
        }
    }
}
