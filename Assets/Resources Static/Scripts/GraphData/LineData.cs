using System;
using UnityEngine;

[Serializable]
public class LineData
{
    [field: SerializeField] public Gradient GradientColor;
    [field: SerializeField] public Material[] Materials;
    [field: SerializeField] public LineTextureMode TextureMode;
}
