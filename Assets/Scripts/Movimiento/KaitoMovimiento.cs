using UnityEngine;
using UnityEngine.InputSystem;

public class KaitoMovimiento : MonoBehaviour
{
    public float velocidadCaminata = 2f;
    public float velocidadCarrera = 5f;
    public float velocidadRotacion = 5f;


    private Animator animador;
    private Rigidbody rb;
    private Vector3 movimiento;
    private bool estaCaminando = false;
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
        rb = GetComponent<Rigidbody>();

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
        if(Physics.SphereCast(transform.position, 1, transform.forward, out RaycastHit hitData, 1f))
        {
            if (hitData.collider.CompareTag("Interactable"))
            {
                hitData.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    void Update()
    {
        bool presionandoW = movimiento.z > 0;
        bool presionandoS = movimiento.z < 0;
        bool shiftPresionado = Input.GetKey(KeyCode.LeftShift);

        // ROTACIÓN (siempre puede girar)
        if (movimiento.x > 0)
        {
            rb.angularVelocity = Vector3.up * velocidadRotacion;
            animador.SetBool("girandoDerecha", true);
            animador.SetBool("girandoIzquierda", false);
        }
        else if (movimiento.x < 0)
        {
            rb.angularVelocity = Vector3.up * -velocidadRotacion;
            animador.SetBool("girandoIzquierda", true);
            animador.SetBool("girandoDerecha", false);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            animador.SetBool("girandoIzquierda", false);
            animador.SetBool("girandoDerecha", false);
        }

        // MOVIMIENTO HACIA ADELANTE
        if (presionandoW)
        {
            // Si Shift aún no está permitido, caminamos
            if (!puedeCorrer)
            {
                rb.velocity = transform.forward * velocidadCaminata;
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
                rb.velocity = transform.forward * velocidadCarrera;
                animador.SetBool("estaCaminando", false);
                animador.SetBool("estaCorriendo", true);
                animador.SetBool("estaRetrocediendo", false);
            }
            else
            {
                // Si suelta Shift pero sigue caminando
                rb.velocity = transform.forward * velocidadCaminata;
                animador.SetBool("estaCaminando", true);
                animador.SetBool("estaCorriendo", false);
                animador.SetBool("estaRetrocediendo", false);
            }
        }
        // MOVIMIENTO HACIA ATRÁS
        else if (presionandoS)
        {
            rb.velocity = -transform.forward * velocidadCarrera / 2;
            animador.SetBool("estaCaminando", false);
            animador.SetBool("estaCorriendo", false);
            animador.SetBool("estaRetrocediendo", true);
            puedeCorrer = false;
        }
        else
        {
            // IDLE
            rb.velocity = Vector3.zero;
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
