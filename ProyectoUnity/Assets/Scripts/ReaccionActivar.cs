using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaccionActivar : ReaccionQuimica 
{
	public GameObject Activable;


	void Start () {
		Activable.SetActive (false);
	}

    void Update()
    {
        if (reaccionando)
        {
            tiempoRestante = tiempoRestante - Time.deltaTime;

            if (tiempoRestante < 0)
            {
                reaccionando = false;
                Activable.SetActive(false);
            }
        }

    }


	public override void Reaccionar()
	{
		Activable.SetActive (true);

        reaccionando = true;
        tiempoRestante = TiempoReaccion;
    }
}
