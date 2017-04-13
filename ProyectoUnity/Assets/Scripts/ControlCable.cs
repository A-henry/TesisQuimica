using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCable: MonoBehaviour
{
    public Transform Conexion;

	void Start () {
	}


    void Update()
    {
        Conexion.position = Vector3.Lerp(Conexion.position, transform.position, 1f * Time.deltaTime);

        Conexion.rotation = Quaternion.Lerp(Conexion.rotation, transform.rotation, 1f * Time.deltaTime);
    }
	

}
