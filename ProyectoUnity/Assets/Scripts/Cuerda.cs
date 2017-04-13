using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public bool Padre;
    public Transform ConectadoA;

    CharacterJoint _joint;

    void Awake()
    {
        if(Padre==false)
            _joint = GetComponent<CharacterJoint>();
    }

    void Start()
    {
        if (Padre == false)
        {
            _joint.connectedBody = transform.parent.GetComponent<Rigidbody>();
            transform.parent = null;
        }

    }

    void Update()
    {
        if (Padre && ConectadoA != null)
            transform.position = ConectadoA.position;
    }
}
