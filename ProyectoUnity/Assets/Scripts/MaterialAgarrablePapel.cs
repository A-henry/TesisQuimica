using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgarrablePapel : MaterialAgarrable
{
    Rigidbody _body;

    protected override void Start()
    {
        base.Start();

        _body = GetComponent<Rigidbody>();
    }



    void Update()
    {
        if (_body.isKinematic)
        {
            transform.position = EstaAgarrado ? RaycasterClorato.PosicionAgarre : _posicionOriginal;
        }
    }



    public void Hechar()
    {
        (_coll as SphereCollider).radius = 0.84f;
        gameObject.layer = LayerMask.NameToLayer("Fisica");
        _body.isKinematic = false;

        Soltar();
    }
}
