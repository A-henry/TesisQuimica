using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableZinc : MaterialAgarrable
{
    ExperimentoOxidacion gc;

    protected override void Start ()
    {
        base.Start();

        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoOxidacion");
        gc = obj.GetComponent<ExperimentoOxidacion>();
	}


	
	void Update ()
    {
        if ((gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.Agarrado ||  gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.MarcandoReactivo) && 
            gc.Agarrado == this.gameObject && 
            EstaAgarrado == true
            )
        {
            transform.position = RaycasterManipulacion.PosicionAgarre;
        } else
        {
            transform.position = _posicionOriginal;
        }
    }
}
