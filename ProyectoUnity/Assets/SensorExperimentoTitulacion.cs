using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorExperimentoTitulacion : MonoBehaviour
{
    bool dentro = false;
    bool activo = false;
    GameObject player;

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
		if(dentro && Input.GetKeyDown(KeyCode.Return))
        {
            activo = true;

            player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);

            UI.SetActive(true);
            Camara.SetActive(true);
            Gotas.enabled = true;
            Vaso.enabled = true;
            Experimento.enabled = true;
        }


        if (activo && Input.GetKeyDown(KeyCode.Escape)) {
            activo = false;

            player.SetActive(true);

            UI.SetActive(false);
            Camara.SetActive(false);
            Gotas.enabled = false;
            Vaso.enabled = false;
            Experimento.enabled = false;
        }
	}


    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            dentro = true;
        }
    }


    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            dentro = false;
        }
    }
}
