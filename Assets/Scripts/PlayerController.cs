using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [Header("Velocity Settings")]
    public float fowardVelocity = 0.02f;
    public float horizontalVelocity = 0.02f;
    public LayerMask groundLayer;
    public AudioSource AS;

    private CharacterController characterController;
    private FristPersonLook firstPersonLook;
    private WalkingShake walkingShake;
    private float horizontal;
    private float vertical;
    private bool canMove;
    private Vector3 playerVelocity;
    private DetectForwardObject detectForwardObject;
    private ThrowLight throwLight;
    private bool death;

    [Header("Breath Settings")]
    public GameObject Head;
    public AudioSource HardBreath;
    public bool Spotted;
    public AudioSource Breath;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        walkingShake = GetComponent<WalkingShake>();
        firstPersonLook = GetComponent<FristPersonLook>();
        detectForwardObject = GetComponent<DetectForwardObject>();
        throwLight = GetComponent<ThrowLight>();
    }

    private void Start()
    {
        GameController.Instance.playerController = this;
        canMove = true;
        Breath = Head.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (playerVelocity.y < 0.1f && IsGrounded())
            playerVelocity.y = 0;
        ApplyGravity();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (InputMove() && canMove)
        {
            Move();
        }
        else
        {
            if (walkingShake.shaking) walkingShake.StopShake();
            AS.Stop();
        }

        if (Spotted)
        {
            Breath.Stop();
            if (!HardBreath.isPlaying)
            {
                HardBreath.Play();
            }
        }
        else if (!Spotted)
        {
            HardBreath.Stop();
            if (!Breath.isPlaying)
            {
                Breath.Play();
            }
        }


    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.localPosition, Vector3.down);
        return Physics.SphereCast(ray, 0, .1f, groundLayer);
    }

    private void ApplyGravity() => characterController.SimpleMove(playerVelocity);

    private bool InputMove()
    {
        return Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
    }

    private void Move()
    {
        Vector2 input = new Vector2(horizontal, vertical);
        input = Vector2.ClampMagnitude(input, 1f);
        Vector3 movement;
        if(vertical < 0)
            movement = transform.forward * input.y * fowardVelocity/2 * Time.deltaTime + transform.right * input.x * horizontalVelocity * Time.deltaTime;
        else
            movement = transform.forward * input.y * fowardVelocity * Time.deltaTime + transform.right * input.x * horizontalVelocity * Time.deltaTime;
        characterController.Move(movement);
        if (!walkingShake.shaking) walkingShake.StartShake();
        if (!AS.isPlaying)
        {
            AS.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Criature") && !death)
        {
            death = true;
            GameController.Instance.Die();
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        firstPersonLook.DisableMovement();
        throwLight.DisableThrowStick();
    }

    public void EnableMovement()
    {
        canMove = true;
        firstPersonLook.EnableMovement();
        throwLight.EnableThrowStick();
    }

    public string GetTagForwardObject()
    {
        return detectForwardObject.tagNameDetected;
    }
    public GameObject GetForwardObject()
    {
        return detectForwardObject.objectDetected;
    }

    public void AddGlowSticks(int count)
    {
        throwLight.AddSticks(count);
    }

    public void PlayerSpotted()
    {
        Spotted = true;
    }
    public void PlayerLost()
    {
        Spotted = false;
    }

}
