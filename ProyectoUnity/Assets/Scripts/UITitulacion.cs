using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitulacion : MonoBehaviour
{
    ExperimentoTitulacion experimento;


    public GameObject SliderVolumen;


    public float VolumenAcido
    {
        set
        {
            experimento.CambiarVolumenInicialAcido(value);
        }

    }

    void Start ()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        experimento = obj.GetComponent<ExperimentoTitulacion>();


        SliderVolumen.SetActive(true);
    }
	
	void Update () {
	}



    public void EmpezarExperimento()
    {
        experimento.EmpezarExperimento();

        SliderVolumen.SetActive(false);
    }


    
}
