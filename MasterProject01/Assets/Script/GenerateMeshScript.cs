using UnityEngine;

public class GenerateMeshScript : MonoBehaviour
{
    public Vector3 GridSize;
    public Material material = null;
    private Mesh mesh = null;
    private ReadData readData;

    private void Start()
    {
        readData = new ReadData();
        GridSize = new Vector3(readData.Brain.GetLength(0), readData.Brain.GetLength(1), readData.Brain.GetLength(2));
        MakeGrid();
        Noise3d();
        March();
    }
    private void MakeGrid()
    {
        //allocate
        MarchingCube.grd = new GridPoint[(int)GridSize.x, (int)GridSize.y, (int)GridSize.z];

        // define the points
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z] = new GridPoint();
                    MarchingCube.grd[x, y, z].Position = new Vector3(x, y, z);
                    MarchingCube.grd[x, y, z].On = false;
                }
            }
        }
    }
    private void Noise3d()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    if (readData.Graymatter[x ,y ,z] == 1) {
                        MarchingCube.grd[x, y, z].On = true;
                    }
                    
                }
            }
        }
    }
    private void March()
    {
        GameObject go = this.gameObject;
        mesh = MarchingCube.GetMesh(ref go, ref material);
        MarchingCube.Clear();
        MarchingCube.MarchCubes();
        MarchingCube.SetMesh(ref mesh);
    }
}