using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoTitulacion : MonoBehaviour
{
    [Range(50, 100)]
    public float VolumenMaximoAcido = 5f;
    [Range(0, 50)]
    public float VolumenMinimoAcido = 80f;

    public VasoPrecipitado Vaso;

    [HideInInspector]
    public bool Empezado;

	void Start () {
        Empezado = false;
	}
	
	void Update () {
		
	}



    public void CambiarVolumenInicialAcido(float volumenRelativo)
    {
        float vol = Mathf.Lerp(VolumenMinimoAcido, VolumenMaximoAcido, volumenRelativo);

        Vaso.CantidadLiquido = vol;
    }


    public void EmpezarExperimento()
    {
        Empezado = true;
        Debug.Log("empezar");
    }
}
