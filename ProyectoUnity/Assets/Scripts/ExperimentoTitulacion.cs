using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ExperimentoTitulacion : MonoBehaviour
{
    [Range(50, 100)]
    public float VolumenMaximoAcido = 5f;
    [Range(0, 50)]
    public float VolumenMinimoAcido = 80f;

    public VasoPrecipitado Vaso;
    public GeneradorGotas Gotero;

    [HideInInspector]
    public bool Empezado;


    public UITitulacion UI;


    void Awake()
    {
    }



	void Start ()
    {
        Empezado = false;
    }
	


	void Update () {
		
	}



    public float CambiarVolumenInicialAcido(float volumenRelativo)
    {
        float vol = Mathf.Lerp(VolumenMinimoAcido, VolumenMaximoAcido, volumenRelativo);

        Vaso.CantidadLiquido = vol;

        return vol;
    }


    public void CambiarVolumenAcido(float volumen)
    {
        UI.ActualizarVolumenActual(volumen);
    }

    public void CambiarTitulacion(float volumenT)
    {
        UI.ActualizarVolumenTitulacion(volumenT);
    }

    public void CambiarPh(float ph)
    {
        UI.ActualizarPh(ph);
    }


    public void CambiarFrecuenciaGotero(float freqP)
    {
        float maxFreq = 10;
        float minFreq = 1;

        Gotero.Frecuencia = Mathf.Lerp(minFreq, maxFreq, freqP);
    }


    public void EmpezarExperimento()
    {
        Empezado = true;
       
}
}
