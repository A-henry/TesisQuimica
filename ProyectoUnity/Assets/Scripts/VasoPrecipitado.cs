using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class VasoPrecipitado : MonoBehaviour
{
    public Transform Liquido;
    public Renderer RendererLiquido;
    public float CapacidadMaxima = 100; // ml

    ExperimentoTitulacion experimento;

    [Range(0, 100f)]
    private float cantidadLiquido= 45.56f;
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



    void Awake()
    {
        tablaExperimental = new List<RelacionTitulacion>();
        tablaExperimental.Add(new RelacionTitulacion(5, 6, 11.1f));
        tablaExperimental.Add(new RelacionTitulacion(10, 12.35f, 11.23f));
        tablaExperimental.Add(new RelacionTitulacion(15, 18.25f, 11.43f));
        tablaExperimental.Add(new RelacionTitulacion(20, 27, 11.57f));
        tablaExperimental.Add(new RelacionTitulacion(25, 32.96f, 11.57f));
        tablaExperimental.Add(new RelacionTitulacion(30, 38.88f, 11.57f));
        tablaExperimental.Add(new RelacionTitulacion(35, 45.75f, 11.59f));
        tablaExperimental.Add(new RelacionTitulacion(40, 50.88f, 11.59f));
        tablaExperimental.Add(new RelacionTitulacion(45, 56.31f, 11.6f));
        tablaExperimental.Add(new RelacionTitulacion(50, 61.63f, 11.61f));
        tablaExperimental.Add(new RelacionTitulacion(55, 68.45f, 11.61f));
        tablaExperimental.Add(new RelacionTitulacion(60, 74.88f, 11.65f));
        tablaExperimental.Add(new RelacionTitulacion(65, 80.34f, 11.68f));
        tablaExperimental.Add(new RelacionTitulacion(70, 86.38f, 11.71f));
        tablaExperimental.Add(new RelacionTitulacion(75, 92.01f, 11.71f));
        tablaExperimental.Add(new RelacionTitulacion(80, 97.38f, 11.71f));

        cambiandoColor = false;
        estaTitulado = false;

        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        Assert.IsNotNull(go, "No se encuentra el objeto con tag GameController");

        experimento = go.GetComponent<ExperimentoTitulacion>();
        Assert.IsNotNull(experimento, "No es posible encontrar el objeto con script ExperimentoTitulacion");
    }



    void Start()
    {
        colorOriginal = RendererLiquido.material.color;
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

        cantidadLiquido = Mathf.Min(cantidadLiquido, CapacidadMaxima);

        Instantiate(PrototipoGoteo, LugarGoteo.position, LugarGoteo.rotation);

        experimento.CambiarVolumenAcido(cantidadLiquido);
        
    }


    public float CalcularVolumenTitulacion(float cantidad)
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
        float ph = anterior.PH + (siguiente.PH - anterior.PH) * p;
        
        experimento.CambiarTitulacion(vol);
        experimento.CambiarPh(ph);
       
        return vol;
    }





    class RelacionTitulacion
    {
        public RelacionTitulacion(float vol, float volTitulacion, float ph) {
            Volumen = vol;
            VolumenTitulacion = volTitulacion;
            PH = ph;
        }

        public float Volumen;
        public float VolumenTitulacion;
        public float PH;
    }





}
