using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReactivo : MonoBehaviour
{
	public enum UnidadMedida {gr,ml,kg,cc};
	public UnidadMedida Unidad;

	[Range(10f, 150f)]
	public float Cantidad = 50f;

	public string CantidadFormateada 
	{
		get { 
			Dictionary<UnidadMedida, string> formatoUnidades = new Dictionary<UnidadMedida, string> ();
			formatoUnidades.Add (UnidadMedida.gr, "Gr.");
			formatoUnidades.Add (UnidadMedida.cc, "CC.");
			formatoUnidades.Add (UnidadMedida.kg, "Kg.");
			formatoUnidades.Add (UnidadMedida.ml, "Ml.");

			return string.Format ("{0:0.00}", Cantidad) + " " + formatoUnidades [Unidad];
		}	
	}
	[HideInInspector]
	public string NombreReactivo;
	[HideInInspector]
	public float TiempoReaccion;

    

	public GameObject GizmoSeleccion;
	public List<ReaccionQuimica> Reacciones;

	public bool Usado;



    protected virtual void Start()
    {
        Usado = false;
		GizmoSeleccion.SetActive (false);
    }

	

    public void Marcar()
    {
		GizmoSeleccion.SetActive (true);
    }

    public virtual void Desmarcar()
    {
		GizmoSeleccion.SetActive (false);
    }

    public virtual void ReaccionQuimica()
    {
		Desmarcar ();

		for (int i = 0; i < Reacciones.Count; i++) {
			Reacciones [i].Reaccionar ();
		}

		Usado = true;
    }


    public static string FormatearCantidad(float cantidad, UnidadMedida unidad)
    {
        Dictionary<UnidadMedida, string> formatoUnidades = new Dictionary<UnidadMedida, string>();
        formatoUnidades.Add(UnidadMedida.gr, "Gr.");
        formatoUnidades.Add(UnidadMedida.cc, "CC.");
        formatoUnidades.Add(UnidadMedida.kg, "Kg.");
        formatoUnidades.Add(UnidadMedida.ml, "Ml.");

        return string.Format("{0:0.00}", cantidad) + " " + formatoUnidades[unidad];
    }
}
