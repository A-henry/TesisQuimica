using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterClorato : MonoBehaviour {

    public float DistanciaAgarre = 5;
    public static Vector3 PosicionAgarre;
    public MaterialReactivo Tubo;
    public MaterialReactivo Clorato;

    ExperimentoClorato _experimento;


    void Start ()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoClorato");
        _experimento = obj.GetComponent<ExperimentoClorato>();
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
            Tubo.Desmarcar();
            Clorato.Desmarcar();
            return;
        }


        if (hit.collider.tag == "Mechero")
        {
            if (_experimento.Agarrado == null && Input.GetMouseButtonDown(0))
            {
                _experimento.SwitchMechero();
            }
        }
        else if (hit.collider.tag == "MaterialAgarrable")
        {
            if (_experimento.Agarrado == null && Input.GetMouseButtonDown(0))
            {
                _experimento.Agarrar(hit.collider.gameObject);
            }
        } 
        else if(hit.collider.tag == "MaterialReactivo")
        {
            bool mostrarReactivo = _experimento.SobreReactivo(hit.collider.GetComponent<MaterialReactivo>());
            if (mostrarReactivo) {
                hit.collider.GetComponent<MaterialReactivo>().Marcar();
            }

            if(Input.GetMouseButtonDown(0) && _experimento.Agarrado != null)
            {
                _experimento.Accion(hit.collider.gameObject);
            }
        }
    }
}
