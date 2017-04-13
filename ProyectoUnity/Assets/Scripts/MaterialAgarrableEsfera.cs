using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableEsfera : MaterialAgarrable {

    ExperimentoDensidad gc;

    public Transform TransformConexion;

    protected override void Start()
    {
        base.Start();

        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDensidad");
        gc = obj.GetComponent<ExperimentoDensidad>();
    }

    void Update()
    {
        //transform.position = EstaAgarrado ? RaycasterDensidad.PosicionAgarre : _posicionOriginal;
        transform.localScale = Vector3.one * ((Cantidad / 300f) * 0.2f);
        if ((gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Agarrado || gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.MarcandoReactivo) &&
            gc.Agarrado == this.gameObject && EstaAgarrado == true)
        {
            transform.position = RaycasterDensidad.PosicionAgarre;
        }
        else
        {
            if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Reaccionando)
            {
                transform.position = TransformConexion.position;
            }
            else
                transform.position = _posicionOriginal;
        }
    }


}
