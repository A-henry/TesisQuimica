using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorConexion : MonoBehaviour
{
    string _tipoCable = "";

    void OnTriggerEnter(Collider otro)
    {
        if(otro.tag == "MaterialAgarrable")
        {
            if (otro.transform.parent.name == "Cable_A")
                _tipoCable = "A";

            if (otro.transform.parent.name == "Cable_B")
                _tipoCable = "B";


            GetComponentInParent<ConductorLiquido>().ConexionFisica(_tipoCable, true);
        }
    }


    void OnTriggerExit(Collider otro)
    {
        if (otro.tag == "MaterialAgarrable")
        {
            if (otro.transform.parent.name == "Cable_A")
                _tipoCable = "A";

            if (otro.transform.parent.name == "Cable_B")
                _tipoCable = "B";


            GetComponentInParent<ConductorLiquido>().ConexionFisica(_tipoCable, false);
        }
    }


}
