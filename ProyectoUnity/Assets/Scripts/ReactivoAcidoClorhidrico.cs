using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ReactivoAcidoClorhidrico : MaterialReactivo
{
	[System.Serializable]
	public struct Reaccion {
		public Reaccion(float iniAc) {
			CantidadInicialAzufre = 2.4f;
			CantidadInicialAcido = iniAc;
			CantidadFinalAcido=0;
			CantidadFinalAzufre=0;
			TiempoOxidacion=0;
		}
		public float CantidadInicialAzufre;
		public float CantidadFinalAzufre;
		public float CantidadInicialAcido;
		public float CantidadFinalAcido;
		public float TiempoOxidacion;
	};



	public Reaccion DatosReaccion;

	List<Reaccion> TablaExperimental;
	ReaccionCantidadLiquido reaccionCantidadAcido;
	ReaccionCambioTamanho reaccionCantidadAzufre;
    ReaccionMovimientoAleatorio reaccionMovimientoAzufre;

    float cantidadInicial;





	void Start () 
	{
		GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoOxidacion");
		gc = obj.GetComponent<ExperimentoOxidacion>();

		NombreReactivo = "Ácido Clorhidrico";

		Usado = false;
		GizmoSeleccion.SetActive (false);
	}




	void Update () {
		
	}


	public void CalcularReaccion(out Reaccion datos, float cantidadAcido)
	{
		Assert.IsTrue (cantidadAcido >= 10f, "La cantidad de acido inicial no debe ser menor a 10 ml.");

		Assert.IsTrue (cantidadAcido <= 150f, "La cantidad de acido inicial no debe ser mayor a 150 ml.");

		datos = new Reaccion (cantidadAcido);

		for (int i = 1; i < TablaExperimental.Count; i++) {
			if (cantidadAcido == TablaExperimental [i].CantidadInicialAcido) {
				datos = TablaExperimental [i];
				break;
			} else if(TablaExperimental[i].CantidadInicialAcido > cantidadAcido && 
				TablaExperimental[i-1].CantidadFinalAcido < cantidadAcido) {
				Reaccion datosMin = TablaExperimental[i-1];
				Reaccion datosMax = TablaExperimental[i];

				float rel = Mathf.Abs(datos.CantidadInicialAcido - datosMin.CantidadInicialAcido) / Mathf.Abs(datosMax.CantidadFinalAcido - datosMin.CantidadInicialAcido);
				datos.TiempoOxidacion = rel * (datosMax.TiempoOxidacion - datosMin.TiempoOxidacion) + datosMin.TiempoOxidacion;
				datos.CantidadFinalAzufre = rel * (datosMax.CantidadFinalAzufre - datosMin.CantidadFinalAzufre) + datosMin.CantidadFinalAzufre;
				datos.CantidadFinalAcido = rel * (datosMax.CantidadFinalAcido - datosMin.CantidadFinalAcido) + datosMin.CantidadFinalAcido;

				DatosReaccion = datos;

				break;
			}
		}
	}



    public float CalcularAcidoRestante(float tiempoTranscurrido)
    {
        Assert.IsTrue(tiempoTranscurrido >= 0, "El tiempo transcurrido en Reactivo no debe ser menor a cero.");

        float p = (tiempoTranscurrido / DatosReaccion.TiempoOxidacion);

        float cantidad = ((DatosReaccion.CantidadFinalAcido - DatosReaccion.CantidadInicialAcido) * p) + DatosReaccion.CantidadInicialAcido;

        return Mathf.Max(cantidad, DatosReaccion.CantidadFinalAcido);
    }


    public float CalcularAzufreRestante(float tiempoTranscurrido)
    {
        Assert.IsTrue(tiempoTranscurrido >= 0, "El tiempo transcurrido en Reactivo no debe ser menor a cero.");

        float p = (tiempoTranscurrido / DatosReaccion.TiempoOxidacion);

        float cantidad = ((DatosReaccion.CantidadFinalAzufre - DatosReaccion.CantidadInicialAzufre) * p) + DatosReaccion.CantidadInicialAzufre;

        return Mathf.Max(cantidad, DatosReaccion.CantidadFinalAzufre);
    }


    public void LlenarTablaExperimental()
	{
		TablaExperimental = new List<Reaccion> ();

		double[] acidoAntes =    {  10,  20,   30,   40,   50,   60,   70,   80,   90, 100, 110, 120, 130, 140, 150};
		double[] tiempos =       {   1, 2.2,  2.9,  3.7,  4.6,  5.1,  5.9,  6.5,  7.9, 8.4, 9.6, 9.7, 9.7, 9.7, 9.7 };
		double[] azufre =        { 2.3, 2.1, 1.85, 1.73, 1.61, 1.54, 1.32,  0.9,    0,   0,   0,   0,   0,   0,   0};
		double[] acidoDespues =  {   0,   0,    0,    0,    0,    0,    0,    0,   10,  20,  30,  40,  50,  60,  70 };


		for (int i = 0; i < tiempos.Length; i++) {
			Reaccion r = new Reaccion();
			r.CantidadInicialAzufre = 2.4f;
			r.CantidadInicialAcido = (float)acidoAntes [i];
			r.TiempoOxidacion = (float)tiempos [i] * 60f;
			r.CantidadFinalAzufre = (float)azufre[i];
			r.CantidadFinalAcido = (float)acidoDespues[i];

			TablaExperimental.Add (r);
		}
	}


	public override void ReaccionQuimica()
	{
        CalcularReaccion();

		Desmarcar ();

		for (int i = 0; i < Reacciones.Count; i++) {
			Reacciones [i].Reaccionar ();
		}

		Usado = true;
		reaccionCantidadAzufre.gameObject.SetActive (true);
	}



    public void CambiarCantidadInicial(float cantidad)
    {
        cantidadInicial = cantidad;

        for (int i = 0; i < Reacciones.Count; i++)
        {
            Reacciones[i].TiempoReaccion = TiempoReaccion;

            if (Reacciones[i].GetType() == typeof(ReaccionCantidadLiquido))
            {
                reaccionCantidadAcido = (ReaccionCantidadLiquido)Reacciones[i];
                reaccionCantidadAcido.CambiarCantidadInicial(cantidad);
                break;
            }
        }
    }




    private void CalcularReaccion()
    {
        Cantidad = cantidadInicial;

        LlenarTablaExperimental();

        Reaccion d;
        CalcularReaccion(out d, Cantidad);
        DatosReaccion = d;
        TiempoReaccion = DatosReaccion.TiempoOxidacion;

        for (int i = 0; i < Reacciones.Count; i++)
        {
            Reacciones[i].TiempoReaccion = TiempoReaccion;

            if (Reacciones[i].GetType() == typeof(ReaccionCantidadLiquido))
            {
                reaccionCantidadAcido = (ReaccionCantidadLiquido)Reacciones[i];
            }

            if (Reacciones[i].GetType() == typeof(ReaccionCambioTamanho))
            {
                reaccionCantidadAzufre = (ReaccionCambioTamanho)Reacciones[i];
            }
        }

        reaccionCantidadAcido.CambiarCantidadInicial(DatosReaccion.CantidadInicialAcido);
        reaccionCantidadAcido.CantidadFinal = DatosReaccion.CantidadFinalAcido;

        reaccionCantidadAzufre.CantidadInicial = DatosReaccion.CantidadInicialAzufre;
        reaccionCantidadAzufre.CantidadFinal = DatosReaccion.CantidadFinalAzufre;

        reaccionCantidadAzufre.gameObject.SetActive(false);
    }
}
