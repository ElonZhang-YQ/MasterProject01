using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nifti.NET;

public class ReadData
{
    public float[,,] BrainBodyArray;
    // GrayMatter
    public float[,,] GMArray;
    private Nifti.NET.Nifti File;
    // Start is called before the first frame update

    public ReadData()
    {
        read();
    }

    public void read()
    {
        /**
         *  We tried different function to read the data.
         *  SimpleITK has no function to get the pixels in C#.
         *  We tried different functions, includes GetPixelAsVectorFloat32, GetPixelAsFloat and GetBufferAsFloat
         *  These functions could not get the byte data in per pixel
         *  Then we found another function in Binary Reader and Binary Writer
         *  We have gotten the images width, heigh and depth, so we could definate a array by ourselves and full the byte data in the array.
         *  Finally, we found NiftiFile to read the image, it will be easier than binary reader.
         *  Before we write the function, we use the SimpleITK by python to pre-read the picture,
         *  and we found when the graymatter between 10-45 will be good.
         *  Resources:
         *  https://brainder.org/2012/09/23/the-nifti-file-format/
         *  https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0
         *  https://nifti.nimh.nih.gov/nifti-1
         */
        File = NiftiFile.Read("Assets/Images/brain.nii");
        //Debug.Log(File[200,300,300]);
        BrainBodyArray = new float[File.Dimensions[0], File.Dimensions[1], File.Dimensions[2]];
        GMArray = new float[File.Dimensions[0], File.Dimensions[1], File.Dimensions[2]];

        for (int i = 0; i < File.Dimensions[0]; i++) 
        {
            for (int j = 0; j < File.Dimensions[1]; j++)
            {
                for (int k = 0; k < File.Dimensions[2]; k++) 
                {
                    var length = File[i, j, k];
                    if (length > 15 && length < 40)
                    {
                        GMArray[i, j, k] = 1;
                    }
                    else
                    {
                        GMArray[i, j, k] = 0;
                    }
                    BrainBodyArray[i,j,k] = length;
                }
            }
        }
    }
}

