using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionCambioTamanho : ReaccionQuimica 
{
	public float EscalaMaxY = 1f;  // escala de unity
	public float CantidadMaxima = 2.4f;  // gr

	public float CantidadInicial = 2.4f;//ml
	public float CantidadFinal = 0f; // ml

	float gr2Esc;
	float perdidaEscPorSegundo;


	void Start()
	{
		gr2Esc = EscalaMaxY / CantidadMaxima;

		perdidaEscPorSegundo = ((CantidadInicial - CantidadFinal) / TiempoReaccion) * gr2Esc;

		transform.localScale = new Vector3 (CantidadInicial * gr2Esc, CantidadInicial * gr2Esc , CantidadInicial * gr2Esc);
	}


	void Update () 
	{
		Vector3 s = transform.localScale;

		if (reaccionando) {
			tiempoRestante = tiempoRestante - Time.deltaTime;

			float gr = CantidadFinal + Mathf.Abs(CantidadFinal - CantidadInicial) * (tiempoRestante / TiempoReaccion);

			float e = (gr * gr2Esc);

			transform.localScale = new Vector3 (e, e, e);

			if (tiempoRestante <= 0) {
				reaccionando = false;
			}

			e = Mathf.Max (0, e);

			if (e == 0) {
				Destroy (gameObject);
			}
		}
	}


	public override void Reaccionar()
	{
		reaccionando = true;
		tiempoRestante = TiempoReaccion;
	}
}
