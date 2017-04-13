using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISodio : MonoBehaviour
{
    ExperimentoSodio _experimento;


    static UISodio _instancia;
    public static UISodio Instancia
    {
        get { return _instancia; }
    }

    public Slider SliderCantidadSodio;
    public Text TextoCandidadSodio;
    public Text TextoTiempo;

    public float CantidadSodio
    {
        set
        {
            float cantidadReal = _experimento.CambiarCantidadSodioRel(value);
            TextoCandidadSodio.text = string.Format("{0:00.00} gr." , cantidadReal);
        }
    }

    void Awake()
    {
        _instancia = this;
    }

	void Start ()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoSodio");
        _experimento = obj.GetComponent<ExperimentoSodio>();
        CantidadSodio = SliderCantidadSodio.value;

        TextoTiempo.gameObject.SetActive(false);
	}



    public void ReaccionQuimica()
    {
        SliderCantidadSodio.gameObject.SetActive(false);
        TextoTiempo.gameObject.SetActive(true);
    }



    public void ActualizarCantidadSodio(float cantidad)
    {
        TextoCandidadSodio.text = string.Format("{0:00.00} gr.", cantidad);
    }


    public void ActualizarTiempo(float tiempo)
    {
        TextoTiempo.text = FormatearTiempo(tiempo);
    }


    private string FormatearTiempo(float tiempo)
    {
        int mins = (int)(tiempo / 60);
        float segs = (((int)tiempo) % 60) + (((tiempo * 1000) % 1000) / 1000f);

        string res = "";
        if (mins > 0)
        {
            string m = mins > 1 ? "mins" : "min";
            res = res + mins + " " + m + ", ";
        }

        res = res + string.Format("{0:0.00}", segs) + " seg.";

        return res;
    }




}
