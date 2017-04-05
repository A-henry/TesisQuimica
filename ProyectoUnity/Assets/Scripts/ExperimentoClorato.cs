using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoClorato : MonoBehaviour
{
    private static ExperimentoClorato _instancia;
    public static ExperimentoClorato Instancia
    {
        get
        {
            return _instancia;
        }
    }


    public TuboClorato Tubo;
    public ParticleSystem Mecha;
    [HideInInspector]
    public float TiempoDisolucion = 2f;
    bool _activo = false;
    bool _reaccionando = false;
    float _tiempoRestante;

    public GameObject Agarrado;



    void Awake()
    {
        _instancia = this;
        _activo = false;
    }


	void Update ()
    {
        if (Tubo.Disuelto && _reaccionando)
        {
            UIClorato.Instancia.ActualizarTiempoReaccion(Tubo.TiempoReaccion - _tiempoRestante);

            _tiempoRestante = _tiempoRestante - Time.deltaTime;

            _tiempoRestante = Mathf.Max(0, _tiempoRestante);

            float p = _tiempoRestante / Tubo.TiempoReaccion;
        }
        else if (_activo)
        {
            UIClorato.Instancia.ActualizarTiempoDisolucion(TiempoDisolucion - _tiempoRestante);

            _tiempoRestante = _tiempoRestante - Time.deltaTime;

            _tiempoRestante = Mathf.Max(0, _tiempoRestante);

            float p = _tiempoRestante / TiempoDisolucion;

            if(_tiempoRestante == 0)
            {
                Tubo.Disuelto = true;

                UIClorato.Instancia.InstruccionPapel();
            }

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

            TiempoDisolucion = _CalcularTiempoDisolucion(Tubo.Cantidad);
            Tubo.TiempoReaccion = _CalcularTiempoReaccion(Tubo.Cantidad);

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
                    tubo.HecharClorato(cuchara.Cantidad);
                    cuchara.Vaciar();
                }
            } else if(papel != null)
            {
                if(Tubo.Cantidad > 0 && Tubo.Disuelto)
                {
                    Agarrado = null;
                    papel.Hechar();
                    tubo.HecharPapel(papel.gameObject);
                }
            }
        }
    }


    public void Reaccionar()
    {
        _reaccionando = true;
        _tiempoRestante = Tubo.TiempoReaccion;
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



    private float _CalcularTiempoDisolucion(float cantidad) // cantidad de clorato en polvo
    {
        if(Mathf.Abs(0.5f - cantidad) <= float.Epsilon)  return 15f;

        if (Mathf.Abs(1f - cantidad) <= float.Epsilon) return 30f;

        if (Mathf.Abs(1.5f - cantidad) <= float.Epsilon) return 43f;

        if (Mathf.Abs(2f - cantidad) <= float.Epsilon) return 58f;


        // por defecto si no es ninguno de los anteriores
        return 71f;
    }


    private float _CalcularTiempoReaccion(float cantidad) // cantidad de clorato en polvo
    {
        if (Mathf.Abs(0.5f - cantidad) <= float.Epsilon) return 10f;

        if (Mathf.Abs(1f - cantidad) <= float.Epsilon) return 12f;

        if (Mathf.Abs(1.5f - cantidad) <= float.Epsilon) return 14f;

        if (Mathf.Abs(2f - cantidad) <= float.Epsilon) return 15f;


        // por defecto si no es ninguno de los anteriores
        return 17f;
    }

}
