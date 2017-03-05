using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorGotas : MonoBehaviour
{
    public GameObject PrototipoGota;
    [Range(0.1f, 10f)]
    public float Frecuencia = 2;
    [Range(0.01f, 1)]
    public float CantidadPorGota = 0.2f;

    public Transform LugarGeneracionGotas;

	void Start ()
    {
        StartCoroutine(GenerarGotas());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GenerarGotas()
    {
        while(true)
        {
            GameObject go = Instantiate(PrototipoGota, LugarGeneracionGotas.position, LugarGeneracionGotas.rotation);
            go.GetComponent<Gota>().Cantidad = CantidadPorGota;

            yield return new WaitForSeconds(Frecuencia);
        }
    }
}
