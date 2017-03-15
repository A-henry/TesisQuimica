using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoPrecipitado : MonoBehaviour
{
    public Transform Liquido;
    public Renderer RendererLiquido;
    public float CapacidadMaxima = 100; // ml

    [Range(0, 100f)]
    private float cantidadLiquido;
    public float CantidadLiquido
    {
        get { return cantidadLiquido; }
        set {
            cantidadLiquido = value;
            VolumenTitulacion = CalcularVolumenTitulacion(cantidadLiquido);
        }
    }
        

    public Color ColorTitulacion = Color.magenta;
    public float TiempoTitulacion = 2f;
    public float VolumenTitulacion = 50; // ml

    public GameObject PrototipoGoteo;
    public Transform LugarGoteo;

    List<RelacionTitulacion> tablaExperimental;
    bool estaTitulado;
    bool cambiandoColor;
    Color colorOriginal;
    float tiempoRestante;


    void Start()
    {
        cambiandoColor = false;
        estaTitulado = false;
        colorOriginal = RendererLiquido.material.color;


        tablaExperimental = new List<RelacionTitulacion>();
        tablaExperimental.Add(new RelacionTitulacion(5, 6));
        tablaExperimental.Add(new RelacionTitulacion(10, 12.35f));
        tablaExperimental.Add(new RelacionTitulacion(15, 18.25f));
        tablaExperimental.Add(new RelacionTitulacion(20, 27));
        tablaExperimental.Add(new RelacionTitulacion(25, 32.96f));
        tablaExperimental.Add(new RelacionTitulacion(30, 38.88f));
        tablaExperimental.Add(new RelacionTitulacion(35, 45.75f));
        tablaExperimental.Add(new RelacionTitulacion(40, 50.88f));
        tablaExperimental.Add(new RelacionTitulacion(45, 56.31f));
        tablaExperimental.Add(new RelacionTitulacion(50, 61.63f));
        tablaExperimental.Add(new RelacionTitulacion(55, 68.45f));
        tablaExperimental.Add(new RelacionTitulacion(60, 74.88f));
        tablaExperimental.Add(new RelacionTitulacion(65, 80.34f));
        tablaExperimental.Add(new RelacionTitulacion(70, 86.38f));
        tablaExperimental.Add(new RelacionTitulacion(75, 92.01f));
        tablaExperimental.Add(new RelacionTitulacion(80, 97.38f));
    }


    void Update()
    {
        // Cantidad de Liquido
        float esc = cantidadLiquido / CapacidadMaxima;
        Vector3 s = Liquido.localScale;
        Liquido.localScale = new Vector3(s.x, esc, s.z);

        if (cantidadLiquido >= VolumenTitulacion) {
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


    public void IngresarLiquido(float cantidad)
    {
        cantidadLiquido = cantidadLiquido + cantidad;

        Instantiate(PrototipoGoteo, LugarGoteo.position, LugarGoteo.rotation);
    }


    private float CalcularVolumenTitulacion(float cantidad)
    {
        int idx = 0;
        while (tablaExperimental[idx].Volumen < cantidad && tablaExperimental[idx + 1].Volumen < cantidad)
        {
            idx++;
        }


        RelacionTitulacion anterior = tablaExperimental[idx];
        RelacionTitulacion siguiente = tablaExperimental[idx+1];

        float p = (cantidad-anterior.Volumen) / (siguiente.Volumen - anterior.Volumen);
        float vol = anterior.VolumenTitulacion + ((siguiente.VolumenTitulacion - anterior.VolumenTitulacion) * p);

        return vol;
    }


    class RelacionTitulacion
    {
        public RelacionTitulacion(float vol, float volTitulacion) {
            Volumen = vol;
            VolumenTitulacion = volTitulacion;
        }

        public float Volumen;
        public float VolumenTitulacion;
    }


   


}
