using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConductividad : MonoBehaviour
{
    private static UIConductividad _instancia;
    public static UIConductividad Instancia
    {
        get { return _instancia; }
    }

    public Text TextoCableA;
    public Text TextoCableB;
    public GameObject PanelConductor;
    public Text TextoNombreConductor;
    public Text TextoConductividad;


    void Awake()
    {
        _instancia = this;
    }


    void Start()
    {
        PanelConductor.SetActive(false);
    }


    public void EstadoCable(string tipoCable, ConductorLiquido conductor)
    {
        if(tipoCable == "A")
        {
            if (conductor == null)
            {
                TextoCableA.text = "Cable A: Desconectado";
            } else
            {
                TextoCableA.text = "Cable A: Conectado a " + conductor.Nombre;
            }
        }


        if (tipoCable == "B")
        {
            if (conductor == null)
            {
                TextoCableB.text = "Cable B: Desconectado";
            }
            else
            {
                TextoCableB.text = "Cable B: Conectado a " + conductor.Nombre;
            }
        }
    }



    public void MostrarPanelConductor(string nombre, int conductividad)
    {
        PanelConductor.SetActive(true);
        TextoNombreConductor.text = "Conductor: " + nombre;
        TextoConductividad.text = "Numero de electrones en liquido: " + conductividad;
    }



    public void OcultarPanelConductor()
    {
        PanelConductor.SetActive(false);
    }

}
