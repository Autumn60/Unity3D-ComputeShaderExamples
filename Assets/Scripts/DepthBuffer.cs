using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthBuffer : MonoBehaviour
{
    [SerializeField]
    private ComputeShader _depthBuffer_shader;

    [SerializeField]
    private Camera _cam;

    [SerializeField] private Vector2Int _resolution = new Vector2Int(640, 480);

    [SerializeField] private float[] _distance;

    private RenderTexture _rt_color = null;
    private RenderTexture _rt_depth = null;

    private ComputeBuffer _distance_cb;


    private void Awake()
    {
        _distance = new float[_resolution.x*_resolution.y];
        _distance_cb = new ComputeBuffer(_resolution.x*_resolution.y, sizeof(float));
    }

    private void Start()
    {
        _rt_color = new RenderTexture(_resolution.x, _resolution.y, 0, RenderTextureFormat.ARGB32);
        _rt_depth = new RenderTexture(_resolution.x, _resolution.y, 32, RenderTextureFormat.Shadowmap);
        
        _cam.SetTargetBuffers(_rt_color.colorBuffer, _rt_depth.depthBuffer);

        float n_inv = 1.0f / _cam.nearClipPlane;
        float f_inv = 1.0f / _cam.farClipPlane;

        _depthBuffer_shader.SetFloat("n_f", n_inv - f_inv);
        _depthBuffer_shader.SetFloat("f", f_inv);
        _depthBuffer_shader.SetInt("width", _resolution.x);
        _depthBuffer_shader.SetTexture(0, "depthBuffer", _rt_depth);
        _depthBuffer_shader.SetTexture(0, "colorBuffer", _rt_color);
        _depthBuffer_shader.SetBuffer(0, "distance", _distance_cb);
    }

    private void OnPostRender()
    {
        _depthBuffer_shader.Dispatch(0, _resolution.x / 4, _resolution.y / 4, 1);
        _distance_cb.GetData(_distance);
    }
}
