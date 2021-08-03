using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilderScript : MonoBehaviour
{

    public Material hardMaterial;
    public Material perspectiveMaterial;
    public GameObject Brain;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnNumSliderChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnNumSliderChange(float currentValue)
    {
        
        if (currentValue <= 0.5f)
        {
            Brain.GetComponent<Renderer>().sharedMaterial = perspectiveMaterial;
            Color materialColor = perspectiveMaterial.color;
            materialColor.a = currentValue;
            perspectiveMaterial.color = materialColor;
        }
        else
        {
            Brain.GetComponent<Renderer>().sharedMaterial = hardMaterial;
        }
    }
}
