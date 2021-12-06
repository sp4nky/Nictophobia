using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectForwardObject: MonoBehaviour
{

	public string tagNameDetected { get; set; }
	public GameObject objectDetected { get; set; }
	public Transform cameraTransform;
	public float distance = 5f;
	public LayerMask layer;

	public bool debug;

	void Update()
	{
		objectDetected = DetectObjectRayCast(cameraTransform, layer);
		if(objectDetected)
			tagNameDetected = objectDetected.tag;
		else
			tagNameDetected = "";
	}
	
	private GameObject DetectObjectRayCast(Transform cameraTransform, LayerMask layer)
	{
		GameObject detectObject = null;
		RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, distance, layer);
		if (hits.Length > 0)
        {
			detectObject = hits[0].collider.gameObject;
			if (debug) Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.green, Time.deltaTime);

		}
		return detectObject;
	}
}