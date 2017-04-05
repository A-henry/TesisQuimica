using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClorato : MonoBehaviour
{
    // Singleton de UI
    private static UIClorato _instancia;
    public static UIClorato Instancia
    {
        get
        {
            return _instancia;
        }
    }

    public Text TextoCantidadClorato;
    public Text TextoInstruccion;
    public Text TextoTiempo;
    public Text TextoTiempoReaccion;


    void Awake()
    {
        _instancia = this;
    }



    public void CambiarCantidadClorato(float cantidad)
    {
        TextoCantidadClorato.text = string.Format("{0:00.00} gr.", cantidad);
    }


    public void InstruccionMechero()
    {
        TextoInstruccion.text = "Encienda Mechero";
    }


    public void InstruccionPapel()
    {
        TextoInstruccion.text = "Heche papel al tubo";
    }


    public void ActualizarTiempoDisolucion(float tiempo)
    {
        TextoTiempo.gameObject.SetActive(true);
        TextoTiempo.text = FormatearTiempo(tiempo);
    }


    public void ActualizarTiempoReaccion(float tiempo)
    {
        TextoTiempoReaccion.gameObject.SetActive(true);
        TextoTiempoReaccion.text = FormatearTiempo(tiempo);
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
