using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sum : MonoBehaviour
{
    [SerializeField]
    private ComputeShader _sum_shader;

    [SerializeField]
    [Range(16, 64)]
    private int _size;

    [SerializeField]
    private float[] _value1;
    [SerializeField]
    private float[] _value2;

    [SerializeField]
    private float[] _sum;

    private ComputeBuffer _value1_cb;
    private ComputeBuffer _value2_cb;
    private ComputeBuffer _sum_cb;

    private void Awake()
    {
        _value1 = new float[_size];
        _value2 = new float[_size];
        _sum = new float[_size];

        _value1_cb = new ComputeBuffer(_size, sizeof(float));
        _value2_cb = new ComputeBuffer(_size, sizeof(float));
        _sum_cb = new ComputeBuffer(_size, sizeof(float));
    }

    private void Start()
    {
        _sum_shader.SetBuffer(0, "_value1", _value1_cb);
        _sum_shader.SetBuffer(0, "_value2", _value2_cb);
        _sum_shader.SetBuffer(0, "_sum", _sum_cb);
    }

    private void Update()
    {
        _value1_cb.SetData(_value1);
        _value2_cb.SetData(_value2);
        _sum_shader.Dispatch(0, _size/16, _size/16, 1);
        _sum_cb.GetData(_sum);
    }
}
