using System;
using UnityEngine;

public class OrthographicProjectionTransformation : Transformation
{
    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix[2, 2] = 0f;
            return matrix;
        }
    }
}
