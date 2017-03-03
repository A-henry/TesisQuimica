using UnityEngine;
using System.Collections;


public class CambiarColor : MonoBehaviour {
    public Color startColor;
    public Color mouseOverColor;
    bool mouseOver = false;
	// Use this for initialization
	
    void OnMouseEnter()
    {
        mouseOver = true;
        //GetComponent<Renderer>().material.SetColor("_Color", mouseOverColor);
        //GetComponent<Renderer>().transform.Rotate(new Vector3(0f, 300f, 0f) * Time.deltaTime);
        //transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime);
    }
    void Update()
    {
        if (mouseOver == true)
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime);
        }


        if (mouseOver == false)
        {
            transform.Rotate(new Vector3(0f, 0f, 0f));
        }
    }


    void OnMouseExit()
    {
        mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_Color", startColor);
    }
}
