using UnityEngine;

public class KaitoSurvival : MonoBehaviour
{
    public float walkSpeed = 0f;
    public float runSpeed = 0f;
    public float rotationSpeed = 0f;

    private Animator animator;
    private Transform kaitoTransform;
    private bool isInteracting = false;

    void Start()
    {
        kaitoTransform = transform.Find("Kaito"); // Busca al hijo "Kaito"
        if (kaitoTransform != null)
        {
            animator = kaitoTransform.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("No se encontró el Animator en Kaito.");
        }
    }

    void Update()
    {
        // Si está interactuando, bloquear todos los movimientos y la rotación
        if (isInteracting) return;

        bool isWalking = Input.GetKey(KeyCode.W);
        bool isWalkingBack = Input.GetKey(KeyCode.S);
        bool isTurningLeft = Input.GetKey(KeyCode.A);
        bool isTurningRight = Input.GetKey(KeyCode.D);
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isInteractingNow = Input.GetKeyDown(KeyCode.E);

        // Movimiento
        Vector3 moveDirection = Vector3.zero;

        if (isWalking)
        {
            float speed = isRunning ? runSpeed : walkSpeed; // Si está corriendo, usa runSpeed
            moveDirection -= transform.forward * speed * Time.deltaTime;
        }
        else if (isWalkingBack)
        {
            moveDirection += transform.forward * walkSpeed * Time.deltaTime;
        }

        transform.position += moveDirection;

        // Rotación
        if (isTurningLeft)
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (isTurningRight)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Animaciones
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isWalkingBack", isWalkingBack);
        animator.SetBool("isTurningLeft", isTurningLeft);
        animator.SetBool("isTurningRight", isTurningRight);
        animator.SetBool("isRunning", isRunning);

        // Interacción
        if (isInteractingNow)
        {
            isInteracting = true; // Bloquea el movimiento
            animator.SetBool("isInteracting", true);
        }
    }

    // Método llamado desde la animación cuando finaliza interactuar
    public void EndInteraction()
    {
        isInteracting = false; // Desbloquea el movimiento
        animator.SetBool("isInteracting", false);
    }
}
