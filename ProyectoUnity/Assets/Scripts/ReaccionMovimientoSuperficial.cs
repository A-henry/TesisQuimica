using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionMovimientoSuperficial : ReaccionQuimica
{
    float x = 0;
    float y = 0;
    float w = 0;
    public float VelocidadMovimiento = 0.1f;
    public float VelocidadRotacion = 0.03f;
    public float VelocidadRotacionEsfera = 0.2f;
    public float AmplitudMovimiento = 5;
    public float AmplitudRotacion = 45f;
    public float AmplitudRotacionEsfera = 120f;
    public float RadioEsfera = 1f;
    public Transform Pivot;
    public Transform Esfera;
    public Transform Fuego;
    public float CantidadInicial;


    Vector3 _tamanhoOriginal;


    void Start()
    {
        Esfera.gameObject.SetActive(false);
    }


    void Update()
    {
        if (reaccionando)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;
            tiempoRestante = Mathf.Max(0, tiempoRestante);

            Esfera.localScale = _tamanhoOriginal * (tiempoRestante / TiempoReaccion);

            if (tiempoRestante <= 0)
            {
                reaccionando = false;
                Destroy(gameObject, 20f);
            }


            Vector3 pos = transform.position;

            Fuego.position = Esfera.position + Vector3.up * 0.04f;

            float adelante = Mathf.PerlinNoise(x, 0);
            adelante = (AmplitudMovimiento - RadioEsfera) * (adelante * 2 - 1);

            float rotacion = Mathf.PerlinNoise(0, y);
            rotacion = AmplitudRotacion * (rotacion * 2 - 1);

            float rotacionEsfera = Mathf.PerlinNoise(w, w);
            rotacionEsfera = AmplitudRotacionEsfera * (rotacionEsfera * 2 - 1);

            w = w + VelocidadRotacionEsfera;

            x = x + VelocidadMovimiento;
            y = y + VelocidadRotacion;

            Pivot.localPosition = new Vector3(0, 0, adelante);
            transform.rotation = Quaternion.Euler(0, rotacion, 0);

            Esfera.rotation = Quaternion.Euler(rotacionEsfera, rotacionEsfera, -rotacionEsfera);
        }
    }


    public override void Reaccionar()
    {
        reaccionando = true;
        tiempoRestante = TiempoReaccion;
        Esfera.gameObject.SetActive(true);

        float escala = Mathf.Lerp(0, 0.06f, CantidadInicial / 20f);
        RadioEsfera = escala;
        Esfera.localScale = Vector3.one * escala;
        _tamanhoOriginal = Esfera.localScale;
    }
}
