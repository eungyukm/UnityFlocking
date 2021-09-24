using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    public int spawnCount = 10;
    public enum GizmoType { Never, SelectedOnly, Always }
    public GizmoType showSpawnRegion;
    public float spawnRadius = 10;
    public Color color;

    private void Awake()
    {
        for (int i=0; i<spawnCount; i++)
        {
            // 위치 설정
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            
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
