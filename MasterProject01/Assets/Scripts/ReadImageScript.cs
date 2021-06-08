using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using itk.simple;
using System.IO;

public class ReadImageScript : MonoBehaviour{

    private string folderPath = Directory.GetCurrentDirectory();

    private string filePath = "/Assets/Scripts/brain.nii";

    // Start is called before the first frame update
    void Start()
    {
        ReadTheImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadTheImage() {
        
        Image image = SimpleITK.ReadImage(folderPath + filePath);
        IntPtr buffers = image.GetBufferAsFloat();

        int width = getIntLength(image.GetWidth());
        Debug.Log(width);
        int height = getIntLength(image.GetHeight());
        Debug.Log(height);
        int depth = getIntLength(image.GetDepth());;
        Debug.Log(depth);
        
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
        float[,,] imageArray = new float[width, height, depth];
        FileStream fileInput = new FileStream(folderPath +filePath, FileMode.Create);
        BinaryReader reader = new BinaryReader(fileInput);
        try {
            // the nii file head is 348B
            reader.ReadBytes(348);

            for (int w = 0; w < width; w++) {
                for (int h = 0; h < height; h++) {
                    for (int d = 0; d < depth; d++) {
                        imageArray[w, h, d] = reader.ReadSingle();
                    }
                }
            }
        } catch (EndOfStreamException e) {
            // reader reachs to the end of the file
        }

    }

    private int getIntLength(uint uLength) {

        return Convert.ToInt32(uLength);
    }

}