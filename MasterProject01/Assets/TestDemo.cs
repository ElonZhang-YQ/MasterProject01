using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int t = 0;
        for (int i = 0; i < 8; i ++) {
            t++;
            Debug.Log("i :" + VertexOffset[i, 0] + ", " + t);
            Debug.Log("j :" + VertexOffset[i, 1] + ", " + t);
            Debug.Log("k :" + VertexOffset[i, 2] + ", " + t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected static readonly int[,] VertexOffset = new int[,]
	    {
	        {0, 0, 0},{1, 0, 0},{1, 1, 0},{0, 1, 0},
	        {0, 0, 1},{1, 0, 1},{1, 1, 1},{0, 1, 1}
	    };
}
