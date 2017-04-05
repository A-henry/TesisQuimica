using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISodio : MonoBehaviour
{
    ExperimentoSodio _experimento;





    public Slider SliderCantidadSodio;
    public Text TextoCandidadSodio;

    public float CantidadSodio
    {
        set
        {
            float cantidadReal = _experimento.CambiarCantidadSodioRel(value);
            TextoCandidadSodio.text = string.Format("{0:00.00} gr." , cantidadReal);
        }
    }

	void Start ()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoSodio");
        _experimento = obj.GetComponent<ExperimentoSodio>();
        CantidadSodio = SliderCantidadSodio.value;
	}
	
	void Update () {
		
	}
}
