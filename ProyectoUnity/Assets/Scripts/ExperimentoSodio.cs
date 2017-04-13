using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoSodio : MonoBehaviour
{
    [HideInInspector]
    public GameObject Agarrado;

    public MaterialAgarrableSodio Sodio;

    class RelacionSodioTiempo
    {
        public float Cantidad;
        public float TiempoReaccion;

        public RelacionSodioTiempo(float na, float mm, float ss)
        {
            Cantidad = na;
            TiempoReaccion = mm*60 + ss;
        }
    }

    List<RelacionSodioTiempo> _tablaExperimental;

    void Awake()
    {
        _tablaExperimental = new List<RelacionSodioTiempo>();
        _tablaExperimental.Add(new RelacionSodioTiempo(2, 0, 26));
        _tablaExperimental.Add(new RelacionSodioTiempo(4, 0, 55));
        _tablaExperimental.Add(new RelacionSodioTiempo(6, 1, 15));
        _tablaExperimental.Add(new RelacionSodioTiempo(8, 1, 27));
        _tablaExperimental.Add(new RelacionSodioTiempo(12, 2, 25));
        _tablaExperimental.Add(new RelacionSodioTiempo(14, 2, 49));
        _tablaExperimental.Add(new RelacionSodioTiempo(10, 1, 59));
        _tablaExperimental.Add(new RelacionSodioTiempo(16, 3, 1));
        _tablaExperimental.Add(new RelacionSodioTiempo(18, 3, 30));
        _tablaExperimental.Add(new RelacionSodioTiempo(20, 3, 45));
    }



    public void Agarrar(GameObject agarrable)
    {
        Agarrado = agarrable;
        agarrable.GetComponent<MaterialAgarrable>().Agarrar();
    }




    public void Soltar()
    {
        if (Agarrado != null)
        {
            Agarrado.GetComponent<MaterialAgarrableSodio>().Soltar();
            Agarrado = null;
        }
    }


    public void Accion(GameObject reactivo)
    {
        VasoAgua agua = reactivo.GetComponent<VasoAgua>();

        float cantidad = Agarrado.GetComponent<MaterialAgarrableSodio>().Cantidad;

        float tiempoReaccion = CalcularTiempoReaccion(cantidad);

        agua.TiempoReaccion = tiempoReaccion;
        agua.CantidadSodio = cantidad;
        agua.ReaccionQuimica();

        UISodio.Instancia.ReaccionQuimica();

        Destroy(Agarrado);

        Soltar();
    }

    public float CambiarCantidadSodioRel(float rel)
    {
        float cantidadReal = Mathf.Lerp(2, 20f, rel);

        Sodio.Cantidad = cantidadReal;

        return cantidadReal;
    }



    private float CalcularTiempoReaccion(float cantidad)
    {
        int idx = 0;
        while (_tablaExperimental[idx].Cantidad < cantidad && _tablaExperimental[idx + 1].Cantidad < cantidad)
        {
            idx++;
        }

        RelacionSodioTiempo anterior = _tablaExperimental[idx];
        RelacionSodioTiempo siguiente = _tablaExperimental[idx + 1];

        float p = (cantidad - anterior.Cantidad) / (siguiente.Cantidad - anterior.Cantidad);
        float tiempo = anterior.TiempoReaccion + ((siguiente.TiempoReaccion - anterior.TiempoReaccion) * p);

        return tiempo;
    }
}
