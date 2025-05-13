using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticulasCercania : MonoBehaviour
{
    public Transform jugador;                // Referencia al jugador
    public float distanciaActivacion = 10f;  // Distancia para empezar a emitir

    private ParticleSystem sistemaParticulas;
    private bool emitiendo = false;

    void Start()
    {
        sistemaParticulas = GetComponent<ParticleSystem>();

        // Asegurarse que no esté emitiendo al principio
        var emission = sistemaParticulas.emission;
        emission.enabled = false;
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);
        var emission = sistemaParticulas.emission;

        if (distancia <= distanciaActivacion && !emitiendo)
        {
            // Empieza a emitir, pero manteniendo las partículas previas
            emission.enabled = true;
            sistemaParticulas.Play();
            emitiendo = true;
        }
        else if (distancia > distanciaActivacion && emitiendo)
        {
            // Deja de emitir, pero no borra las ya creadas
            emission.enabled = false;
            emitiendo = false;
        }
    }
}
