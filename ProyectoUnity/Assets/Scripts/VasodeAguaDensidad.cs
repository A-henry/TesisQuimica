using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasodeAguaDensidad : MaterialReactivo {

    public float CantidadSodio;

    public Transform liquido;
    protected ExperimentoDensidad _experimento;

    public GameObject posicionAgua;

    protected override void Start()
    {
        base.Start();

        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDensidad");
        _experimento = obj.GetComponent<ExperimentoDensidad>();

        NombreReactivo = "AGUA";
    }

    public void aumentarLiquidoCuadrado(float cantidad)
    {
        Vector3 s = liquido.localScale;
        liquido.localScale = new Vector3(s.x, s.y + cantidad, s.z);

    }

    public void aumentarLiquidoEsfera(float  cantidad)
    {
        Vector3 s = liquido.localScale;
        liquido.localScale = new Vector3(s.x, s.y+cantidad, s.z);
        
    }
    public void DisminuirLiquidoEsfera(float cantidad)
    {
        Vector3 s = liquido.localScale;
        liquido.localScale = new Vector3(s.x, s.y - cantidad, s.z);

    }

    public Transform PosicionAgua()
    {
        Transform trConexion = null;
        trConexion = posicionAgua.transform;

        return trConexion;
    }

}
