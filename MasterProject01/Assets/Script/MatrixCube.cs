using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatrixCube
{
    /*
     * Every cube has 8 point A-B-C-D-E-F-G-H
     * For example:
     * in a single cube, the Point C could be (0,0,0), Point A could be (1,0,0)..., Point F could be (1,1,1)
     *    （0，1，1）（1，1，1）
     *        E ---EF-- F
     *        |         |
     *        | A ---AB--- B （1，1，0）
     *        | |       |  |
     *        G | -GH - H -|
     *          |          |
     *          C ---CD--- D  
     *      （0，0，0） （0，1，0）
     * So the half point between A and B is Point AB
     * the half point between B and C is Point BC
     * ... ...
     * the half point between G and H is Point GH
     */

    // CORNERS
    public static MatrixPoint A = null;
    public static MatrixPoint B = null;
    public static MatrixPoint C = null;
    public static MatrixPoint D = null;
    public static MatrixPoint E = null;
    public static MatrixPoint F = null;
    public static MatrixPoint G = null;
    public static MatrixPoint H = null;
    
}
