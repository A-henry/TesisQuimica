using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExperimentos : MonoBehaviour 
{
	public Text TextoAgarrado;
	public Text TextoCantidadAgarrado;
    public Text TextoVolumenInicial;
	public GameObject SinAgarrar;
    public Slider SliderVolumen;

	public Text TextoNombreReactivo;
	public Text TextoCantidadReactivo;
	public Text TextoTiempoReaccion;
    public Text TextoCantidadAcidoRestante;
    public Text TextoAzufreRestante;
	public GameObject SinReactivo;


    ExperimentoOxidacion experimento;



    public float VolumenAcido
    {
        set
        {
            float VolumenCalculado = experimento.CambiarVolumenInicialAcido(value);
            TextoVolumenInicial.text = string.Format("{0:00.00}", VolumenCalculado) + " ml.";
        }
    }


    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoOxidacion");
        experimento = obj.GetComponent<ExperimentoOxidacion>();
    }

    void Start ()
	{
		MostrarAgarrable (false);
		MostrarReactivo (false);

        SliderVolumen.value = 0.5f;

    }
	
	void Update () {
		
	}


	public void MostrarInformacion(MaterialAgarrableZinc m) 
	{
		MostrarAgarrable (true);

		TextoAgarrado.text = m.NombreMaterial;
		TextoCantidadAgarrado.text = m.CantidadFormateada;
	}


	public void MostrarInformacionReactivo(MaterialReactivo reactivo)
	{
		MostrarReactivo (true);

		TextoNombreReactivo.text = reactivo.NombreReactivo;
		TextoCantidadReactivo.text = reactivo.CantidadFormateada;
	}





	public void MostrarAgarrable(bool mostrar)
	{
		TextoAgarrado.transform.parent.gameObject.SetActive (mostrar);
		TextoCantidadAgarrado.transform.parent.gameObject.SetActive (mostrar);
		SinAgarrar.gameObject.SetActive (!mostrar);
	}


	public void MostrarReactivo(bool mostrar)
	{
		TextoNombreReactivo.transform.parent.gameObject.SetActive (mostrar);
		TextoCantidadReactivo.transform.parent.gameObject.SetActive (mostrar);
        SinReactivo.SetActive (!mostrar);

		if (!mostrar) {
			TextoTiempoReaccion.transform.parent.gameObject.SetActive (false);
            TextoAzufreRestante.transform.parent.gameObject.SetActive(false);
            TextoCantidadAcidoRestante.transform.parent.gameObject.SetActive(false);
		}
	}


	public void ActualizarDatosReactivo(MaterialReactivo reactivo, float tiempo)
	{
		TextoTiempoReaccion.transform.parent.gameObject.SetActive (true);
        TextoAzufreRestante.transform.parent.gameObject.SetActive(true);
        TextoCantidadAcidoRestante.transform.parent.gameObject.SetActive(true);
        TextoTiempoReaccion.text = FormatearTiempo (tiempo);

        ReactivoAcidoClorhidrico acido = (ReactivoAcidoClorhidrico)reactivo;

        float acidoRestante = acido.CalcularAcidoRestante(tiempo);
        float azufreRestante = acido.CalcularAzufreRestante(tiempo);

        string acidoFormateado = MaterialReactivo.FormatearCantidad(acidoRestante, MaterialReactivo.UnidadMedida.ml);
        string azufreFormateado = MaterialReactivo.FormatearCantidad(azufreRestante, MaterialReactivo.UnidadMedida.gr);

        TextoCantidadAcidoRestante.text = acidoFormateado;
        TextoAzufreRestante.text = azufreFormateado;
    }



    private string FormatearTiempo(float tiempo)
	{
		int mins = (int)(tiempo/60);
		float segs = (((int)tiempo) % 60) + (((tiempo*1000)%1000)/1000f);

		string res = "";
		if (mins > 0) {
			string m = mins > 1 ? "min." : "min.";
			res = res + mins + " " + m + ", ";
		}

		res = res + string.Format("{0:0.00}",segs) + " seg.";

		return res;
	}
}
