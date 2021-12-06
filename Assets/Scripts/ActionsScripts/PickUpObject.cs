using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpObject : MonoBehaviour
{
    [Header("Settings")]
    public string pickUpInputButton = "Fire1";
    public LayerMask objectLayerMask;
    public int maxDistance = 10;
    public Transform firstPersonCamera;
    public FixedJoint joint;

    private GameObject selectedObject = null;

    private bool input;

    void Update()
    {
        input = Input.GetButton(pickUpInputButton);

        if (selectedObject == null)
            FindObject();
        if (!input)
        {
            selectedObject = null;
            joint.connectedBody = null;
        }
    }

    private void FindObject()
    {
        RaycastHit[] hits = Physics.RaycastAll(firstPersonCamera.position, firstPersonCamera.forward, maxDistance, objectLayerMask);

        if (hits.Length > 0)
        {
            if (input)
                SelectObject(hits);

        }
    }

    private void SelectObject(RaycastHit[] hits)
    {
        selectedObject = hits[0].transform.gameObject;
        Rigidbody rbObject = selectedObject.GetComponent<Rigidbody>();
        if (rbObject)
        {
            joint.connectedBody = rbObject;
        }
    }
}
