using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionSistemaParticulas : ReaccionQuimica
{
    ParticleSystem _particulas;
    public bool _muriendo = false;
    public float _rateOriginal;
    public float _tamanhoOriginal;

    public float TiempoMuerte = 2f;
    public bool BajarRate = false;
    public bool BajarTamanho = false;
    public float TiempoDestruccion = 20f;

	void Start ()
    {
        _particulas = GetComponent<ParticleSystem>();
        _particulas.Stop();
	}
	
	void Update ()
    {
        if(reaccionando)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;
            tiempoRestante = Mathf.Max(0, tiempoRestante);

            if (tiempoRestante <= 0)
            {
                reaccionando = false;
                _muriendo = true;
                tiempoRestante = TiempoMuerte;
                _rateOriginal = _particulas.emissionRate;
                _tamanhoOriginal = _particulas.startSize;
            }
        }

        if(_muriendo)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;
            tiempoRestante = Mathf.Max(0, tiempoRestante);

            if(BajarRate)
                _particulas.emissionRate = Mathf.Lerp(_rateOriginal, 0, 1f - (tiempoRestante / TiempoMuerte));

            if(BajarTamanho)
                _particulas.startSize = Mathf.Lerp(_tamanhoOriginal, 0, 1f - (tiempoRestante / TiempoMuerte));

            if (tiempoRestante <= 0)
            {
               Destroy(gameObject, TiempoDestruccion);
            }
        }


	}


    public override void Reaccionar()
    {
        reaccionando = true;
        tiempoRestante = TiempoReaccion;
        _particulas.Play();
    }
}
