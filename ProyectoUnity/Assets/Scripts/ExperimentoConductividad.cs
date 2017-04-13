using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoConductividad : MonoBehaviour
{
    private static ExperimentoConductividad _instancia;
    public static ExperimentoConductividad Instancia {
        get {return _instancia;}
    }


    [HideInInspector]
    public GameObject Agarrado;

    public Foco Foco;


    void Awake()
    {
        _instancia = this;
    }



    public void Agarrar(GameObject agarrable)
    {
        Agarrado = agarrable;
        MaterialAgarrableCable cable = agarrable.GetComponent<MaterialAgarrableCable>();
        cable.Agarrar();

        UIConductividad.Instancia.EstadoCable(cable.Tipo, null);
    }


    public void Soltar()
    {
        if (Agarrado != null)
        {
            Agarrado.GetComponent<MaterialAgarrableCable>().Soltar();
            Agarrado = null;
        }
    }



    public void Conectar(int conductividad)
    {
        if (conductividad > 0)
        {
            float p = conductividad / 10f;

            Foco.Conectar(p);
        }
    }


    public void Desconectar()
    {
        Foco.Apagar();
    }


}
