using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColour : MonoBehaviour
{
    private Color originalColour;
    private Vector3 originalVector;

    // Use this for initialization
    void Start()
    {
        var newColour = new Color(Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0));
        this.GetComponent<Renderer>().material.color = newColour;

        originalColour = newColour;
        originalVector = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        // Get the object gazed at, then store original colour
        var cube = this.GetComponent<Renderer>();

        // Change to highlight colour
        cube.material.color = Color.magenta;
    }

    void OnReset()
    {
        // Get the object gazed at, then store original colour
        var cube = this.GetComponent<Renderer>();

        // Change to highlight colour
        cube.material.color = originalColour;
    }

    public void OnResetBlock()
    {
        this.transform.position = originalVector;
    }
}
