using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrable : MonoBehaviour
{
    public enum UnidadMedida { gr, ml, kg, cc };
    public bool EstaAgarrado;
    public string NombreMaterial;
    [Range(1, 100)]
    public float Cantidad;
    public UnidadMedida Unidad;

    public string CantidadFormateada
    {
        get
        {
            Dictionary<UnidadMedida, string> formatoUnidades = new Dictionary<UnidadMedida, string>();
            formatoUnidades.Add(UnidadMedida.gr, "Gr.");
            formatoUnidades.Add(UnidadMedida.cc, "CC.");
            formatoUnidades.Add(UnidadMedida.kg, "Kg.");
            formatoUnidades.Add(UnidadMedida.ml, "Ml.");

            return string.Format("{0:0.00}", Cantidad) + " " + formatoUnidades[Unidad];
        }
    }

    protected Collider _coll;
    protected Vector3 _posicionOriginal;


    protected virtual void Start ()
    {
        EstaAgarrado = false;
        _posicionOriginal = transform.position;
        _coll = GetComponent<Collider>();
    }



    public void Agarrar()
    {
        EstaAgarrado = true;
        _coll.enabled = false;
    }



    public void Soltar()
    {
        EstaAgarrado = false;
        _coll.enabled = true;
    }
}
