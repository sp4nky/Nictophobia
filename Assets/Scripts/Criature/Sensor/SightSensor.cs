using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class SightSensor : MonoBehaviour
{
    private SphereCollider col;
    [SerializeField]
    private List<GameObject> targetsInTrigger = new List<GameObject>();

    [Header("Targets")]
    public float targetAngleHeight = 80;
    public LayerMask layerMask;
    public float distance;
    public UnityEvent OnPlayerSpoted;
    public UnityEvent OnPlayerLost;

    [Header("Debug")]
    public bool drawRay;

    public Vector3 playerLastPosition { get; private set; }
    public bool playerIsVisible { get; private set; }
    public bool stickIsVisible { get; private set; }

    private GameObject player;
    private PlayerController PC;

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        StartCoroutine(UpdateVisibleTargets());

    }
    
    void Update()
    {        
        if (drawRay) DebugVision();
        if (playerIsVisible) playerLastPosition = player.transform.position;

    }

    public void PlayerLost()
    {
        player = null;
        playerIsVisible = false;
        OnPlayerLost.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return; 
        if(!targetsInTrigger.Contains(other.gameObject) && !IsMyself(other.gameObject))
        {
            targetsInTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetsInTrigger.Contains(other.gameObject))
        {
            targetsInTrigger.Remove(other.gameObject);
            if (other.gameObject.CompareTag("Player")) PlayerLost();
        }
    }

    private bool IsMyself(GameObject target)
    {
        return transform.IsChildOf(target.transform);
    }

    private bool IsInFieldOfView(GameObject target)
    {
        if (!target) return false;

        var foward = transform.forward;
        var direction = ((target.transform.position) - transform.position).normalized;
        return Vector3.Angle(foward, direction) < targetAngleHeight;
    }

    private void DebugVision()
    {
        Debug.DrawRay(transform.position, transform.forward * col.radius, Color.yellow);
        for (int i=0; i< targetsInTrigger.Count; i++)
        {
            if (targetsInTrigger[i])
            {
                var foward = transform.forward;
                var direction = ((targetsInTrigger[i].transform.position) - transform.position);

                Color rayColor;
                if (IsInFieldOfView(targetsInTrigger[i]) && HasInLineSight(targetsInTrigger[i]))
                    rayColor = Color.green;
                else
                    rayColor = Color.red;
                Debug.DrawRay(transform.position, direction, rayColor);
            }
        }
    }

    private bool HasInLineSight(GameObject target)
    {
        if (!target) return false;
        var direction = ((target.transform.position) - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, col.radius, layerMask))
        {
            if (hit.collider.gameObject == target.gameObject) return true;
        }
        return false;

    }

    private IEnumerator UpdateVisibleTargets()
    {
        while (true)
        {
            for(int i=0; i< targetsInTrigger.Count;i++)
            {
                if (!targetsInTrigger[i])
                {
                    targetsInTrigger.Remove(targetsInTrigger[i]);
                    break;
                }
                if (IsInFieldOfView(targetsInTrigger[i]) && (PlayerHasEnoughNear(targetsInTrigger[i]) || HasInLineSight(targetsInTrigger[i])))
                {
                    OnPlayerSpoted.Invoke();
                    player = targetsInTrigger[i];
                    playerIsVisible = true;
                }
                yield return null;
            }
            yield return null;

        }
    }

    private bool PlayerHasEnoughNear(GameObject target)
    {
        return (Vector3.Distance(transform.position, target.transform.position) < col.radius / 2) && target.tag == "Player";
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color color = Color.cyan;
        UnityEditor.Handles.color = color;
        float angle = targetAngleHeight * Mathf.PI / 180f;
        float centerDistance = distance * Mathf.Cos(angle);
        Vector3 arcCenter = transform.position + transform.forward.normalized * centerDistance;
        UnityEditor.Handles.DrawWireDisc(arcCenter, transform.forward.normalized, distance);
        UnityEditor.Handles.DrawLine(transform.position, arcCenter + transform.right.normalized * distance);
        UnityEditor.Handles.DrawLine(transform.position, arcCenter + transform.up.normalized * distance);
        UnityEditor.Handles.DrawLine(transform.position, arcCenter + -transform.right.normalized * distance);
        UnityEditor.Handles.DrawLine(transform.position, arcCenter + -transform.up.normalized * distance);
    }
#endif
}
