using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorExperimentoTitulacion : MonoBehaviour
{
    bool _dentro = false;
    public GameObject UI;
    public GameObject Camara;
    public GeneradorGotas Gotas;
    public VasoPrecipitado Vaso;
    public ExperimentoTitulacion Experimento;


	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(_dentro && Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);

            UI.SetActive(true);
            Camara.SetActive(true);
            Gotas.enabled = true;
            Vaso.enabled = true;
            Experimento.enabled = true;
        }
	}


    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            _dentro = true;
        }
    }


    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            _dentro = false;
        }
    }
}
