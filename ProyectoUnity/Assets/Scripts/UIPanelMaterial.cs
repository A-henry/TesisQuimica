using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanelMaterial : MonoBehaviour 
{
    public Text nombre;
    public Text descripcion;
    public Image imagen;

	void Start () 
    {
	
	}
	
	void Update () {
	
	}


    public void ActualizarMaterial(Material mat)
    {
        nombre.text = mat.name;
        imagen.sprite = mat.image;
        descripcion.text = mat.descripcion;
    }
}
