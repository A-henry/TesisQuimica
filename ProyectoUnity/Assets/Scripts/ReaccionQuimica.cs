using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReaccionQuimica : MonoBehaviour 
{
	public float TiempoReaccion = 3f;

	protected float tiempoRestante;
	protected bool reaccionando;


	void Start ()
	{
		reaccionando = false;
	}
	
	void Update () 
	{
	}


	public abstract void Reaccionar ();
}
