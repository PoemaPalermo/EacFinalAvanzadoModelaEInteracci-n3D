using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBotRaycastDetection : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLenght;
    public LayerMask layerMask;
    SentryBotBehavior sentryBotBehavior;

    // Start is called before the first frame update
    void Awake()
    {
        sentryBotBehavior=GetComponent<SentryBotBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLenght, layerMask))
        {
            Debug.Log("funca");
            sentryBotBehavior.targetTR = hit.collider.transform;
            sentryBotBehavior.isPatrolling = false;
        }
        else
        {
            sentryBotBehavior.isPatrolling = true;
            sentryBotBehavior.targetTR = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLenght);
    }
}
