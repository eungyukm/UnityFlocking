using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAction : MonoBehaviour
{
    // ���� ��
    BoidSettings settings;

    // ����
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // ĳ��
    Material material;
    Transform cachedTransform;
    Transform target;

    private void Awake()
    {
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = transform;
    }

    public void Initialize(BoidSettings settings, Transform target)
    {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetColor(Color col)
    {
        if(material != null)
        {
            material.color = col;
        }
    }

    public void UpdateBoid()
    {
        Vector3 acceleration = Vector3.zero;

        if(target != null)
        {
            Vector3 offsetToTarget = (target.position - position);
            //acceleration
        }
    }
}
