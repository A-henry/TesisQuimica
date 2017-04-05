using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrableCuadrado : MonoBehaviour
{
    protected Vector3 _posicionOriginal;
    public bool EstaAgarrado;
    public string NombreMaterial;




    public void Start()
    {
        EstaAgarrado = false;
        _posicionOriginal = transform.position;
    }

    void Update()
    {
        transform.position = EstaAgarrado ? RaycasterDensidad.PosicionAgarre : _posicionOriginal;
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
