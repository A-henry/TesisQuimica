using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoClorato : MonoBehaviour
{
    public TuboClorato Tubo;
    public ParticleSystem Mecha;
    public float TiempoDisolucion = 2f;
    bool _activo = false;
    float _tiempoRestante;

    public GameObject Agarrado;



    void Awake()
    {
        _activo = false;
    }


	void Start () {
		
	}
	
	void Update ()
    {
        if (_activo)
        {
            _tiempoRestante = _tiempoRestante - Time.deltaTime;

            _tiempoRestante = Mathf.Max(0, _tiempoRestante);

            float p = _tiempoRestante / TiempoDisolucion;

            Tubo.Disolucion(p);
        }
		
	}


    public void SwitchMechero()
    {

        if(_activo)
        {
            Mecha.Stop();
            _activo = false;
        } else if(Tubo.Cantidad > 0)
        {
            _activo = true;
            Mecha.Play();
            _tiempoRestante = TiempoDisolucion;
        }
    }


    public void Agarrar (GameObject go)
    {
        Agarrado = go;
        go.GetComponent<MaterialAgarrable>().Agarrar();
    }


    public void Soltar()
    {
        Agarrado.GetComponent<MaterialAgarrable>().Soltar();
        Agarrado = null;
    }


    public void Accion(GameObject reactivo)
    {
        MaterialReactivoCloratoEnPolvo clorato = reactivo.GetComponent<MaterialReactivoCloratoEnPolvo>();
        TuboClorato tubo = reactivo.GetComponent<TuboClorato>();

        MaterialAgarrableCuchara cuchara = Agarrado.GetComponent<MaterialAgarrableCuchara>();
        MaterialAgarrablePapel papel = Agarrado.GetComponent<MaterialAgarrablePapel>();

        if (clorato != null)
        {
            if(cuchara != null)
            {
                cuchara.Llenar();
            }
        } else if(tubo != null)
        {
            if(cuchara != null)
            {
                if(cuchara.Cantidad > 0)
                {
                    tubo.AumentarClorato(cuchara.Cantidad);
                    cuchara.Vaciar();
                }
            } else if(papel != null)
            {
                if(Tubo.Cantidad > 0)
                {
                    tubo.HecharPapel();
                    Destroy(Agarrado);
                    Agarrado = null;
                }
            }
        }
    }


    public bool SobreReactivo(MaterialReactivo reactivo)
    {
        if (Agarrado == null)
            return false;

        bool esCuchara = Agarrado.GetComponent<MaterialAgarrableCuchara>() != null;
        bool esPapel = Agarrado.GetComponent<MaterialAgarrablePapel>() != null;

        bool esTubo = reactivo is TuboClorato;
        bool esClorato = reactivo is MaterialReactivoCloratoEnPolvo;

        if (esCuchara && esClorato)
            return true;

        if (esCuchara && Agarrado.GetComponent<MaterialAgarrableCuchara>().Cantidad > 0 && esTubo)
            return true;

        if (esPapel && Tubo.Cantidad > 0 && Tubo.Disuelto)
            return true;

        return false;

    }



}
