using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;
using itk.simple;

namespace MarchingCubesProject
{
    public class Main : MonoBehaviour
    {
        
        private string filePath = "Assets/Images/brain.nii";

        public Material m_material;
        //public Material transparent_material;

        List<GameObject> meshes = new List<GameObject>();

        public GameObject currentGameObject;

        public float alpha = 10f;

        void Start()
        {

            Image image = SimpleITK.ReadImage(filePath);
            IntPtr buffers = image.GetBufferAsFloat();

            int width = getIntLength(image.GetWidth());
            int height = getIntLength(image.GetHeight());
            int depth = getIntLength(image.GetDepth());


            /**
             *  SimpleITK has no function to get the pixels in C#.
             *  We tried different functions, includes GetPixelAsVectorFloat32, GetPixelAsFloat and GetBufferAsFloat
             *  These functions could not get the byte data in per pixel
             *  Then we found another function in Binary Reader and Binary Writer
             *  We have gotten the images width, heigh and depth, so we could definate a array by ourselves and full the byte data in the array.
             *  Resources:
             *  https://brainder.org/2012/09/23/the-nifti-file-format/
             *  https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0
             *  https://nifti.nimh.nih.gov/nifti-1
             */
            float[] voxels = new float[width * height * depth];
            float[,,] imageArray = new float[width, height, depth];

            BinaryReader br = new BinaryReader(new FileStream(filePath, FileMode.Open));

            // the head of nii file is 348B, ignore head info
            br.ReadBytes(348);

            for (int w = 0; w < width; ++w)
            {
                for (int h = 0; h < height; ++h)
                {
                    for (int d = 0; d < depth; ++d)
                    {
                        imageArray[w, h, d] = br.ReadSingle();
                    }
                }
            }
            br.Close();
            // TODO describe why should be convert
            convert2Voxel(voxels, imageArray, width, height, depth);
            
            // for (int w = 0; w < width; ++w)
            // {
            //     for (int h = 0; h < height; ++h)
            //     {
            //         for (int d = 0; d < depth; ++d)
            //         {
            //             if (voxels[w*384*384+ h*384+ d] > 50 || voxels[w*384*384+ h*384+ d] < 30)
            //             {
            //                 voxels[w*384*384+ h*384+ d] = 0;
            //             }


            //         }
            //     }
            // }
            currentGameObject = gameObject;

            //Set the mode used to create the mesh.
            //Cubes is faster and creates less verts, tetrahedrons is slower and creates more verts but better represents the mesh surface.
            Marching marching = new MarchingCubes();

            //Surface is the value that represents the surface of mesh
            //For example the perlin noise has a range of -1 to 1 so the mid point is where we want the surface to cut through.
            //The target value does not have to be the mid point it can be any value with in the range.
            marching.Surface = 0.5f;

            List<Vector3> verts = new List<Vector3>();
            List<int> indices = new List<int>();

            //The mesh produced is not optimal. There is one vert for each index.
            //Would need to weld vertices for better quality mesh.
            marching.Generate(voxels, width, height, depth, verts, indices);

            //A mesh in unity can only be made up of 65000 verts.
            //Need to split the verts between multiple meshes.

            int maxVertsPerMesh = 65001; //must be divisible by 3, ie 3 verts == 1 triangle
            int numMeshes = verts.Count / maxVertsPerMesh + 1;
            Debug.Log(verts.Count);

            for (int i = 0; i < numMeshes; i++)
            {

                List<Vector3> splitVerts = new List<Vector3>();
                List<int> splitIndices = new List<int>();

                for (int j = 0; j < maxVertsPerMesh; j++)
                {
                    int idx = i * maxVertsPerMesh + j;

                    if (idx < verts.Count)
                    {
                        splitVerts.Add(verts[idx]);
                        splitIndices.Add(j);
                    }
                }

                if (splitVerts.Count == 0) continue;

                Mesh mesh = new Mesh();
                mesh.SetVertices(splitVerts);
                mesh.SetTriangles(splitIndices, 0);
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                GameObject go = new GameObject("Mesh");
                go.transform.parent = transform;
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>();
                Color oldColor = m_material.color;
                Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
                Material transparent_material;
                transparent_material = m_material;
                transparent_material.SetColor("_Color", newColor);

                go.GetComponent<Renderer>().material = transparent_material;
                go.GetComponent<MeshFilter>().mesh = mesh;
                go.transform.localPosition = new Vector3(-width / 2, -height / 2, -depth / 2);

                meshes.Add(go);
            }

        }

        void Update()
        {
            transform.Rotate(Vector3.up, 10.0f * Time.deltaTime);
            
        }

        private int getIntLength(uint uLength) {

            return Convert.ToInt32(uLength);
        }

        private void convert2Voxel(float[] voxels, float[,,] imageArray, int width, int height, int depth)
        {
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int d = 0; d < depth; d++)
                    {
                        voxels[w * height * depth + h * depth + d] = imageArray[w, h, d];
                    }
                }
            }
            
        }
    }
}
