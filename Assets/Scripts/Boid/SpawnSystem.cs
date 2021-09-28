using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// 물고기를 만드는 시스템
/// </summary>
public class SpawnSystem : MonoBehaviour
{
    public int spawnCount = 10;
    public enum GizmoType { Never, SelectedOnly, Always }
    public GizmoType showSpawnRegion;
    public float spawnRadius = 10;
    public Color color;

    // 물고기
    public BoidAction prefab;

    private void Awake()
    {
        for (int i=0; i<spawnCount; i++)
        {
            // 위치 설정
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            BoidAction boid = Instantiate(prefab);
            // 랜덤 위치 설정
            boid.transform.position = pos;
            // 반경 1을 갖는 구안의 임의의 지점을 반환합니다.
            boid.transform.forward = Random.insideUnitSphere;
            // 컬러 설정
            boid.SetColor(color);
            
        }
    }


    #region Draw Gizmo
    private void OnDrawGizmos()
    {
        if(showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    private void DrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
    #endregion
}
