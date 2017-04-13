using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorLiquido : MaterialReactivo
{
    public GameObject MarcadorA;
    public GameObject MarcadorB;

    public string Nombre;

    [Range(0,10)]
    public int Conductividad = 10;


    bool _conectadoA;
    bool _conectadoB;

    void Awake()
    {
        _conectadoA = false;
        _conectadoB = false;
    }


    protected override void Start()
    {
        MarcadorA.SetActive(false);
        MarcadorB.SetActive(false);
    }



    public void MarcarTipo(string tipoCable)
    {
        if(tipoCable == "A")
        {
            MarcadorA.SetActive(true);
            MarcadorB.SetActive(false);
        }


        if (tipoCable == "B")
        {
            MarcadorA.SetActive(false);
            MarcadorB.SetActive(true);
        }
    }


    public override void Desmarcar()
    {
        MarcadorA.SetActive(false);
        MarcadorB.SetActive(false);
    }


    public Transform Conexion(string tipoCable)
    {
        Transform trConexion=null;
        if (tipoCable == "A")
        {
            trConexion = MarcadorA.transform;
        }

        if (tipoCable == "B")
        {
            trConexion = MarcadorB.transform;
        }

        return trConexion;
    }


    public void ConexionFisica(string tipoCable, bool estaConectado)
    {
        if (tipoCable == "A")
        {
            _conectadoA = estaConectado;
        }

        if (tipoCable == "B")
        {
            _conectadoB = estaConectado;
        }


        if (_conectadoA && _conectadoB)
        {
            ExperimentoConductividad.Instancia.Conectar(Conductividad);

            UIConductividad.Instancia.MostrarPanelConductor(Nombre, Conductividad);
        } else
        {
            ExperimentoConductividad.Instancia.Desconectar();
            UIConductividad.Instancia.OcultarPanelConductor();
        }

    }

}
