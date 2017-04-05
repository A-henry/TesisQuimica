using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoDensidad : MonoBehaviour {


    public GameObject Agarrado;

    void Start () {
		
	}
	
	void Update () {
		
	}


    public void Agarrar(GameObject go)
    {
        Agarrado = go;
        go.GetComponent<MaterialAgarrableCuadrado>().Agarrar();
    }


    public void Soltar()
    {
        Agarrado.GetComponent<MaterialAgarrableCuadrado>().Soltar();
        Agarrado = null;
    }


    public bool SobreReactivo(MaterialReactivo reactivo)
    {
            return false;
    }
}
