using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableSodio : MaterialAgarrable
{
    void Update()
    {
        transform.position = EstaAgarrado ? RaycasterSodio.PosicionAgarre : _posicionOriginal;


        transform.localScale = Vector3.one * ((Cantidad / 20f) * 0.2f);
    }
}
