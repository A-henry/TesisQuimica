using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{
    public float VelocidadDesplazamiento = 2f;
    public float VelocidadRotacion = 10f;

    void Start () {
		
	}
	
	void Update ()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        transform.Translate(0, 0, v * VelocidadDesplazamiento * Time.deltaTime, Space.Self);

        transform.Rotate(0, h * VelocidadRotacion * Time.deltaTime, 0);
    }
}
