using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorExperimentoClorato : MonoBehaviour
{
    public bool dentro = false;
    public bool activo = false;
    GameObject player;

    public GameObject UI;
    public GameObject Camara;
    public ExperimentoClorato Experimento;
    public TuboClorato Tubo;


    void Update()
    {
        if (dentro && Input.GetKeyDown(KeyCode.Return))
        {
            activo = true;

            player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);

            UI.SetActive(true);
            Camara.SetActive(true);
            Experimento.enabled = true;
            Tubo.enabled = true;
        }


        if (activo && Input.GetKeyDown(KeyCode.Escape))
        {
            activo = false;

            player.SetActive(true);

            UI.SetActive(false);
            Camara.SetActive(false);
            Experimento.enabled = false;
            Tubo.enabled = false;
        }
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
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
