using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionCambioColor : ReaccionQuimica
{
	public Renderer Rend;
	public Color ColorReaccion = Color.blue;
	Color colorOriginal;

	void Start () 
	{
		colorOriginal = Rend.material.color;
	}



	void Update ()
	{
		if (reaccionando) {
			tiempoRestante = tiempoRestante - Time.deltaTime;

			float p = tiempoRestante / TiempoReaccion; // valor desde 1 a 0

			Rend.material.color = Color.Lerp (ColorReaccion, colorOriginal, p);

			if (tiempoRestante < 0) {
				reaccionando = false;
			}
		}
	}




	public override void Reaccionar()
	{
		reaccionando = true;
		tiempoRestante = TiempoReaccion;
	}
}
