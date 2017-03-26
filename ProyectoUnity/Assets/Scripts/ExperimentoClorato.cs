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


    public void Empezar()
    {
        _activo = true;
        _tiempoRestante = TiempoDisolucion;
        Mecha.Play();
    }
}
