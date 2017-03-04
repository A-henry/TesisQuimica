using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoPrecipitado : MonoBehaviour
{
    public Transform Liquido;
    public Renderer RendererLiquido;
    public float CapacidadMaxima = 100; // ml

    [Range(0, 100f)]
    public float CantidadLiquido;
    public Color ColorTitulacion = Color.magenta;
    public float TiempoTitulacion = 2f;
    public float VolumenTitulacion = 50; // ml

    bool estaTitulado;
    bool cambiandoColor;
    Color colorOriginal;
    float tiempoRestante;

    void Start()
    {
        cambiandoColor = false;
        estaTitulado = false;
        colorOriginal = RendererLiquido.material.color;

        StartCoroutine(CorutinaGoteo());
    }

    void Update()
    {
        // Cantidad de Liquido
        float esc = CantidadLiquido / CapacidadMaxima;
        Vector3 s = Liquido.localScale;
        Liquido.localScale = new Vector3(s.x, esc, s.z);

        if (CantidadLiquido >= VolumenTitulacion) {
            if (estaTitulado == false) {
                estaTitulado = true;
                TitularLiquido();
            }

        }

        // Color de Liquido
        if (cambiandoColor)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;

            float p = 1f - (tiempoRestante / TiempoTitulacion);

            Color colorActual = Color.Lerp(colorOriginal, ColorTitulacion, p);

            RendererLiquido.material.color = colorActual;
        }
    }



    private void TitularLiquido()
    {
        cambiandoColor = true;
        tiempoRestante = TiempoTitulacion;
    }


    private void IngresarLiquido(float cantidad)
    {
        CantidadLiquido = CantidadLiquido + cantidad;
    }


    IEnumerator CorutinaGoteo()
    {
        while(true)
        { 
            IngresarLiquido(1);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
