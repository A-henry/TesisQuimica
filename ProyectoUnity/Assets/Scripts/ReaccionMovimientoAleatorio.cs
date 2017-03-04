using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionMovimientoAleatorio : ReaccionQuimica
{
    public float Velocidad = 0.1f;
    public float RotMax = 50;
    Vector2 pos;

	// Use this for initialization
	void Start () {
        pos = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (reaccionando)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;
            
            pos = pos + new Vector2(1, 1).normalized * Velocidad;

            float p = Mathf.PerlinNoise(pos.x, pos.y);

            transform.rotation = Quaternion.Euler(0, p * RotMax, p * RotMax);

            if (tiempoRestante <= 0)
            {
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
