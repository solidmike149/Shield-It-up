using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformer : PhysicsObject
{
    public int hp = 1;

    //10 Velocita di movimento del GameObject
    public float maxSpeed = 7;

    //5 Aggiungiamo una potenza di salto
    public float jumpTakeOffSpeed = 7;

    //13 Variabile per lo sprite che ci servirà per far girare la sprite
    private SpriteRenderer spriteRenderer;

    //18 Variabile per l'animator
    private Animator animator;

    // Variabile controllo piattaforma infuocata
    public bool isBurning;

    // Variabili per movimento oggetti
    public Rigidbody2D intobject;
    public bool canMoveIt;

    public bool canHpUp;

    public FixedJoint2D joint;

    public Scrap scrapScript;

    public ShieldMovement shieldScript;

    //12 Usiamo Awake per inizializzare i componenti
    void Awake()
    {
        //14 referenza 
        spriteRenderer = GetComponent<SpriteRenderer>();

        //19 referenza
        animator = GetComponent<Animator>();

        //shieldScript = joint.connectedBody.gameObject.GetComponent<ShieldMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset Piattaforma infuocata
        if(collision.gameObject.tag == "Floor")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().freezeRotation = true;
            isBurning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Movimento oggetti
        if (collision.CompareTag("InteractableObject"))
        {
            canMoveIt = true;

            intobject = collision.gameObject.GetComponent<Rigidbody2D>();
        }

        // Mucchio di rottami
        if (collision.CompareTag("Scrap"))
        {
            canHpUp = true;

            scrapScript = collision.gameObject.GetComponent<Scrap>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Movimento oggetti
        if (collision.CompareTag("InteractableObject"))
        {
            canMoveIt = false;

            intobject = null;
        }
        // Mucchio di rottami
        if (collision.CompareTag("Scrap"))
        {
            canHpUp = false;

            scrapScript = null;
        }
    }
    //1 Togliamo update e questo verra chiamato ogni frame dalla classe base per controllare input e aggiornare l'animazione relativa
    protected override void ComputeVelocity()
    {

        //2 dobbiamo prendere i valori per computevelocity, dichiariamo un vettore da azzerare a ogni computevelocity
        Vector2 move = Vector2.zero;

        //3 Prendiamo il valore x di input
        move.x = Input.GetAxis("Horizontal");

        //4 Controlliamo per  il salto e se il GameObject è a terra quindi no doublejump
        if (Input.GetButtonDown("Jump") && grounded)
        {

            //6 impostiamo la velocita del salto
            velocity.y = jumpTakeOffSpeed;

            //7 cancellazione del salto, se il bottone è stato lasciato
        }
        else if (Input.GetButtonUp("Jump"))
        {

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
            joint.connectedBody = intobject;
        }
        else if(Input.GetButtonDown("Interact") && joint.connectedBody != shieldScript.gameObject.GetComponent<Rigidbody2D>())
        {
            joint.connectedBody = shieldScript.gameObject.GetComponent<Rigidbody2D>();
        }
        
        // interazione cumulo
        if (Input.GetButtonDown("Interact") && canHpUp == true)
        {
            if(hp < 2)
            hp++;
            scrapScript.used = true;
        }

        //15 spriterenderer è vero se move.x e maggiore di 0.01 o minore di -0.01
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        //16 se è vero
        if (flipSprite)
        {

            //17 lo giriamo (opposto)
            //spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //20 settiamo i parametri all'animator
        animator.SetBool("grounded", grounded);

        //21 valore assoluto di velocity sull'asse x per maxspeed
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        //11 Movimento che verrà passato anche alla classe PhysicsObject. Adesso vogliamo far partire le animazioni
        targetVelocity = move * maxSpeed;

        if (Mathf.Abs(velocity.x) > 0)
        {
            animator.SetBool("velocityX", true);           
        }
        else if(Mathf.Abs(velocity.x) == 0)
        {
            StartCoroutine("DelayCheck");
        }
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(0.1f);

        if (Mathf.Abs(velocity.x) == 0)
            animator.SetBool("velocityX", false);        
    }
}