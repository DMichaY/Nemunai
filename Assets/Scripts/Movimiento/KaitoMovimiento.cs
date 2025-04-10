using UnityEngine;

public class KaitoMovimiento : MonoBehaviour
{
    public float velocidadCaminata = 2f;
    public float velocidadCarrera = 5f;
    public float velocidadRotacion = 100f;

    private Animator animador;
    private bool estaCaminando = false;
    private bool puedeCorrer = false;

    public AudioClip andarTierra;
    public AudioClip andarPiedra;
    public AudioClip correrTierra;
    public AudioClip correrPiedra;
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

    void Update()
    {
        float movimientoVertical = Input.GetAxisRaw("Vertical");
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        bool presionandoW = movimientoVertical > 0;
        bool presionandoS = movimientoVertical < 0;
        bool shiftPresionado = Input.GetKey(KeyCode.LeftShift);

        // ROTACIÓN (siempre puede girar)
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
        if (collision.collider.CompareTag("Tierra") || collision.collider.CompareTag("Piedra"))
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
