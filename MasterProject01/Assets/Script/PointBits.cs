using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointBits
{
    /*
     * depend on the edgecube table, we could get:
     * cubeindex = 0;
     * if (V[0] < isolevel) cubeindex |= 1;
     * if (V[1] < isolevel) cubeindex |= 2;
     * if (V[2] < isolevel) cubeindex |= 4;
     * if (V[3] < isolevel) cubeindex |= 8;
     * if (V[4] < isolevel) cubeindex |= 16;
     * if (V[5] < isolevel) cubeindex |= 32;
     * if (V[6] < isolevel) cubeindex |= 64;
     * if (V[7] < isolevel) cubeindex |= 128;
     * so:
     * follow this cube, we make some changes. in out cube, Point C could be (0, 0, 0)
     * Point 0 = Point G
     * Point 1 = Point H
     * Point 2 = Point D
     * Point 3 = Point C
     * Point 4 = Point E
     * Point 5 = Point F
     * Point 6 = Point B
     * Point 7 = Point A
     */
    public static int A = (int)Mathf.Pow(2, 7);
    public static int B = (int)Mathf.Pow(2, 6);
    public static int C = (int)Mathf.Pow(2, 3);
    public static int D = (int)Mathf.Pow(2, 2);
    public static int E = (int)Mathf.Pow(2, 4);
    public static int F = (int)Mathf.Pow(2, 5);
    public static int G = (int)Mathf.Pow(2, 0);
    public static int H = (int)Mathf.Pow(2, 1);
}
