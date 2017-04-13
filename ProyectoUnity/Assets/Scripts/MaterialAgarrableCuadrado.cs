using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableCuadrado : MaterialAgarrable
{


    void Update()
    {
        transform.position = EstaAgarrado ? RaycasterDensidad.PosicionAgarre : _posicionOriginal;


    }

    



}
