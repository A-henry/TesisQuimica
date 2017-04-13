using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDensidad : MonoBehaviour {

    ExperimentoDensidad _experimento;


    static UIDensidad _instancia;
    public static UIDensidad Instancia
    {
        get { return _instancia; }
    }

    public Slider SliderCantidadEsfera;
    public Text TextoCandidadSodio;

    public float CantidadEsfera
    {
        set
        {
            float cantidadReal = _experimento.CambiarCantidadEsferaRel(value);
            TextoCandidadSodio.text = string.Format("{0:00.00} gr.", cantidadReal);
        }
    }

    void Awake()
    {
        _instancia = this;
    }

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ExperimentoDensidad");
        _experimento = obj.GetComponent<ExperimentoDensidad>();
        CantidadEsfera = SliderCantidadEsfera.value;

    }



    public void ReaccionQuimica()
    {
        SliderCantidadEsfera.gameObject.SetActive(false);
    }



    public void ActualizarCantidadSodio(float cantidad)
    {
        TextoCandidadSodio.text = string.Format("{0:00.00} gr.", cantidad);
    }

}
