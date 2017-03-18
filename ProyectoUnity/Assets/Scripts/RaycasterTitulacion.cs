using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterTitulacion : MonoBehaviour
{
    SensorVasoPrecipitado sensor;
    bool estaSeleccionado;

	void Start ()
    {
        sensor = null;
        estaSeleccionado = false;
	}


	
	void Update ()
    {
        if (!estaSeleccionado) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << LayerMask.NameToLayer("Mouse"));

            if(hits.Length > 0) {
                RaycastHit hit = hits[0];
                GameObject g = hit.collider.gameObject;
                SensorVasoPrecipitado s = g.GetComponent<SensorVasoPrecipitado>();

                if(sensor == null) {
                    sensor = s;
                    sensor.MouseSobre();
                } else if(sensor != s) {
                    sensor.MouseSaliendo();
                    s.MouseSobre();

                    sensor = s;
                }
            } else if(sensor != null) {
                sensor.MouseSaliendo();
                sensor = null;
            }

            if (Input.GetMouseButtonDown(0) && sensor != null)
            {
                sensor.Seleccionar();
                estaSeleccionado = true;
            }
        } else {  // Con seleccion
            if (Input.GetMouseButtonDown(1))
            {
                estaSeleccionado = false;
                sensor.Deseleccionar();

                sensor = null;
            }
        }
    }
}
