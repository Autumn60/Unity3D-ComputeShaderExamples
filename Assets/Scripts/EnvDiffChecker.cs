using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvDiffChecker : MonoBehaviour
{
    [Flags]
    enum Info
    {
        None = 0,
        GraphicsMultiThreaded                           = 1 << 0,
        GraphicsUVStartsAtTop                           = 1 << 1,
        HasDynamicUniformArrayIndexingInFragmentShaders = 1 << 2,
        HasHiddenSurfaceRemovalOnGPU                    = 1 << 3,
        HasMipMaxLevel                                  = 1 << 4,
        Supports2DArrayTextures                         = 1 << 5,
        Supports32bitsIndexBuffer                       = 1 << 6,
        Supports3DRenderTextures                        = 1 << 7,
        Supports3DTextures                              = 1 << 8,
        SupportsAccelerometer                           = 1 << 9,
        SupportsAsyncCompute                            = 1 << 10,
        SupportsAsyncGPUReadback                        = 1 << 11,
        SupportsAudio                                   = 1 << 12,
        SupportsCompressed3DTextures                    = 1 << 13,
        SupportsComputeShaders                          = 1 << 14,
        SupportsConservativeRaster                      = 1 << 15,
        SupportsCubemapArrayTextures                    = 1 << 16,
        SupportsGeometryShaders                         = 1 << 17,
        SupportsGpuRecorder                             = 1 << 18,
        SupportsGraphicsFence                           = 1 << 19,
        SupportsGyroscope                               = 1 << 20,
        SupportsHardwareQuadTopology                    = 1 << 21,
        SupportsInstancing                              = 1 << 22,
        SupportsLocationService                         = 1 << 23,
        SupportsMipStreaming                            = 1 << 24,
        SupportsMotionVectors                           = 1 << 25,
        SupportsMultisampleAutoResolve                  = 1 << 26,
        SupportsMultisampled2DArrayTextures             = 1 << 27,
        SupportsMultiview                               = 1 << 28,
        SupportsRawShadowDepthSampling                  = 1 << 29,
        SupportsRayTracing                              = 1 << 30,
        SupportsRenderTargetArrayIndexFromVertexShader  = 1 << 31,
        SupportsSeparatedRenderTargetsBlend             = 1 << 32,
        SupportsSetConstantBuffer                       = 1 << 33,
        SupportsShadows                                 = 1 << 34,
        SupportsSparseTextures                          = 1 << 35,
        SupportsStoreAndResolveAction                   = 1 << 36,
        SupportsTessellationShaders                     = 1 << 37,
        SupportsVibration                               = 1 << 38,
        UsesLoadStoreActions                            = 1 << 39,
        UsesReversedZBuffer                             = 1 << 40
    }

    [SerializeField] private Info _myInfo;
    [SerializeField] private ulong _myInfo_value;
    [SerializeField] private ulong _compareInfo_value;
    [SerializeField] private Info _info_diff;

    [Header("Debug")]

    [SerializeField] private Info _debugInfo2Value;
    [SerializeField] private ulong _debugValue;

    [SerializeField] private ulong _debugValue2Info;
    [SerializeField] private Info _debugInfo;

    private void Awake()
    {
        _debugValue = (ulong)_debugInfo2Value;
        _debugInfo = (Info)_debugValue2Info;

        string name = string.Empty;
        int maxLength = Enum.GetNames(typeof(Info)).Max(v => v.Length);

        foreach (Info value in Enum.GetValues(typeof(Info)))
        {
            name = string.Format("{0, -" + maxLength + "}", value.ToString());
            Console.WriteLine("{0}: {1}", name, Convert.ToString((byte)value, 2).PadLeft(4, '0'));
        }

        
        _myInfo = Info.None;
        if (SystemInfo.graphicsMultiThreaded)                           _myInfo |= Info.GraphicsMultiThreaded;
        if (SystemInfo.graphicsUVStartsAtTop)                           _myInfo |= Info.GraphicsUVStartsAtTop;
        if (SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders) _myInfo |= Info.HasDynamicUniformArrayIndexingInFragmentShaders;
        if (SystemInfo.hasHiddenSurfaceRemovalOnGPU)                    _myInfo |= Info.HasHiddenSurfaceRemovalOnGPU;
        if (SystemInfo.hasMipMaxLevel)                                  _myInfo |= Info.HasMipMaxLevel;
        if (SystemInfo.supports2DArrayTextures)                         _myInfo |= Info.Supports2DArrayTextures;
        if (SystemInfo.supports32bitsIndexBuffer)                       _myInfo |= Info.Supports32bitsIndexBuffer;
        if (SystemInfo.supports3DRenderTextures)                        _myInfo |= Info.Supports3DRenderTextures;
        if (SystemInfo.supports3DTextures)                              _myInfo |= Info.Supports3DTextures;
        if (SystemInfo.supportsAccelerometer)                           _myInfo |= Info.SupportsAccelerometer;
        if (SystemInfo.supportsAsyncCompute)                            _myInfo |= Info.SupportsAsyncCompute;
        if (SystemInfo.supportsAsyncGPUReadback)                        _myInfo |= Info.SupportsAsyncGPUReadback;
        if (SystemInfo.supportsAudio)                                   _myInfo |= Info.SupportsAudio;
        if (SystemInfo.supportsCompressed3DTextures)                    _myInfo |= Info.SupportsCompressed3DTextures;
        if (SystemInfo.supportsComputeShaders)                          _myInfo |= Info.SupportsComputeShaders;
        if (SystemInfo.supportsConservativeRaster)                      _myInfo |= Info.SupportsConservativeRaster;
        if (SystemInfo.supportsCubemapArrayTextures)                    _myInfo |= Info.SupportsCubemapArrayTextures;
        if (SystemInfo.supportsGeometryShaders)                         _myInfo |= Info.SupportsGeometryShaders;
        if (SystemInfo.supportsGpuRecorder)                             _myInfo |= Info.SupportsGpuRecorder;
        if (SystemInfo.supportsGraphicsFence)                           _myInfo |= Info.SupportsGraphicsFence;
        if (SystemInfo.supportsGyroscope)                               _myInfo |= Info.SupportsGyroscope;
        if (SystemInfo.supportsHardwareQuadTopology)                    _myInfo |= Info.SupportsHardwareQuadTopology;
        if (SystemInfo.supportsInstancing)                              _myInfo |= Info.SupportsInstancing;
        if (SystemInfo.supportsLocationService)                         _myInfo |= Info.SupportsLocationService;
        if (SystemInfo.supportsMipStreaming)                            _myInfo |= Info.SupportsMipStreaming;
        if (SystemInfo.supportsMotionVectors)                           _myInfo |= Info.SupportsMotionVectors;
        if (SystemInfo.supportsMultisampleAutoResolve)                  _myInfo |= Info.SupportsMultisampleAutoResolve;
        if (SystemInfo.supportsMultisampled2DArrayTextures)             _myInfo |= Info.SupportsMultisampled2DArrayTextures;
        if (SystemInfo.supportsMultiview)                               _myInfo |= Info.SupportsMultiview;
        if (SystemInfo.supportsRawShadowDepthSampling)                  _myInfo |= Info.SupportsRawShadowDepthSampling;
        if (SystemInfo.supportsRayTracing)                              _myInfo |= Info.SupportsRayTracing;
        if (SystemInfo.supportsRenderTargetArrayIndexFromVertexShader)  _myInfo |= Info.SupportsRenderTargetArrayIndexFromVertexShader;
        if (SystemInfo.supportsSeparatedRenderTargetsBlend)             _myInfo |= Info.SupportsSeparatedRenderTargetsBlend;
        if (SystemInfo.supportsSetConstantBuffer)                       _myInfo |= Info.SupportsSetConstantBuffer;
        if (SystemInfo.supportsShadows)                                 _myInfo |= Info.SupportsShadows;
        if (SystemInfo.supportsSparseTextures)                          _myInfo |= Info.SupportsSparseTextures;
        if (SystemInfo.supportsStoreAndResolveAction)                   _myInfo |= Info.SupportsStoreAndResolveAction;
        if (SystemInfo.supportsTessellationShaders)                     _myInfo |= Info.SupportsTessellationShaders;
        if (SystemInfo.supportsVibration)                               _myInfo |= Info.SupportsVibration;
        if (SystemInfo.usesLoadStoreActions)                            _myInfo |= Info.UsesLoadStoreActions;
        if (SystemInfo.usesReversedZBuffer)                             _myInfo |= Info.UsesReversedZBuffer;

        Info compareInfo1 = (Info)_compareInfo_value;
        _myInfo_value = (ulong) _myInfo;
        _info_diff = compareInfo1 ^ _myInfo;

        Info checkedInfo = Info.None;
        foreach (Info value in Enum.GetValues(typeof(Info)))
        {
            if (_info_diff == Info.None)
            {
                Debug.Log("No Diff");
                break;
            }

            if (value == Info.None) continue;
            if ((checkedInfo & value) == value) continue;

            if ((_info_diff & value) == value)
            {
                checkedInfo |= value;

                if((_myInfo & value) == value)
                    Debug.Log("PositiveDiff: " + value.ToString());
                else
                    Debug.Log("NegativeDiff: " + value.ToString());
            }
        }
    }
}
