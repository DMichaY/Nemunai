using UnityEngine;

public class PeleaMovimiento : MonoBehaviour
{
    // Velocidad del movimiento
    public float speed = 5f;

    // Posición inicial del objeto
    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posición inicial del objeto padre (Empty)
        initialPosition = transform.position;
    }

    void Update()
    {
        // Movimiento del objeto basado en la entrada del jugador
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Bloquear posición Y y Z (mantener posición inicial en esos ejes si quieres)
        transform.position = new Vector3(transform.position.x, initialPosition.y, initialPosition.z);
    }
}
