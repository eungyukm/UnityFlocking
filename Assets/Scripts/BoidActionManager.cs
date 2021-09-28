using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidActionManager : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public BoidProperty settings;
    // 컴퓨트 쉐이더 선언
    public ComputeShader compute;

    // 물고기들
    BoidAction[] boids;

    void Start()
    {
        boids = FindObjectsOfType<BoidAction>();
        foreach(BoidAction b in boids)
        {
            b.Initialize(settings, null);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(boids != null)
        {
            int numBoids = boids.Length;
            var boidData = new BoidData[numBoids];

            for(int i=0; i<boids.Length; i++)
            {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);


        }
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCenter;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
