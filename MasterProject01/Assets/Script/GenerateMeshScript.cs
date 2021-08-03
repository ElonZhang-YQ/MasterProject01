using System;
using UnityEngine;

public class GenerateMeshScript : MonoBehaviour
{
    private float speed = 100.0f;
    public Vector3 GridSize;
    public Material material = null;
    private Mesh mesh = null;
    private ReadData reader;
    public float[,,] BrainBodyArray;
    // GrayMatter
    public float[,,] GMArray;

    private void Start()
    {
        readData();
        GridSize = new Vector3(BrainBodyArray.GetLength(0), BrainBodyArray.GetLength(1), BrainBodyArray.GetLength(2));
        BuildTheMeshGrid();
        IsGrayMatterMatrix();
        March();
    }

    private void readData()
    {
        reader = new ReadData();
        BrainBodyArray = reader.BrainBodyArray;
        GMArray = reader.GMArray;
    }
    private void BuildTheMeshGrid()
    {
        MarchingCube.singlePoint = new MatrixPoint[(int)GridSize.x, (int)GridSize.y, (int)GridSize.z];

        // define the points
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.singlePoint[x, y, z] = new MatrixPoint();
                    MarchingCube.singlePoint[x, y, z].Position = new Vector3(x, y, z);
                    MarchingCube.singlePoint[x, y, z].On = false;
                }
            }
        }
    }
    private void IsGrayMatterMatrix()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    if (GMArray[x ,y ,z] == 1) {
                        MarchingCube.singlePoint[x, y, z].On = true;
                    }
                    
                }
            }
        }
    }
    private void March()
    {
        GameObject go = this.gameObject;
        mesh = MarchingCube.GenerateMesh(ref go, ref material);
        MarchingCube.GenerateCubes();
        MarchingCube.Mesh(ref mesh);
    }
    
    private void Update()
    {
        
    }
}