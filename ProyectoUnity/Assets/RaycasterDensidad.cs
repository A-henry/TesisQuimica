using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterDensidad : MonoBehaviour {

    public float DistanciaAgarre = 5;
    public static Vector3 PosicionAgarre;

    ExperimentoDensidad _experimento;


    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDencidad");
        _experimento = obj.GetComponent<ExperimentoDensidad>();
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
        if (!Physics.Raycast(ray, out hit))
        {
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
            bool mostrarReactivo = _experimento.SobreReactivo(hit.collider.GetComponent<MaterialReactivo>());
            if (mostrarReactivo)
            {
                hit.collider.GetComponent<MaterialReactivo>().Marcar();
            }


        }
    }
}
