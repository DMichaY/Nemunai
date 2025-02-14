using UnityEngine;

public class MovimientoControlado : MonoBehaviour
{
    public float velocidadAdelante = 5f;  
    public float velocidadAtras = 3f;     
    public float velocidadRotacion = 100f;  
    public float velocidadSprint = 10f;    

    private float velocidadActual;         
    private AnimacionesControlador animControlador;  

    void Start()
    {
        animControlador = GetComponentInChildren<AnimacionesControlador>();
    }

    void Update()
    {
        velocidadActual = velocidadAdelante;  

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);

        if (isSprinting)
        {
            velocidadActual = velocidadSprint;
            animControlador.SetIsSprinting(true);
            animControlador.SetIsWalking(false);
            Debug.Log("Sprint Activado!");  // <-- Esto debe aparecer en la consola
        }
        else
        {
            animControlador.SetIsSprinting(false);
        }

        // Movimiento y animaciones
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.back * velocidadActual * Time.deltaTime);

            if (isSprinting)
            {
                animControlador.SetIsSprinting(true);
                animControlador.SetIsWalking(false);
            }
            else
            {
                animControlador.SetIsSprinting(false);
                animControlador.SetIsWalking(true);
            }

            animControlador.SetIsBackwards(false);

            // Desactivar animaciones de Left y Right mientras se mueve hacia adelante
            animControlador.SetIsLeft(false);
            animControlador.SetIsRight(false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * velocidadAtras * Time.deltaTime);
            animControlador.SetIsWalking(false);
            animControlador.SetIsSprinting(false);
            animControlador.SetIsBackwards(true);

            // Desactivar animaciones de Left y Right mientras se mueve hacia atrás
            animControlador.SetIsLeft(false);
            animControlador.SetIsRight(false);
        }
        else
        {
            animControlador.SetIsWalking(false);
            animControlador.SetIsSprinting(false);
            animControlador.SetIsBackwards(false);

            // Desactivar animaciones de Left y Right cuando no se mueve
            animControlador.SetIsLeft(false);
            animControlador.SetIsRight(false);
        }

        // Rotaciones
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -velocidadRotacion * Time.deltaTime);
            animControlador.SetIsLeft(true);
            animControlador.SetIsRight(false);

            // Asegurarnos de que la animación Left solo se active si A está siendo presionado
            animControlador.SetIsWalking(false); // Desactivar Walking mientras se presiona A
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
            animControlador.SetIsLeft(false);
            animControlador.SetIsRight(true);

            // Asegurarnos de que la animación Right solo se active si D está siendo presionado
            animControlador.SetIsWalking(false); // Desactivar Walking mientras se presiona D
        }
        else
        {
            // Si no se presiona A ni D, desactivar las animaciones de Left y Right
            animControlador.SetIsLeft(false);
            animControlador.SetIsRight(false);
        }
    }
}
