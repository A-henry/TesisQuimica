using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoDensidad : MonoBehaviour {


    public GameObject Agarrado;

    public enum EnumSeleccion { Nada, Agarrado, MarcandoReactivo, Reaccionando };
    public MaterialAgarrableEsfera esfera;
    public List<GameObject> objetos;
    public List<MaterialReactivo> Reactivos;
    public MaterialReactivo ReactivoPosible;
    public MaterialReactivo ReactivoEnReaccion;
    public EnumSeleccion EstadoSeleccion;
    

    public GameObject Seleccionado;

    class RelacionMasaVolumen
    {
        public float Masa;
        public float Volumen;
        public float densidad;

        public RelacionMasaVolumen(float ma, float vol, float de)
        {
            Masa = ma;
            Volumen = vol;
            densidad = de;
        }
    }

    List<RelacionMasaVolumen> _tablaExperimental;

    void Awake()
    {
        _tablaExperimental = new List<RelacionMasaVolumen>();
        _tablaExperimental.Add(new RelacionMasaVolumen(50, 5, 10));
        _tablaExperimental.Add(new RelacionMasaVolumen(75, 8, 9.38f));
        _tablaExperimental.Add(new RelacionMasaVolumen(100, 11, 9.09f));
        _tablaExperimental.Add(new RelacionMasaVolumen(125, 13, 9.62f));
        _tablaExperimental.Add(new RelacionMasaVolumen(150, 17, 8.82f));
        _tablaExperimental.Add(new RelacionMasaVolumen(175, 20, 8.75f));
        _tablaExperimental.Add(new RelacionMasaVolumen(200, 25, 8));
        _tablaExperimental.Add(new RelacionMasaVolumen(225, 28, 8.04f));
        _tablaExperimental.Add(new RelacionMasaVolumen(250, 32, 7.81f));
        _tablaExperimental.Add(new RelacionMasaVolumen(275, 36, 7.64f));
        _tablaExperimental.Add(new RelacionMasaVolumen(300, 41, 7.32f));



        EstadoSeleccion = EnumSeleccion.Nada;
    }



    public void Seleccionar(GameObject obj)
    {
        DeseleccionarTodos();
        Seleccionado = obj;
        Material m = obj.GetComponent<Material>();
        m.seleccionado = true;

    }

    public void DeseleccionarTodos()
    {

        for (int i = 0; i < objetos.Count; i++)
        {
            Material m = objetos[i].GetComponent<Material>();
            m.seleccionado = false;
        }

        Seleccionado = null;
    }


    public void Agarrar(MaterialAgarrableEsfera m)
    {

        Agarrado = m.gameObject;
        EstadoSeleccion = EnumSeleccion.Agarrado;
        m.Agarrar();

    }


    public void Soltar()
    {
        if (Agarrado == null)
            return;

        Agarrado.GetComponent<MaterialAgarrableEsfera>().Soltar();
        Agarrado = null;

        if (EstadoSeleccion == EnumSeleccion.MarcandoReactivo)
        {
            ReactivoPosible.Desmarcar();
            ReactivoPosible = null;
        }

        EstadoSeleccion = EnumSeleccion.Nada;

    }


    public void DesmarcarTodosReactivos()
    {
        for (int i = 0; i < Reactivos.Count; i++)
        {
            Reactivos[i].Desmarcar();
        }

        ReactivoPosible = null;

    }


    public bool MarcarReactivo(MaterialReactivo reactivo)
    {
        if (reactivo.Usado == false)
        {
            ReactivoPosible = reactivo;
            EstadoSeleccion = EnumSeleccion.MarcandoReactivo;
            reactivo.Marcar();


            return true;
        }

        return false;
    }

    public void VolverEstadoAgarrado()
    {
        EstadoSeleccion = EnumSeleccion.Agarrado;
        ReactivoPosible.Desmarcar();
        ReactivoPosible = null;
    }



    public void Accion(GameObject reactivo)
    {
        EstadoSeleccion = EnumSeleccion.Reaccionando;
        
        VasodeAguaDensidad agua = reactivo.GetComponent<VasodeAguaDensidad>();
        MaterialAgarrableCuadrado cuadrado = Agarrado.GetComponent<MaterialAgarrableCuadrado>();

        MaterialAgarrableEsfera esfera = Agarrado.GetComponent<MaterialAgarrableEsfera>();

        if (cuadrado != null)
        {
            float cantidadVolumen = CalcularVolumenReaccion(cuadrado.Cantidad);
            float volRelativo = CambiarCantidadVolumenRel(cantidadVolumen);
            agua.aumentarLiquidoCuadrado(volRelativo);
        }
        else
            if(esfera != null)
            {   
            float cantidadVolumen = CalcularVolumenReaccion(esfera.Cantidad);
            float volRelativo = CambiarCantidadVolumenRel(cantidadVolumen);
            agua.aumentarLiquidoEsfera(volRelativo);
            //esfera.TransformConexion = agua.PosicionAgua();
            }
     
    }

    public void DisminuirLiquido(GameObject reactivo)
    {
        VasodeAguaDensidad agua = reactivo.GetComponent<VasodeAguaDensidad>();
            float cantidadVolumen = CalcularVolumenReaccion(esfera.Cantidad);
            float volRelativo = CambiarCantidadVolumenRel(cantidadVolumen);
            agua.DisminuirLiquidoEsfera(volRelativo);
            
        
    }
    public float CambiarCantidadEsferaRel(float rel)
    {
        float cantidadReal = Mathf.Lerp(50, 300f, rel);
        esfera.Cantidad = cantidadReal;
        return cantidadReal;
    }


    public float CambiarCantidadVolumenRel(float volReal)
    {
        
        float volumenRelativo = (volReal / 41f)/10;
        return volumenRelativo;
    }

    private float CalcularVolumenReaccion(float cantidad)
    {
        int idx = 0;
        while (_tablaExperimental[idx].Masa < cantidad && _tablaExperimental[idx + 1].Masa < cantidad)
        {
            idx++;
        }

        RelacionMasaVolumen anterior = _tablaExperimental[idx];
        RelacionMasaVolumen siguiente = _tablaExperimental[idx + 1];
        float p = (cantidad - anterior.Masa) / (siguiente.Masa - anterior.Masa);
        float vol = anterior.Volumen + ((siguiente.Volumen - anterior.Volumen) * p);

        return vol;
    }
}
