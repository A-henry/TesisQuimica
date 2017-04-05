using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboClorato : MaterialReactivo
{
    public Transform PosicionCloratoEnPolvo;
    public GameObject PrototipoCloratoEnPolvo;
    public GameObject[] CloratoLiquido;
    public MeshRenderer TuboRenderer;
    public UnityEngine.Material MaterialCaliente;
    public ParticleSystem Vapor;
    public bool Disuelto;


    bool _terminandoReaccion;
    float tasaEmisionVapor;

    GameObject nivelCloratoActual;

    void Awake()
    {
        nivelCloratoActual = null;
        _terminandoReaccion = false;
        Disuelto = false;
    }


	protected override void Start ()
    {
        base.Start();

        Cantidad = 0;

        for(int i = 0; i < CloratoLiquido.Length; i++)
        {
            CloratoLiquido[i].SetActive(false);
        }
	}

    
	void Update ()
    {
        if(_terminandoReaccion)
        {
            tasaEmisionVapor -= 10f * Time.deltaTime;
            Vapor.emissionRate = tasaEmisionVapor;

            if(tasaEmisionVapor <= 0)
            {
                Vapor.Stop();
                _terminandoReaccion = false;
            }
        }
	}



    public void Disolucion(float factor)
    {
        MeshRenderer rend = nivelCloratoActual.GetComponent<MeshRenderer>();

        Color c = rend.material.GetColor("_Color");

        rend.material.SetColor("_Color", new Color(c.r, c.g, c.b, factor));

        CalentarTubo(1f-factor);
    }


    public void HecharPapel (GameObject papel)
    {
        papel.transform.position = PosicionCloratoEnPolvo.position;
    }


    public void PapelDetectado()
    {
        Vapor.Play();
        TuboRenderer.material = MaterialCaliente;
        ExperimentoClorato.Instancia.Reaccionar();
        Invoke("TerminarReaccion", TiempoReaccion);
    }


    void TerminarReaccion()
    {
        _terminandoReaccion = true;
        tasaEmisionVapor = 30;
    }



    public void CalentarTubo(float factor)
    {
        TuboRenderer.material.SetColor("_EmissionColor", Color.LerpUnclamped(Color.black, Color.red, factor*10));
    }



    public void HecharClorato(float cantidad)
    {
        GameObject cloratoGenerado = (GameObject)Instantiate(PrototipoCloratoEnPolvo, PosicionCloratoEnPolvo.position, PosicionCloratoEnPolvo.rotation);
        Destroy(cloratoGenerado, 3.5f);

        StartCoroutine(_AumentarClorato(cantidad));
    }


    IEnumerator _AumentarClorato(float cantidad)
    {
        yield return new WaitForSeconds(1.3f);

        Cantidad = Mathf.Min(2.5f, Cantidad + cantidad);

        UIClorato.Instancia.CambiarCantidadClorato(Cantidad);
        UIClorato.Instancia.InstruccionMechero();

        if (nivelCloratoActual == null)
        {
            nivelCloratoActual = CloratoLiquido[0];
            nivelCloratoActual.SetActive(true);
        }
        else if (nivelCloratoActual == CloratoLiquido[0])
        {
            nivelCloratoActual.SetActive(false);
            nivelCloratoActual = CloratoLiquido[1];
            nivelCloratoActual.SetActive(true);
        }
        else if (nivelCloratoActual == CloratoLiquido[1])
        {
            nivelCloratoActual.SetActive(false);
            nivelCloratoActual = CloratoLiquido[2];
            nivelCloratoActual.SetActive(true);
        }
        else if (nivelCloratoActual == CloratoLiquido[2])
        {
            nivelCloratoActual.SetActive(false);
            nivelCloratoActual = CloratoLiquido[3];
            nivelCloratoActual.SetActive(true);
        }
    }
}
