using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foco : MonoBehaviour
{
    [Range(0,1f)]
    public float Nivel;

    public UnityEngine.Material FocoEncendido;
    public UnityEngine.Material FocoApagado;

    Renderer _rend;
    Light _luz;

    void Awake()
    {
        _rend = GetComponent<Renderer>();
        _luz = GetComponentInChildren<Light>();
        Nivel = 0;
    }

    void Start()
    {
        _rend.material = FocoApagado;
    }

	
	void Update ()
    {
        if(Nivel > 0)
        {
            _rend.material = FocoEncendido;

            _rend.material.SetColor("_EmissionColor", Color.Lerp(new Color(0.5f, 0.5f, 0.5f), Color.white, Nivel));
            _luz.intensity = Mathf.Lerp(0.5f, 2f, Nivel);

        } else
        {
            _rend.material = FocoApagado;
            _luz.intensity = 0;
        }
		
	}


    public void Conectar(float nivel)
    {
        Nivel = nivel;
    }


    public void Apagar()
    {
        Nivel = 0;
    }
}
