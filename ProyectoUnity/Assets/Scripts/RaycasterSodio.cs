using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterSodio : MonoBehaviour
{
    public float DistanciaAgarre = 5;
    public static Vector3 PosicionAgarre;
    public VasoAgua Agua;

    ExperimentoSodio _experimento;
    


    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoSodio");
        _experimento = obj.GetComponent<ExperimentoSodio>();
    }



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PosicionAgarre = Camera.main.transform.position + ray.direction * DistanciaAgarre;

        if (_experimento.Agarrado && Input.GetMouseButtonDown(1))
        {
            _experimento.Soltar();
            return;
        }

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Mouse")))
        {
            Agua.Desmarcar();
            return;
        }


        if (hit.collider.tag == "MaterialAgarrable")
        {
            if (_experimento.Agarrado == null && Input.GetMouseButtonDown(0))
            {
                _experimento.Agarrar(hit.collider.gameObject);
            }
        }
        else if (hit.collider.tag == "MaterialReactivo")
        {
            hit.collider.GetComponent<MaterialReactivo>().Marcar();

            if (Input.GetMouseButtonDown(0) && _experimento.Agarrado != null)
            {
                _experimento.Accion(hit.collider.gameObject);
            }
        }
    }
}
