using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterManipulacion : MonoBehaviour
{
    public float DistanciaAgarre = 5;

    public static Vector3 PosicionAgarre;

    ExperimentoOxidacion gc;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoOxidacion");
        gc = obj.GetComponent<ExperimentoOxidacion>();
    }

    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PosicionAgarre = Camera.main.transform.position + ray.direction * DistanciaAgarre;

        RaycastHit[] hits = Physics.RaycastAll(ray);

        bool encontrado = false;

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            GameObject g = hit.collider.gameObject;

            if (gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.Nada)
            {
                if (hit.collider.tag == "MaterialAgarrable")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        MaterialAgarrableZinc m = hit.collider.GetComponent<MaterialAgarrableZinc>();
                        gc.Agarrar(m);
                    }
                }
            }
            else if (gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.Agarrado)
            {
                gc.DesmarcarTodosReactivos();

                if (Input.GetMouseButtonDown(1))
                {
                    gc.Soltar();
                }

                if (hit.collider.tag == "MaterialReactivo")
                {
                    MaterialReactivo react = hit.collider.GetComponent<MaterialReactivo>();
					if (gc.MarcarReactivo (react)) {
						encontrado = true;
					}
                }
            } else if(gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.MarcandoReactivo)
            {
				if (Input.GetMouseButtonDown (0)) {
					gc.ReaccionQuimica ();
				}
                if (Input.GetMouseButtonDown(1))
                {
                    gc.Soltar();
                }

                if (hit.collider.tag == "MaterialReactivo")
                {
                    encontrado = true;
                }
            }
        } // for


        if(gc.EstadoSeleccion == ExperimentoOxidacion.EnumSeleccion.MarcandoReactivo) {
            if(encontrado == false)
            {
                gc.VolverEstadoAgarrado();
            }
        }
    }
}
