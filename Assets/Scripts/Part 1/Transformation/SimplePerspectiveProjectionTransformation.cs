using UnityEngine;

public class SimplePerspectiveProjectionTransformation : Transformation
{
    public float focalLength = 1;

    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix[0, 0] = focalLength;
            matrix[1, 1] = focalLength;
            matrix[3, 2] = 1;
            return matrix;
        }
    }
}
