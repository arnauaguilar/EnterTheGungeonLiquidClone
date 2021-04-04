using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    public ComputeShader computeShader;

    static public RenderTexture computeRenderTexture;
    static public RenderTexture liquidRenderTexture;
    static public RenderTexture obstacleRenderTexture;

    private int updateKernelID = -1;
    private int diffuseKernelID = -1;
    private int clearAgelID = -1;

    public Material displayMaterial;
    public Material debugMat1;
    public Material debuMat2;

    public Camera liquidCamera;
    public Camera obstacleCamera;

    public float diffuseSpeed = 1;
    public float expandSpeed = 1;
    public float stopSpreadingLifetime;

    void Start()
    {
        
        GetKernels();

        Vector2Int resolution = (new Vector2Int(1024, 1024));
        CreateRenderTexture(ref computeRenderTexture, resolution, 24);
        CreateRenderTexture(ref liquidRenderTexture, resolution, 24);
        CreateRenderTexture(ref obstacleRenderTexture, resolution, 24);

        liquidCamera.targetTexture = liquidRenderTexture;
        obstacleCamera.targetTexture = obstacleRenderTexture;
        
        computeShader.SetTexture(updateKernelID, "Result", computeRenderTexture);
        computeShader.SetTexture(updateKernelID, "liquidRenderTexture", liquidRenderTexture);
        computeShader.SetTexture(updateKernelID, "obstacleRenderTexture", obstacleRenderTexture);

        computeShader.SetTexture(clearAgelID, "Result", computeRenderTexture);
        
        computeShader.SetTexture(diffuseKernelID, "Result", computeRenderTexture);
        computeShader.SetTexture(diffuseKernelID, "liquidRenderTexture", liquidRenderTexture);
        computeShader.SetTexture(diffuseKernelID, "obstacleRenderTexture", obstacleRenderTexture);
        
        
        computeShader.SetVector("resolution", new Vector4(resolution.x, resolution.y ,0,0));
        computeShader.Dispatch(clearAgelID, computeRenderTexture.width / 8, computeRenderTexture.height / 8, 1);
        
        displayMaterial.mainTexture = computeRenderTexture;
        debugMat1.mainTexture = liquidRenderTexture;
        debuMat2.mainTexture = obstacleRenderTexture;
    }

    public void GetKernels()
    {
        updateKernelID = computeShader.FindKernel("Update");
        diffuseKernelID = computeShader.FindKernel("Diffuse");
        clearAgelID = computeShader.FindKernel("ClearAge");
    }

    public void CreateRenderTexture(ref RenderTexture rt, Vector2Int size, int depth)
    {
        rt = new RenderTexture(size.x,size.y,depth);
        rt.enableRandomWrite = true;
        rt.Create();
    }

    public static RenderTexture GetComputedTexture()
    {
        return computeRenderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        computeShader.SetFloat("dt", Time.deltaTime);
        computeShader.SetFloat("expandSpeed", expandSpeed);
        computeShader.SetFloat("diffuseSpeed", diffuseSpeed);
        computeShader.SetFloat("stopSpreadingLifetime", stopSpreadingLifetime);

        computeShader.Dispatch(updateKernelID, computeRenderTexture.width / 8, computeRenderTexture.height / 8, 1);
        computeShader.Dispatch(diffuseKernelID, computeRenderTexture.width / 8, computeRenderTexture.height / 8, 1);
    }
}
