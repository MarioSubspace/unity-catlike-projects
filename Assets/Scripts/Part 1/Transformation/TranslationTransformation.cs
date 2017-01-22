using UnityEngine;

public class TranslationTransformation : Transformation
{
    public Vector3 translation = new Vector3(0, 0, 0);

    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            Vector4 c = translation;
            c.w = 1;
            matrix.SetColumn(3, c);
            return matrix;
        }
    }
}
