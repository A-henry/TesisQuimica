using UnityEngine;
using System.Collections;

public class Material : MonoBehaviour 
{
    Renderer rend;
    Color colorOriginal;
    ExperimentoOxidacion ctrl;

    public string name;
    public string descripcion;
    public Sprite image;

    [HideInInspector]
    public bool seleccionado;




    void Start () 
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
    }



	void Update () 
    {
        if (seleccionado)
        {
            rend.material.color = Color.green;
        }else {
            rend.material.color = colorOriginal;
        }
	}
}
