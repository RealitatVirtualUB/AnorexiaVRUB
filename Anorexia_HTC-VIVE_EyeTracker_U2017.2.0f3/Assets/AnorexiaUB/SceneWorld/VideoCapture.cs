using System;
using UnityEngine;
using UnityEngine.Rendering;

public class VideoCapture : MonoBehaviour
{
    public string Folder = "ScreenshotFolder";
    public int FrameRate = 25;
    public int FramesToCapture = 100;

    public int Width = 1920;
    public int Height = 1080;

    public Shader DepthShader;

    public bool SaveDepth;

    private Camera _camera;
    private Camera _depthCamera;
    private RenderTexture _renderTexture;
    private RenderTexture _depthRenderTexture;
    private Texture2D _tex;
    private ReflectionProbe[] _probes;

    void Start()
    {
        _camera = GetComponent<Camera>();

        CreateReadTexture();
        // CreateRenderTexture();
        //CreateDepthRenderTexture();
        //CreateDepthCamera();
        //SetupReflectionProbes();

        Time.captureFramerate = FrameRate;
        System.IO.Directory.CreateDirectory(Folder);
    }

    private void CreateReadTexture()
    {
        _tex = new Texture2D(Width, Height, TextureFormat.RGB24, false);
    }

    private void CreateRenderTexture()
    {
        _renderTexture = new RenderTexture(Width, Height, 24, RenderTextureFormat.ARGB32);
    }

    private void CreateDepthRenderTexture()
    {
        _depthRenderTexture = new RenderTexture(Width, Height, 24, RenderTextureFormat.ARGB32);
    }

    private void CreateDepthCamera()
    {
        var depthCameraGameObject = new GameObject("Depth Camera");
        depthCameraGameObject.AddComponent<Camera>();

        _depthCamera = depthCameraGameObject.GetComponent<Camera>();
        _depthCamera.CopyFrom(_camera);
        _depthCamera.SetReplacementShader(DepthShader, null);
    }

    private void SetupReflectionProbes()
    {
        _probes = FindObjectsOfType<ReflectionProbe>();

        foreach (var reflectionProbe in _probes)
        {
            reflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
            reflectionProbe.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;
            reflectionProbe.resolution = 1024;
        }
    }

    void LateUpdate()
    {
        //UpdateReflectionProbes();
        RenderPass();
        //RenderDepthPass();
        //DisplayOnScreen();
        //QuitIfFinished();
    }

    private void DisplayOnScreen()
    {
        _camera.targetTexture = null;
        _camera.Render();
    }

    private void RenderDepthPass()
    {
        if (!SaveDepth) { return; }

        _depthCamera.targetTexture = _depthRenderTexture;
        _depthCamera.Render();
        SaveAfterRender("depth", _depthRenderTexture);
    }

    private void RenderPass()
    {
        // _camera.targetTexture = _renderTexture;
        _camera.Render();
        SaveAfterRender("base", _camera.targetTexture);
    }

    private void QuitIfFinished()
    {
        if (Time.frameCount > FramesToCapture)
        {
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void UpdateReflectionProbes()
    {
        foreach (var reflectionProbe in _probes)
        {
            reflectionProbe.RenderProbe();
        }
    }

    private void SaveAfterRender(string prefix, RenderTexture renderTexture)
    {
        var path = String.Format("{0}/{1}_{2:D04}.jpg", Folder, prefix, Time.frameCount);

        ReadRenderTexture(renderTexture);

        var jpg = _tex.EncodeToJPG();
        System.IO.File.WriteAllBytes(path, jpg);
    }

    private void ReadRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture.active = renderTexture;

        _tex.ReadPixels(new Rect(0.0f, 0.0f, Width, Height), 0, 0);
        _tex.Apply();

        RenderTexture.active = null;
    }
}