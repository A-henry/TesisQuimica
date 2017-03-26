using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SensorExperimentoZinc : MonoBehaviour {

    bool dentro = false;
    bool activo = false;

    GameObject player;

    public GameObject UI;
    public GameObject Camara;
    public ExperimentoOxidacion game;


    void Start ()
    {
        UI.SetActive(false);
        Camara.SetActive(false);
        game.enabled = false;
    }

    void Update()
    {
        if (dentro && Input.GetKeyDown(KeyCode.Return))
        {
            activo = true;

            player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);

            UI.SetActive(true);
            Camara.SetActive(true);
            game.enabled = true;
        }

        if (activo && Input.GetKeyDown(KeyCode.Escape))
        {
            activo = false;

            player.SetActive(true);

            UI.SetActive(false);
            Camara.SetActive(false);
            game.Soltar();
            game.enabled = false;
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
