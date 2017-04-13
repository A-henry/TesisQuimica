using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterConductividad : MonoBehaviour
{

    public float DistanciaAgarre = 3;
    public static Vector3 PosicionAgarre;


    ExperimentoConductividad _experimento;

    void Awake()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ExperimentoConductividad");
        _experimento = go.GetComponent<ExperimentoConductividad>();
    }

    void Start()
    {
        
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
            GameObject[] objs = GameObject.FindGameObjectsWithTag("MaterialReactivo");
            for (int i = 0; i < objs.Length; i++)
            {
                ConductorLiquido c = objs[i].GetComponent<ConductorLiquido>();
                if (c != null)
                {
                    c.Desmarcar();
                }
            }

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
            if(_experimento.Agarrado != null)
            {
                MaterialAgarrableCable cable = _experimento.Agarrado.GetComponent<MaterialAgarrableCable>();

                string tipoCable = cable.Tipo;

                ConductorLiquido conductor = hit.collider.GetComponent<ConductorLiquido>();
                conductor.MarcarTipo(tipoCable);

                if (Input.GetMouseButtonDown(0))
                {
                    _experimento.Soltar();
                    cable.TransformConexion = conductor.Conexion(tipoCable);
                    cable.EstaConectado = true;

                    UIConductividad.Instancia.EstadoCable(cable.Tipo, conductor);
                }

            }





        }

    }
}
