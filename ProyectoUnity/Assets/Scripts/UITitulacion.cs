using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITitulacion : MonoBehaviour
{
    ExperimentoTitulacion experimento;
    VasoPrecipitado vaso;

    public Slider SliderVolumen;
    public Slider SliderGotero;
    public GameObject PanelVolumenInicial;
    public GameObject PanelExperimento;
    public Text VolumenActualLbl;
    public Text VolumenInicialLbl;
    public Text VolumenTitulacion;
    public Text Phfinal;


    public float VolumenAcido
    {
        set
        {
            float VolumenCalculado = experimento.CambiarVolumenInicialAcido(value);
            VolumenInicialLbl.text = string.Format("{0:00.00}", VolumenCalculado) + " ml.";
           
        }
    }


    public float FrecuenciaGotero
    {
        set
        {
            experimento.CambiarFrecuenciaGotero(value);
        }
    }

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        experimento = obj.GetComponent<ExperimentoTitulacion>();

        PanelVolumenInicial.SetActive(true);
        PanelExperimento.SetActive(false);

        SliderVolumen.value = 0.5f;
        SliderGotero.value = 0.1f;
    }



    void Update() {
    }



    public void EmpezarExperimento()
    {
        experimento.EmpezarExperimento();

        PanelVolumenInicial.SetActive(false);
        PanelExperimento.SetActive(true);
    } 


    public void ActualizarVolumenActual(float volumen)
    {
        VolumenActualLbl.text = string.Format("{0:00.00}", volumen) + " ml."; ;
    }

    public void ActualizarVolumenTitulacion(float vol)
    {
        VolumenTitulacion.text = string.Format("{0:00.00}", vol) + " ml."; ;
    }

    public void ActualizarPh(float ph)
    {
        Phfinal.text = string.Format("{0:00.00}", ph) + " "; ;
    }
    

}
