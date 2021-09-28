using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCpuExample01 : MonoBehaviour
{
    // 1. 접근할 컴퓨할 컴퓨트 셰이더 
    public ComputeShader computeShader;

    // 2. 실행할 커널의 인덱스(쉐이더에 선언된 함수의 인덱스 정보)
    private int kernelIndex_FunctionSample;

    // 3. GPU로 부터 연산결과를 받아와 저장할 버퍼
    public ComputeBuffer intComputeBuffer;

    // Start is called before the first frame update
    void Start()
    {
        // 1. 실행할 커널함수의 인덱스를 받아옵니다.
        // (인자값으로, 커널함수에 선언된 커널함수 이름을 할당합니다.)
        kernelIndex_FunctionSample = computeShader.FindKernel("GPU_FunctionSample");

        Debug.Log("kernelIndex_FunctionSample : " + kernelIndex_FunctionSample);

        // 2. GPU의 연산결과를 GPU에서 받아오기 위해, 버퍼공간을 할당합니다.
        intComputeBuffer = new ComputeBuffer(1024, sizeof(int));

        // 3. 해당 커널함수의 실행 결과를 저장하는 버퍼 "intBuffer"의 값을, 해당 스크립트에서 선언된
        // intComputeBuffer에 저장합니다.
        computeShader.SetBuffer(kernelIndex_FunctionSample, "intBuffer", intComputeBuffer);

        // 4. CPU스크립으에서 컴퓨트 쉐이더로 특정 값을 전달하는 방식, 여기선 숫자 1을 전달합니다.
        computeShader.SetInt("intValue", 1);

        // 5. 컴퓨트 쉐이더를 실행합니다.
        // 실행할 커널의 인덱스와, 실행할 그룹의 수(3차원)을 지정합니다. 여기선 한개의 그룹 실행
        computeShader.Dispatch(kernelIndex_FunctionSample, 1024, 1, 1);

        // 6. 결과값을 저장할 임시 배열을 만듭니다.
        int[] result = new int[1024];

        // 7. 컴퓨트 쉐이더 커널의 실행 결과를 가져옵니다.
        intComputeBuffer.GetData(result);

        // 8. 결과값 확인
        for (int i = 0; i < 1024; i++)
        {
            Debug.Log(result[i]);
        }

        // 9. 사용한 ComputeBuffer는 메모리 할당 해제가 필요합니다.
        intComputeBuffer.Release();
    }
}
