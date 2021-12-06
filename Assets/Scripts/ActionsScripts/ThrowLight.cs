using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLight : MonoBehaviour
{
    public int LightQuantity;
    public GameObject LightStickPrefab;
    public Transform PlayerTransform;
    public Transform Camera;
    public AudioSource AS;

    private bool Click;
    public UIController HUD;

    public GameObject PlayerFOV;


    public float ThrowForce = 10f;
    public float Fly = 5f;
    public Vector3 objectPos;


    private float x;
    private float y;
    private float z;
    private bool canThrow = true;

    // Start is called before the first frame update
    void Start()
    {
        HUD = FindObjectOfType<UIController>();
        HUD.SetHUD(LightQuantity);

        AS = PlayerTransform.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Click = Input.GetButtonDown("Fire2");


        if (Click && canThrow)
        {
            if (LightQuantity > 0)
            {
                AS.Play();
                RandomPick();
                SpawnStick();

            }
        }

        if (LightQuantity <= 0)
        {
            PlayerFOV.SetActive(true);
        }
    }

    public void SpawnStick()
    {
        GameObject LightStick = Instantiate(LightStickPrefab, PlayerTransform.position, PlayerTransform.rotation);
        Rigidbody rbLS = LightStick.GetComponent<Rigidbody>();
        rbLS.velocity = (Camera.forward * ThrowForce + Camera.up *Fly);
        rbLS.angularVelocity = new Vector3(x, y, z);
        LightQuantity -= 1;
        HUD.SetHUD(LightQuantity);
    }


    public void RandomPick()
    {
        x = Random.Range(0, 9);
        y = Random.Range(0, 9);
        z = Random.Range(0, 9);
    }

    public void AddSticks(int count)
    {
        LightQuantity += count;
        HUD.SetHUD(LightQuantity);
    }

    public void EnableThrowStick()
    {
        canThrow = true;
    }
    public void DisableThrowStick()
    {
        canThrow = false;
    }

}
