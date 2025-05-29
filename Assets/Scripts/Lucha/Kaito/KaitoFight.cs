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

    public GameObject gameManager;

    KaitoFightExtra sonidosKaito;
    GameObject enemigoActSonido;
    public bool isHit = false;

    public Animator kaitoAnimator;
    private PlayerInput input;
    private Vector2 movement;
    public bool isBlocking;
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

        sonidosKaito = FindObjectOfType<KaitoFightExtra>();
        enemigoActSonido = GameObject.FindGameObjectWithTag("Enemy");

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
        kaitoAnimator.SetBool("goRight", false);
        kaitoAnimator.SetBool("goLeft", false);
        if (movement.x != 0 && !kaitoAnimator.GetBool("isAttacking"))
        {
            if (movement.x > 0) kaitoAnimator.SetBool("goRight", true);
            else if (movement.x < 0) kaitoAnimator.SetBool("goLeft", true);
            if (!kaitoAnimator.GetBool("crouch")) rb.velocity = new Vector3(0, 0, movement.x * speed);
            else rb.velocity = Vector3.zero;
        }
        else rb.velocity = Vector3.zero;

        sonidosKaito.SonidosPisadas(movement.x > 0 || movement.x < 0);
    }

    //Control de animaciones y dirección de movimiento
    public void OnMovimiento(InputValue value)
    {
        //Bloquear
        movement = value.Get<Vector2>();
        if (movement.y < 0)
        {
            kaitoAnimator.SetBool("crouch", true);
        }
        else
        {
            kaitoAnimator.SetBool("crouch", false);
            isBlocking = false;
        }
    }

    //Animaciones de ataque
    public void OnAtacar(InputValue value)
    {
        Vector2 vectorAtaque = value.Get<Vector2>();
        isBlocking = false;

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

    //Perder vida y activar animación de recibir golpe o muerte
    public override void GetHit(float damage, Vector3 effectPos)
    {
        if(!isBlocking)
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

                isHit = true;
                sonidosKaito.SonidoHITKaitoAleatorio();
            }

            Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1); 
        }
        else
        {
            Destroy(Instantiate(blockEffect, effectPos, Quaternion.identity), 1);
        }

        enemigoActSonido?.SendMessage("DetectarGolpe", SendMessageOptions.DontRequireReceiver);
        isHit = false;
    }

    private IEnumerator Death()
    {
        kaitoAnimator.SetBool("death", true);
        yield return new WaitForSeconds(.2f);
        kaitoAnimator.SetBool("hasDied", true);
        GetComponent<PlayerInput>().DeactivateInput();

        panelMuerte.SetActive(true);

        gameManager.GetComponent<MenuPausa>().enabled = false;
        

    

    }
}