using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixPoint
{
    
    private Vector3 _position = Vector3.zero;
    private bool _on = false; 

    public Vector3 Position 
    {
        get
        {
            return _position;
        }
        set
        {
            _position = new Vector3(value.x, value.y, value.z);
        }
    }
    public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            _on = value;
        }
    }
}
