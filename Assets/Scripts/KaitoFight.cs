using System.Collections;
using UnityEngine;

public class KaitoFight : MonoBehaviour
{
    public float moveAmount = 1.0f; // Cantidad que se mueve en Z cuando se pulsa A o D
    private Animator kaitoAnimator;
    private Transform kaitoTransform;
    private bool isActionInProgress = false;

    void Start()
    {
        // Buscar el Animator en el hijo Kaito
        kaitoAnimator = GetComponentInChildren<Animator>();
        kaitoTransform = GetComponentInChildren<Transform>();

        if (kaitoAnimator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo Kaito.");
        }
    }

    void Update()
    {
        if (isActionInProgress)
        {
            // Si una animación está en progreso, no se permite otra acción
            return;
        }

        // Comprobar las teclas y ejecutar las animaciones
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(PerformAction("goLeft", moveAmount));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(PerformAction("goRight", -moveAmount));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(PerformAction("crouch"));
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(PerformAction("punchLeft"));
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(PerformAction("punchRight"));
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(PerformAction("kickLeft"));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PerformAction("kickRight"));
        }
        else
        {
            // Si no se pulsa nada, ejecutar la animación de idleFight
            kaitoAnimator.SetTrigger("idleFight");
        }
    }

    // Corrutina para manejar las acciones
    private IEnumerator PerformAction(string animationTrigger, float movementZ = 0)
    {
        // Activar el trigger de la animación
        kaitoAnimator.SetTrigger(animationTrigger);

        // Si hay movimiento, ajustar la posición en Z
        if (movementZ != 0)
        {
            Vector3 newPosition = transform.position;
            newPosition.z += movementZ;
            transform.position = newPosition;
        }

        // Marcar que una acción está en progreso
        isActionInProgress = true;

        // Esperar hasta que termine la animación antes de permitir otra acción
        yield return new WaitForSeconds(GetCurrentAnimationLength());

        // Desbloquear las acciones
        isActionInProgress = false;
    }

    // Obtener la duración de la animación actual
    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo stateInfo = kaitoAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }
}
