using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gota : MonoBehaviour
{
    [HideInInspector]
    public float Cantidad = 0.1f;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "SuperficieLiquido")
        {
            VasoPrecipitado vaso = c.GetComponentInParent<VasoPrecipitado>();
            vaso.IngresarLiquido(Cantidad);

            Destroy(gameObject);
        }
    }
}
