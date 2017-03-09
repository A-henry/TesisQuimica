using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorVasoPrecipitado : MonoBehaviour
{
    public GameObject MeshNormal;
    public GameObject MeshSeleccionado;
    public GameObject Liquido;
    public GameObject GizmoSeleccion;

	void Start () {
        MeshNormal.SetActive(true);
        Liquido.SetActive(true);
        MeshSeleccionado.SetActive(false);
        GizmoSeleccion.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void MouseSobre()
    {
        MeshNormal.SetActive(false);
        Liquido.SetActive(false);
        MeshSeleccionado.SetActive(true);
        GizmoSeleccion.SetActive(false);
    }



    public void MouseSaliendo()
    {
        MeshNormal.SetActive(true);
        Liquido.SetActive(true);
        MeshSeleccionado.SetActive(false);
        GizmoSeleccion.SetActive(false);
    }


    public void Seleccionar()
    {
        MeshNormal.SetActive(true);
        Liquido.SetActive(true);
        MeshSeleccionado.SetActive(false);
        GizmoSeleccion.SetActive(true);
    }


    public void Deseleccionar()
    {
        MeshNormal.SetActive(true);
        Liquido.SetActive(true);
        MeshSeleccionado.SetActive(false);
        GizmoSeleccion.SetActive(false);
    }
}
