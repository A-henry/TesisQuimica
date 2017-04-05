using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoAgua : MaterialReactivo
{
    public float CantidadSodio;

    protected override void Start ()
    {
        base.Start();	
	}



    public override void ReaccionQuimica()
    {
        Desmarcar();

        for (int i = 0; i < Reacciones.Count; i++)
        {
            ReaccionMovimientoSuperficial sup = Reacciones[i] as ReaccionMovimientoSuperficial;
            if (sup != null)
            {
                sup.CantidadInicial = CantidadSodio;
                
            }


            Reacciones[i].TiempoReaccion = TiempoReaccion;
            Reacciones[i].Reaccionar();
        }

        Usado = true;
    }


}
