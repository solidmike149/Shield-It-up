using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformer : PhysicsObject
{
    public int hp = 1;

    //10 Velocita di movimento del GameObject
    public float gdmaxSpeed;

    //5 Aggiungiamo una potenza di salto
    public float gdjumpTakeOffSpeed;

    //13 Variabile per lo sprite che ci servirà per far girare la sprite
    private SpriteRenderer spriteRenderer;

    //18 Variabile per l'animator
    public Animator animator;

    // Variabile controllo piattaforma infuocata
    public bool isBurning;

    // Variabili per movimento oggetti
    public Rigidbody2D intobject;
    public bool canMoveIt;
    public bool grabbing;

    public bool canHpUp;

    public Scrap scrapScript;

    public ShieldMovement shieldScript;

    private GameObject helmet;

    public float resetVelocity;

    private Vector2 zero = Vector2.zero;

    //12 Usiamo Awake per inizializzare i componenti
    void Awake()
    {
        //14 referenza 
        spriteRenderer = GetComponent<SpriteRenderer>();

        //19 referenza
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        helmet = transform.GetChild(1).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("OnFloor", true);
            if (isBurning)
            {
                isBurning = false;
                animator.SetBool("IsBurning", false);
            }
        }

        // Collisione proiettili e morte
        else if (collision.gameObject.CompareTag ("Projectile") || collision.gameObject.CompareTag ("TriggerProjectile"))
        {
            hp--;
            if(helmet)
            helmet.SetActive(false);
            if (hp < 1)
            {
                Destroy(transform.GetChild(0).gameObject);
                animator.SetTrigger("Dead");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("OnFloor", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingObject"))
        {
            canMoveIt = true;

            intobject = collision.gameObject.GetComponent<Rigidbody2D>();
        }
        else if (collision.CompareTag("Geyser"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(transform.GetChild(0).gameObject);
            gravityModifier = 0;
            Destroy(this);
            animator.SetTrigger("GeyserDeath");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Movimento oggetti
        if (collision.CompareTag("MovingObject"))
        {
            canMoveIt = false;

            intobject = null;
        }
    }

    public void Death()
    {
        Destroy(transform.GetChild(0).gameObject);
        animator.SetTrigger("Dead");
    }

    //1 Togliamo update e questo verra chiamato ogni frame dalla classe base per controllare input e aggiornare l'animazione relativa
    protected override void ComputeVelocity()
    {

        //2 dobbiamo prendere i valori per computevelocity, dichiariamo un vettore da azzerare a ogni computevelocity
        Vector2 move = Vector2.zero;

        //3 Prendiamo il valore x di input
        move.x = Input.GetAxis("Horizontal");

        //4 Controlliamo per  il salto e se il GameObject è a terra quindi no doublejump
        if (Input.GetButtonDown("Jump") && grounded && !grabbing)
        {
            //6 impostiamo la velocita del salto
            velocity.y = gdjumpTakeOffSpeed;

            animator.SetTrigger("JumpCharge");

            animator.SetBool("Jump", true);

            //7 cancellazione del salto, se il bottone è stato lasciato
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("Jump", false);
            //8 se stiamo ancora andando verso su
            if (velocity.y > 0)
            {
                //9 diminuiamo la velocity
                velocity.y = velocity.y * 0.5f;
            }
        }
        
        // Movimento oggetti
        if(Input.GetButtonDown("Interact") && canMoveIt == true && intobject)
        {
            grabbing = true;
            gameObject.AddComponent<FixedJoint2D>();
            gameObject.GetComponent<FixedJoint2D>().connectedBody = intobject;
            shieldScript.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(Input.GetButtonDown("Interact") && grabbing)
        {
            Destroy(gameObject.GetComponent<FixedJoint2D>());
            grabbing = false;
            shieldScript.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        
        // interazione cumulo
        if (Input.GetButtonDown("Interact") && canHpUp == true)
        {
            if(hp < 2)
            {
                StartCoroutine("ScrapChildrenBehaviour");
                animator.SetTrigger("ScrapSearch");
                hp++;
                scrapScript.used = true;
            }
        }

        //15 spriterenderer è vero se move.x e maggiore di 0.01 o minore di -0.01
        //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        //16 se è vero
        /*if (flipSprite)
        {

            //17 lo giriamo (opposto)
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }*/

        //20 settiamo i parametri all'animator
        animator.SetBool("grounded", grounded);

        //21 valore assoluto di velocity sull'asse x per maxspeed
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        //11 Movimento che verrà passato anche alla classe PhysicsObject. Adesso vogliamo far partire le animazioni
        targetVelocity = move * gdmaxSpeed;

        if (Mathf.Abs(velocity.x) > 0)
        {
            animator.SetBool("velocityX", true);           
        }
        else if(Mathf.Abs(velocity.x) == 0)
        {
            StartCoroutine("DelayCheck");
        }
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DestroyChildren()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    IEnumerator ScrapChildrenBehaviour()
    {
        GameObject pivot = transform.GetChild(0).gameObject;

        pivot.SetActive(false);

        yield return new WaitForSeconds(0.80f);

        helmet.SetActive(true);

        pivot.SetActive(true);
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(0.1f);

        if (Mathf.Abs(velocity.x) == 0)
            animator.SetBool("velocityX", false);        
    }

    IEnumerator ResetAddforce()
    {
        yield return new WaitForSeconds(resetVelocity);
        rb2d.velocity = zero;
    }
}