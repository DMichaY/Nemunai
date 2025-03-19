using UnityEngine;

public class KaitoMovimiento : MonoBehaviour
{
    public float velocidadCaminata = 0f;
    public float velocidadCarrera = 0f;
    public float velocidadRotacion = 0f;
    private Animator animador;
    private bool puedeCorrer = false;

    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        float movimientoVertical = Input.GetAxis("Vertical");
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        bool corriendo = Input.GetKey(KeyCode.LeftShift) && puedeCorrer;

        // Movimiento hacia adelante y atrás
        if (movimientoVertical > 0)
        {
            transform.Translate(Vector3.forward * (corriendo ? velocidadCarrera : velocidadCaminata) * Time.deltaTime);
            animador.SetBool("estaCaminando", !corriendo);
            animador.SetBool("estaCorriendo", corriendo);
            animador.SetBool("estaRetrocediendo", false);
            puedeCorrer = true;
        }
        else if (movimientoVertical < 0)
        {
            transform.Translate(Vector3.back * velocidadCaminata * Time.deltaTime);
            animador.SetBool("estaCaminando", false);
            animador.SetBool("estaCorriendo", false);
            animador.SetBool("estaRetrocediendo", true);
            puedeCorrer = false;
        }
        else
        {
            animador.SetBool("estaCaminando", false);
            animador.SetBool("estaCorriendo", false);
            animador.SetBool("estaRetrocediendo", false);
            puedeCorrer = false;
        }

        // Rotación izquierda y derecha
        if (movimientoHorizontal > 0)
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            animador.SetBool("girandoDerecha", true);
            animador.SetBool("girandoIzquierda", false);
        }
        else if (movimientoHorizontal < 0)
        {
            transform.Rotate(Vector3.up * -velocidadRotacion * Time.deltaTime);
            animador.SetBool("girandoIzquierda", true);
            animador.SetBool("girandoDerecha", false);
        }
        else
        {
            animador.SetBool("girandoIzquierda", false);
            animador.SetBool("girandoDerecha", false);
        }
    }
}