using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAction : MonoBehaviour
{
    // 세팅 값
    BoidProperty settings;

    // 상태
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // 캐쉬
    Material material;
    Transform cachedTransform;
    Transform target;

    private void Awake()
    {
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = transform;
    }

    // 물고기들 초기화
    public void Initialize(BoidProperty settings, Transform target)
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
        // 가속 벡터
        Vector3 acceleration = Vector3.zero;

        // 타겟이 있는 경우 가속 벡터 설정
        if(target != null)
        {
            Vector3 offsetToTarget = (target.position - position);
            //acceleration
            acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        }

        if(numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * settings.alignWeigh;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if(IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRay();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position = cachedTransform.position;
        forward = dir;
    }

    private Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }


    /// <summary>
    /// 콜리전에 충돌 시 true, 아닐 시 false
    /// </summary>
    /// <returns></returns>
    private bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if(Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 ObstacleRay()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for(int i=0; i<rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);

            // 장애물에 충돌하지 않은 방향 벡터를 리턴
            if(!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }
        return forward;
    }
}
