using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPapel : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Papel")
        {
            GetComponentInParent<TuboClorato>().PapelDetectado();

            Destroy(coll.gameObject);
        }
    }
}
