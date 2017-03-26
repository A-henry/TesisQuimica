using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorExperimentoDencidad : MonoBehaviour
{

    bool dentro = false;
    bool activo = false;

    GameObject player;

    public GameObject Camara;



    void Start()
    {
        Camara.SetActive(false);
    }

    void Update()
    {
        if (dentro && Input.GetKeyDown(KeyCode.Return))
        {
            activo = true;

            player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);

            Camara.SetActive(true);
        }

        if (activo && Input.GetKeyDown(KeyCode.Escape))
        {
            activo = false;

            player.SetActive(true);

            Camara.SetActive(false);
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
