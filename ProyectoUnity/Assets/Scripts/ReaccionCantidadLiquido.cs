using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionCantidadLiquido : ReaccionQuimica
{
	//public float EscalaMaxY = 1f;  // escala de unity
	public float CantidadMaxima = 150f;  // ml
	public float CantidadInicial = 90;//ml
	public float CantidadFinal = 40f; // ml


	float mls2Esc;
	float perdidaEscPorSegundo;


	void Start()
	{
        Vector3 s = transform.localScale;
        transform.localScale = new Vector3(s.x, CantidadInicial / CantidadMaxima, s.z);
	}


	void Update () 
	{
		if (reaccionando) {
			tiempoRestante = tiempoRestante - Time.deltaTime;

			if (tiempoRestante < 0) {
				reaccionando = false;
			}

            float p = 1f - (tiempoRestante / TiempoReaccion);

            float cantidadActual = ((CantidadFinal - CantidadInicial) * p) + CantidadInicial;

            Vector3 s = transform.localScale;
            transform.localScale = new Vector3(s.x, cantidadActual / CantidadMaxima, s.z);

            if(cantidadActual <= 0)
            {
                Destroy(gameObject);
            }
		}
	}


    public void CambiarCantidadInicial(float cantidad)
    {
        CantidadInicial = cantidad;

        Vector3 s = transform.localScale;
        transform.localScale = new Vector3(s.x, CantidadInicial / CantidadMaxima, s.z);
    }


	public override void Reaccionar()
	{
        reaccionando = true;
		tiempoRestante = TiempoReaccion;
	}
}
