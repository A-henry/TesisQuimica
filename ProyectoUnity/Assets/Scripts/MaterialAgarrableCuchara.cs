using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableCuchara : MaterialAgarrable
{
    public float Capacidad = 3f;
    public GameObject CloratoEnCuchara;

    protected override void Start()
    {
        base.Start();

        Cantidad = 0;
        CloratoEnCuchara.SetActive(false);
    }



    void Update()
    {
        transform.position = EstaAgarrado ? RaycasterClorato.PosicionAgarre : _posicionOriginal;
    }


    public void Llenar()
    {
        Cantidad = Capacidad;
        CloratoEnCuchara.SetActive(true);
    }


    public void Vaciar()
    {
        Cantidad = 0;
        CloratoEnCuchara.SetActive(false);
    }
}
