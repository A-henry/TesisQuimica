using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrablePapel : MaterialAgarrable
{
    protected override void Start()
    {
        base.Start();
    }



    void Update()
    {
        transform.position = EstaAgarrado ? RaycasterClorato.PosicionAgarre : _posicionOriginal;
    }
}
