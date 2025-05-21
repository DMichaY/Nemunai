using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KaitoFight : FighterClass
{
    public float speed = 3.0f, startWaitTime = 0;
    public Slider lifeBar;
    public GameObject hitEffect;
    public GameObject blockEffect;

    public GameObject panelMuerte;

    EnemyFightExtra sonidosEnemigoDemPos;
    EnemyFightAI sonidosEnemigoDemPos2;

    private Animator kaitoAnimator;
    private PlayerInput input;
    private Vector2 movement;
    private bool canMove = true;
    private Rigidbody rb;
    private float life = 100;

    public bool esPuebloInverso;

    void Start()
    {
        // Buscar los componentes
        kaitoAnimator = GetComponentInChildren<Animator>();
        kaitoAnimator.StartPlayback();
        input = GetComponent<PlayerInput>();
        input.DeactivateInput();
        rb = GetComponent<Rigidbody>();

        sonidosEnemigoDemPos = FindObjectOfType<EnemyFightExtra>();
        sonidosEnemigoDemPos2 = FindObjectOfType<EnemyFightAI>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        if (kaitoAnimator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo Kaito.");
        }

        StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        kaitoAnimator.StopPlayback();
        input.ActivateInput();
    }

    //Movimiento por rigidbody
    void Update()
    {
        if (!kaitoAnimator.GetBool("crouch") && canMove) rb.velocity = new Vector3(0, 0, movement.x * speed);
        else rb.velocity = Vector3.zero;
    }

    //Animaciones y dirección de movimiento
    public void OnMovimiento(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movement.x < 0) kaitoAnimator.SetBool("goLeft", true);
        else kaitoAnimator.SetBool("goLeft", false);
        if (movement.y < 0) kaitoAnimator.SetBool("crouch", true);
        else kaitoAnimator.SetBool("crouch", false);
    }

    //Animaciones de ataque
    public void OnAtacar(InputValue value)
    {
        Vector2 vectorAtaque = value.Get<Vector2>();

        if (vectorAtaque.x > 0)
        {
            kaitoAnimator.SetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.x < 0)
        {
            kaitoAnimator.SetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.y > 0)
        {
            kaitoAnimator.SetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.y < 0)
        {
            kaitoAnimator.SetTrigger("kickLeft");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("punchRight");
        }
    }

    //Activar movimiento después de atacar
    public void ActivateMovement()
    {
        canMove = true;
        if (movement.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movement.x < 0) kaitoAnimator.SetBool("goLeft", true);
        else kaitoAnimator.SetBool("goLeft", false);
        if (movement.y < 0) kaitoAnimator.SetBool("crouch", true);
        else kaitoAnimator.SetBool("crouch", false);
    }

    //Desactivar movimiento durante ataques
    public void DeactivateMovement()
    {
        canMove = false;
        kaitoAnimator.SetBool("goRight", false);
        kaitoAnimator.SetBool("goLeft", false);
        kaitoAnimator.SetBool("crouch", false);
    }

    //Perder vida y activar animación de recibir golpe o muerte
    public override void GetHit(float damage, Vector3 effectPos)
    {
        if(!kaitoAnimator.GetBool("crouch"))
        {
            life -= damage;
            lifeBar.value = life;

            if (esPuebloInverso)
            {
                FindObjectOfType<GolpeInverso>().RecibirGolpeInverso();
            }

            if (life <= 0)
            {
                StartCoroutine(Death());

                foreach (GameObject HB in GetComponentInChildren<KaitoFightExtra>().HBs)
                {
                    HB.SetActive(false);
                }
            }
            else
            {
                if (Random.Range(0, 2) == 0) kaitoAnimator.SetTrigger("damage1");
                else kaitoAnimator.SetTrigger("damage2");

                foreach (GameObject HB in GetComponentInChildren<KaitoFightExtra>().HBs)
                {
                    HB.SetActive(false);
                }
            }

            Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1);

            sonidosEnemigoDemPos.SonidoATQPosAleatorio();
        }
        else
        {
            Destroy(Instantiate(blockEffect, effectPos, Quaternion.identity), 1);

            sonidosEnemigoDemPos.SonidoMISSPosAleatorio();
        }
    }

    private IEnumerator Death()
    {
        kaitoAnimator.SetBool("death", true);
        yield return new WaitForSeconds(.1f);
        kaitoAnimator.SetBool("hasDied", true);
        GetComponent<PlayerInput>().DeactivateInput();

        panelMuerte.SetActive(true);
    }
}