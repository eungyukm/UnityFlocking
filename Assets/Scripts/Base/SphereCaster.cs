using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    public GameObject currentHitOjbect;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layeMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;
        if(Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layeMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitOjbect = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitOjbect = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
