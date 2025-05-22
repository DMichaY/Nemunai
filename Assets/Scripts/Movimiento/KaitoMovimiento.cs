using UnityEngine;
using UnityEngine.InputSystem;

public class KaitoMovimiento : MonoBehaviour
{
    public float velocidadCaminata = 2f;
    public float velocidadCarrera = 5f;
    public float velocidadRotacion = 5f;


    private Animator animador;
    public Vector3 movimiento;
    private bool puedeCorrer = false;

    public AudioClip andarTierra;
    public AudioClip andarPiedra;
    public AudioClip andarMadera;
    public AudioClip correrTierra;
    public AudioClip correrPiedra;
    public AudioClip correrMadera;
    public AudioSource audioFuente;
    
    private string sueloActualTag = null;
    private bool enContactoConSuelo = false;

    void Start()
    {
        animador = GetComponent<Animator>();

        // IMPORTANTE: Validamos que haya AudioSource antes de acceder a él
        if (audioFuente != null)
            audioFuente.loop = true;
    }

    //Recibir movimiento por Input System
    public void OnMovimiento(InputValue value)
    {
        movimiento = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }

    //Interacción
    public void OnInteractuar()
    {
        if(Physics.SphereCast(transform.position - transform.forward, 1, transform.forward, out RaycastHit hitData, 1f))
        {
            if (hitData.collider.CompareTag("Interactable"))
            {
                hitData.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    void Update()
    {

        Debug.DrawRay(this.transform.position, this.transform.forward * 15, Color.red);

        bool presionandoW = movimiento.z > 0;
        bool presionandoS = movimiento.z < 0;
        bool shiftPresionado = Input.GetKey(KeyCode.LeftShift);

        // ROTACIÓN (siempre puede girar)
        if (movimiento.x > 0)
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            animador.SetBool("girandoDerecha", true);
            animador.SetBool("girandoIzquierda", false);
        }
        else if (movimiento.x < 0)
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

        // MOVIMIENTO HACIA ADELANTE
        if (presionandoW)
        {
            // Si Shift aún no está permitido, caminamos
            if (!puedeCorrer)
            {
                transform.Translate(Vector3.forward * velocidadCaminata * Time.deltaTime);
                animador.SetBool("estaCaminando", true);
                animador.SetBool("estaCorriendo", false);
                animador.SetBool("estaRetrocediendo", false);

                // Ahora sí, después de caminar al menos un frame, habilitamos correr
                if (!shiftPresionado)
                {
                    puedeCorrer = true;
                }
            }
            // Si ya caminó y luego presiona Shift
            else if (shiftPresionado)
            {
                transform.Translate(Vector3.forward * velocidadCarrera * Time.deltaTime);
                animador.SetBool("estaCaminando", false);
                animador.SetBool("estaCorriendo", true);
                animador.SetBool("estaRetrocediendo", false);
            }
            else
            {
                // Si suelta Shift pero sigue caminando
                transform.Translate(Vector3.forward * velocidadCaminata * Time.deltaTime);
                animador.SetBool("estaCaminando", true);
                animador.SetBool("estaCorriendo", false);
                animador.SetBool("estaRetrocediendo", false);
            }
        }
        // MOVIMIENTO HACIA ATRÁS
        else if (presionandoS)
        {
            transform.Translate(Vector3.back * velocidadCaminata * Time.deltaTime);
            animador.SetBool("estaCaminando", false);
            animador.SetBool("estaCorriendo", false);
            animador.SetBool("estaRetrocediendo", true);
            puedeCorrer = false;
        }
        else
        {
            // IDLE
            animador.SetBool("estaCaminando", false);
            animador.SetBool("estaCorriendo", false);
            animador.SetBool("estaRetrocediendo", false);
            puedeCorrer = false;
        }

        SonidosPisadas(presionandoW || presionandoS);
    }

    // LÓGICA AUDIO PISADAS
    void SonidosPisadas(bool enMovimiento)
    {
        // Si no está en contacto con el suelo o no hay audioFuente, detenemos cualquier sonido
        if (!enContactoConSuelo || audioFuente == null)
        {
            if (audioFuente != null)
                audioFuente.Stop();
            return;
        }

        AudioClip clipActual = null;

        if (sueloActualTag == "Tierra")
        {
            clipActual = animador.GetBool("estaCorriendo") ? correrTierra : andarTierra;
        }
        else if (sueloActualTag == "Piedra")
        {
            clipActual = animador.GetBool("estaCorriendo") ? correrPiedra : andarPiedra;
        }
        else if (sueloActualTag == "Madera")
        {
            clipActual = animador.GetBool("estaCorriendo") ? correrMadera : andarMadera;
        }

        if (enMovimiento)
        {
            if (audioFuente.clip != clipActual)
            {
                audioFuente.clip = clipActual;
                audioFuente.Play();
            }
            else if (!audioFuente.isPlaying)
            {
                audioFuente.Play();
            }
        }
        else
        {
            audioFuente.Stop();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Tierra") || collision.collider.CompareTag("Piedra") || collision.collider.CompareTag("Madera"))
        {
            sueloActualTag = collision.collider.tag;
            enContactoConSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == sueloActualTag)
        {
            sueloActualTag = null;
            enContactoConSuelo = false;
        }
    }
}
