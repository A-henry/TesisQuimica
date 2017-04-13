using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterDensidad : MonoBehaviour {

    /* public float DistanciaAgarre = 5;
     public static Vector3 PosicionAgarre;
     public VasodeAguaDensidad Agua;

     ExperimentoDensidad _experimento;



     void Start()
     {
         GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDensidad");
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
             if(_experimento.Agarrado != null)
             {
                 hit.collider.GetComponent<MaterialReactivo>().Marcar();
             }
             if (Input.GetMouseButtonDown(0) && _experimento.Agarrado != null)
             {
                 _experimento.Accion(hit.collider.gameObject);
             }

         }
     }*/










    public float DistanciaAgarre = 5;

    public static Vector3 PosicionAgarre;

    ExperimentoDensidad gc;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDensidad");
        gc = obj.GetComponent<ExperimentoDensidad>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PosicionAgarre = Camera.main.transform.position + ray.direction * DistanciaAgarre;

        RaycastHit[] hits = Physics.RaycastAll(ray);

        bool encontrado = false;

        if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Agarrado && Input.GetMouseButtonDown(1))
        {
            gc.Soltar();
            return;
        }

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            GameObject g = hit.collider.gameObject;

            if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Nada)
            {
                if (hit.collider.tag == "MaterialAgarrable")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        MaterialAgarrableEsfera m = hit.collider.GetComponent<MaterialAgarrableEsfera>();
                        gc.Agarrar(m);
                    }
                }
            }
            else if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Agarrado)
            {
                gc.DesmarcarTodosReactivos();

                if (Input.GetMouseButtonDown(1))
                {
                    gc.Soltar();
                }

                if (hit.collider.tag == "MaterialReactivo")
                {
                    MaterialReactivo react = hit.collider.GetComponent<MaterialReactivo>();
                    if (gc.MarcarReactivo(react))
                    {
                        encontrado = true;
                    }
                }
            }
            else if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.MarcandoReactivo)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MaterialAgarrableEsfera esfera = gc.Agarrado.GetComponent<MaterialAgarrableEsfera>();
                    VasodeAguaDensidad agua = hit.collider.GetComponent<VasodeAguaDensidad>();
                    gc.Accion(hit.collider.gameObject);
                    esfera.TransformConexion = agua.PosicionAgua();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    gc.Soltar();
                }

                if (hit.collider.tag == "MaterialReactivo")
                {
                    encontrado = true;
                }
                 
            }else if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.Reaccionando)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    MaterialAgarrableEsfera esfera = gc.Agarrado.GetComponent<MaterialAgarrableEsfera>();
                    VasodeAguaDensidad agua = hit.collider.GetComponent<VasodeAguaDensidad>();
                    gc.DisminuirLiquido(hit.collider.gameObject);
                    gc.Agarrar(esfera);

                }
               
            }

        } // for


        if (gc.EstadoSeleccion == ExperimentoDensidad.EnumSeleccion.MarcandoReactivo)
        {
            if (encontrado == false)
            {
                gc.VolverEstadoAgarrado();
            }
        }
    }
}
