using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableCable : MaterialAgarrable
{
    public Transform CentroCable;
    public float RadioMaximoCable = 5f;
    public string Tipo = "A";

    public bool EstaConectado;
    public Transform TransformConexion;

    void Awake() {
        EstaConectado = false;
    }

    void Update()
    {
        if (EstaAgarrado)
        {
            Vector3 dif = RaycasterConductividad.PosicionAgarre - CentroCable.position;
            Vector3 unitario = dif.normalized;

            Vector3 res=dif;

            if (dif.magnitude > RadioMaximoCable)
                dif = unitario * RadioMaximoCable;

            transform.position = CentroCable.position + dif;
        }

        if(EstaConectado)
        {
            transform.position = TransformConexion.position;
        }

    }

    public override void Agarrar()
    {
        EstaAgarrado = true;
        EstaConectado = false;
        _coll.enabled = false;
    }




    public override void Soltar()
    {
        base.Soltar();

        transform.position = _posicionOriginal;
    }

}
