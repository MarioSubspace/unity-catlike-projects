using UnityEngine;
using System.Collections.Generic;

public class TransformationGrid : MonoBehaviour
{
    public Transform pointPrefab;
    public int cubeSize = 10;

    Transform[,,] pointGrid;
    List<Transformation> transformations = new List<Transformation>();
    Matrix4x4 transformMatrix = Matrix4x4.identity;

    private void Start()
    {
        ConstructPointGrid();
    }

    private void Update()
    {
        UpdateTransformMatrix();
        for (int z = 0; z < cubeSize; z++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int x = 0; x < cubeSize; x++)
                {
                    pointGrid[x, y, z].localPosition = TranformPoint(x, y, z);
                }
            }
        }
    }

    private void UpdateTransformMatrix()
    {
        transformMatrix = Matrix4x4.identity;
        GetComponents<Transformation>(transformations);
        if (transformations != null)
        {
            foreach (Transformation t in transformations)
            {
                transformMatrix = t.Matrix * transformMatrix;
            }
        }
    }

    private Vector3 TranformPoint(int x, int y, int z)
    {
        Vector3 point = CubeToCenterTransform(x, y, z);
        return transformMatrix.MultiplyPoint(point);
    }

    private void ConstructPointGrid()
    {
        if (pointGrid != null)
        {
            Debug.Log("Point grid already exists!");
            return;
        }

        pointGrid = new Transform[cubeSize, cubeSize, cubeSize];
        for (int z = 0; z < cubeSize; z++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int x = 0; x < cubeSize; x++)
                {
                    Transform t = ConstructPoint(x, y, z);
                    t.SetParent(transform, true);
                    pointGrid[x, y, z] = t;
                }
            }
        }
    }

    private Transform ConstructPoint(int x, int y, int z)
    {
        Transform t = Instantiate<Transform>(pointPrefab);
        t.position = CubeToCenterTransform(x, y, z);
        t.GetComponent<MeshRenderer>().material.color = new Color(
            (x + 1f) / cubeSize,
            (y + 1f) / cubeSize,
            (z + 1f) / cubeSize
        );
        return t;
    }

    private Vector3 CubeToCenterTransform(int x, int y, int z)
    {
        // D is the displacement from the world origin to the full cube origin
        // for each axis. Subtract the local point coordinates from this to
        // center the full cube at the world origin.
        float D = (cubeSize - 1) * 0.5f;
        return new Vector3(x - D, y - D, z - D);
    }
}
