using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{


    public float velocidadDespzamiento ;

    void Start () {
		
	}
	
	void Update () {
       
        if(Input.GetMouseButton(1))
        {
            float pointer_y = Input.GetAxis("Mouse X");
            float pointer_x = Input.GetAxis("Mouse Y");
            gameObject.transform.Rotate(0, pointer_y * 2, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * velocidadDespzamiento);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.forward * -velocidadDespzamiento);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * velocidadDespzamiento);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * velocidadDespzamiento);
        }
    }
}
