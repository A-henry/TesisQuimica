using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrable : MonoBehaviour
{
	public enum UnidadMedida {gr,ml,kg,cc};

    public bool EstaAgarrado;

	public string NombreMaterial;
	[Range(1, 100)]
	public float Cantidad;
	public UnidadMedida Unidad;


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

    

    Vector3 PosicionOriginal;


    GameController gc;


    void Start () {

        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        gc = obj.GetComponent<GameController>();


        EstaAgarrado = false;
        PosicionOriginal = transform.position;
	}
	
	void Update ()
    {
        if ((gc.EstadoSeleccion == GameController.EnumSeleccion.Agarrado ||  gc.EstadoSeleccion == GameController.EnumSeleccion.MarcandoReactivo) && 
            gc.Agarrado == this.gameObject && 
            EstaAgarrado == true
            )
        {
            transform.position = RaycasterManipulacion.PosicionAgarre;
        } else
        {
            transform.position = PosicionOriginal;
        }
    }


    public void Agarrar()
    {
        EstaAgarrado = true;
    }

    public void Soltar()
    {
        EstaAgarrado = false;
    }
}
