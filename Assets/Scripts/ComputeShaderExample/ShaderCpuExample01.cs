using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCpuExample01 : MonoBehaviour
{
    // 1. ������ ��ǻ�� ��ǻƮ ���̴� 
    public ComputeShader computeShader;

    // 2. ������ Ŀ���� �ε���(���̴��� ����� �Լ��� �ε��� ����)
    private int kernelIndex_FunctionSample;

    // 3. GPU�� ���� �������� �޾ƿ� ������ ����
    public ComputeBuffer intComputeBuffer;

    // Start is called before the first frame update
    void Start()
    {
        // 1. ������ Ŀ���Լ��� �ε����� �޾ƿɴϴ�.
        // (���ڰ�����, Ŀ���Լ��� ����� Ŀ���Լ� �̸��� �Ҵ��մϴ�.)
        kernelIndex_FunctionSample = computeShader.FindKernel("GPU_FunctionSample");

        Debug.Log("kernelIndex_FunctionSample : " + kernelIndex_FunctionSample);

        // 2. GPU�� �������� GPU���� �޾ƿ��� ����, ���۰����� �Ҵ��մϴ�.
        intComputeBuffer = new ComputeBuffer(1024, sizeof(int));

        // 3. �ش� Ŀ���Լ��� ���� ����� �����ϴ� ���� "intBuffer"�� ����, �ش� ��ũ��Ʈ���� �����
        // intComputeBuffer�� �����մϴ�.
        computeShader.SetBuffer(kernelIndex_FunctionSample, "intBuffer", intComputeBuffer);

        // 4. CPU��ũ�������� ��ǻƮ ���̴��� Ư�� ���� �����ϴ� ���, ���⼱ ���� 1�� �����մϴ�.
        computeShader.SetInt("intValue", 1);

        // 5. ��ǻƮ ���̴��� �����մϴ�.
        // ������ Ŀ���� �ε�����, ������ �׷��� ��(3����)�� �����մϴ�. ���⼱ �Ѱ��� �׷� ����
        computeShader.Dispatch(kernelIndex_FunctionSample, 1024, 1, 1);

        // 6. ������� ������ �ӽ� �迭�� ����ϴ�.
        int[] result = new int[1024];

        // 7. ��ǻƮ ���̴� Ŀ���� ���� ����� �����ɴϴ�.
        intComputeBuffer.GetData(result);

        // 8. ����� Ȯ��
        for (int i = 0; i < 1024; i++)
        {
            Debug.Log(result[i]);
        }

        // 9. ����� ComputeBuffer�� �޸� �Ҵ� ������ �ʿ��մϴ�.
        intComputeBuffer.Release();
    }
}
