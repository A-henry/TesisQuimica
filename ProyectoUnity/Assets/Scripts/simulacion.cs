using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simulacion : ReaccionQuimica {

	float A1 = 1.082857143f;
	float B1 = 0.06696428571f;
	float A2 = 2.59f;
	float B2 = 0.018925f;
	public float y1;
	float x1;
	float x2;
	public Transform tam;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 s = tam.localScale;
		//Debug.Log ("s.y"+s);
		if (reaccionando) {
			y1 = s.y * 100;
			x1 = (y1 - A1) * B1;
			x2 = (y1 - A2) * B2;
			Debug.Log (x1+" "+x2);



		}
		
	}
	public override void Reaccionar()
	{
		reaccionando = true;
	}
}
