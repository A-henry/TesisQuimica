using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExperimentoOxidacion : MonoBehaviour
{
    public static float VOLUMEN_MINIMO_ACIDO = 10f;
    public static float VOLUMEN_MAXIMO_ACIDO = 150f;

    public enum EnumSeleccion { Nada, Agarrado, MarcandoReactivo, Reaccionando };

    [Range(1, 10)]
    public int EscalaDeTiempo = 1;
    public List<GameObject> objetos;
    public List<MaterialReactivo> Reactivos;
    public GameObject Seleccionado;
    public GameObject Agarrado;
    public MaterialReactivo ReactivoPosible;
    public MaterialReactivo ReactivoEnReaccion;
    public EnumSeleccion EstadoSeleccion;


    ReactivoAcidoClorhidrico acido;
    UIPanelMaterial uiPanelMaterial;
	UIExperimentos uiExperimentos;


	float tiempoReaccionTranscurrido;

    void Awake()
    {
        GameObject ui = GameObject.FindGameObjectWithTag("UIPanelMaterial");

        if (ui != null)
        {
            uiPanelMaterial = ui.GetComponent<UIPanelMaterial>();
            ui.SetActive(false);
        }


        ui = GameObject.FindGameObjectWithTag("UIExperimentos");
        if (ui != null)
        {
            uiExperimentos = ui.GetComponent<UIExperimentos>();
            ui.SetActive(true);
        }


        EstadoSeleccion = EnumSeleccion.Nada;

        // Materiales normales
        objetos = new List<GameObject>();
        GameObject [] materiales = GameObject.FindGameObjectsWithTag("Material");
        for(int i=0;i<materiales.Length;i++) {
            objetos.Add(materiales[i]);
        }

        // Materiales Reactivos
        GameObject[] rs = GameObject.FindGameObjectsWithTag("MaterialReactivo");
        Reactivos = new List<MaterialReactivo>();
        for (int i = 0; i < rs.Length; i++)
        {
            MaterialReactivo m = rs[i].GetComponent<MaterialReactivo>();
            Reactivos.Add(m);

            ReactivoAcidoClorhidrico ac = (ReactivoAcidoClorhidrico)m;
            if(ac != null)
            {
                acido = ac;
            }
        }
	}
	


	void Update () 
    {
        Time.timeScale = EscalaDeTiempo;

        if (EstadoSeleccion == EnumSeleccion.Reaccionando) 
		{
			uiExperimentos.ActualizarDatosReactivo(ReactivoEnReaccion, tiempoReaccionTranscurrido);

			tiempoReaccionTranscurrido = tiempoReaccionTranscurrido + Time.deltaTime;
		}
    }


    public float CambiarVolumenInicialAcido(float volumenRelativo)
    {
        float vol = Mathf.Lerp(VOLUMEN_MINIMO_ACIDO, VOLUMEN_MAXIMO_ACIDO, volumenRelativo);

        acido.CambiarCantidadInicial(vol); 

        return vol;
    }



    public void Seleccionar(GameObject obj)
    {
        DeseleccionarTodos();

        Seleccionado = obj;
        Material m = obj.GetComponent<Material>();
        m.seleccionado = true;

        uiPanelMaterial.gameObject.SetActive(true);
        uiPanelMaterial.ActualizarMaterial(m);
    }

    public void DeseleccionarTodos()
    {
        uiPanelMaterial.gameObject.SetActive(false);

        for(int i=0;i<objetos.Count;i++) {
            Material m = objetos[i].GetComponent<Material>();
            m.seleccionado = false;
        }

        Seleccionado = null;
    }


    public void Agarrar(MaterialAgarrableZinc m) {

        Agarrado = m.gameObject;
        EstadoSeleccion = EnumSeleccion.Agarrado;
        m.Agarrar();

		uiExperimentos.MostrarInformacion (m);
    }


    public void Soltar()
    {
        if (Agarrado == null)
            return;

        Agarrado.GetComponent<MaterialAgarrableZinc>().Soltar();
        Agarrado = null;

        if (EstadoSeleccion == EnumSeleccion.MarcandoReactivo)
        {
            ReactivoPosible.Desmarcar();
            ReactivoPosible = null;
        }

        EstadoSeleccion = EnumSeleccion.Nada;

		uiExperimentos.MostrarAgarrable(false);
    }


    public void DesmarcarTodosReactivos()
    {
        for(int i = 0; i < Reactivos.Count; i++)
        {
            Reactivos[i].Desmarcar();
        }

        ReactivoPosible = null;

		uiExperimentos.MostrarReactivo (false);
    }


    public bool MarcarReactivo(MaterialReactivo reactivo)
    {
		if (reactivo.Usado == false) {
			ReactivoPosible = reactivo;
			EstadoSeleccion = EnumSeleccion.MarcandoReactivo;
			reactivo.Marcar ();

			uiExperimentos.MostrarInformacionReactivo (reactivo);

			return true;
		}

		return false;
    }

    public void VolverEstadoAgarrado()
    {
        EstadoSeleccion = EnumSeleccion.Agarrado;
        ReactivoPosible.Desmarcar();
        ReactivoPosible = null;
    }


	public void ReaccionQuimica()
	{
		EstadoSeleccion = EnumSeleccion.Reaccionando;

		MaterialReactivo reactivo = ReactivoPosible;
		reactivo.ReaccionQuimica ();
		Reactivos.Remove (reactivo);

		StartCoroutine (CorutinaReaccionQuimica (ReactivoPosible.TiempoReaccion));

        ReactivoEnReaccion = ReactivoPosible;
        ReactivoPosible = null;

		Destroy (Agarrado);
		Agarrado = null;



		tiempoReaccionTranscurrido = 0f;
	}


	// Esta es una corutina
	IEnumerator CorutinaReaccionQuimica(float tiempoReaccion)
	{
		yield return new WaitForSeconds (tiempoReaccion);

		EstadoSeleccion = EnumSeleccion.Nada;
        ReactivoEnReaccion = null;
	}
}
