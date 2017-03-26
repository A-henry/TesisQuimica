using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboClorato : MonoBehaviour
{
    public Transform PosicionCloratoEnPolvo;
    public GameObject PrototipoCloratoEnPolvo;
    public GameObject[] CloratoLiquido;
    public MeshRenderer TuboRenderer;
    public ParticleSystem Vapor;


    bool _terminandoReaccion;
    float tasaEmisionVapor;

    GameObject nivelCloratoActual;

    void Awake()
    {
        nivelCloratoActual = null;
        _terminandoReaccion = false;
    }


	void Start ()
    {
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


    public void HecharClorato()
    {
        GameObject cloratoGenerado = (GameObject)Instantiate(PrototipoCloratoEnPolvo, PosicionCloratoEnPolvo.position, PosicionCloratoEnPolvo.rotation);
        Destroy(cloratoGenerado, 3f);
        Invoke("AumentarClorato", 1f);
    }


    public void Disolucion(float factor)
    {
        MeshRenderer rend = nivelCloratoActual.GetComponent<MeshRenderer>();

        Color c = rend.material.GetColor("_Color");

        rend.material.SetColor("_Color", new Color(c.r, c.g, c.b, factor));

        CalentarTubo(1f-factor);
    }


    public void HecharPapel ()
    {
        Vapor.Play();

        Invoke("TerminarReaccion", 2);
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




    private void AumentarClorato()
    {
        if (nivelCloratoActual == null)
        {
            nivelCloratoActual = CloratoLiquido[0];
            nivelCloratoActual.SetActive(true);
        } else if(nivelCloratoActual == CloratoLiquido[0])
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
