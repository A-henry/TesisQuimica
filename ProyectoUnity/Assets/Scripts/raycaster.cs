using UnityEngine;
using System.Collections;

public class raycaster : MonoBehaviour
{
    ExperimentoOxidacion gc;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        gc = obj.GetComponent<ExperimentoOxidacion>();
    }



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            //Debug.Log("El rayo toca con " + hit.transform.gameObject.name);
            GameObject g = hit.collider.gameObject;
            Material info = g.GetComponent<Material>();
            //Debug.Log(info.name);
            //Debug.Log(info.descripcion);
            if (Input.GetMouseButtonDown(0))
            {
                /*GameObject g = hit.collider.gameObject;
                inforObjet info = g.GetComponent<inforObjet>();
                Debug.Log("click" + info.name);
                // GameObject.*/
                gc.Seleccionar(hit.collider.gameObject);
            }
        } else {
            if (Input.GetMouseButtonDown(0)){
                gc.DeseleccionarTodos();
            } 
        }
    }
}