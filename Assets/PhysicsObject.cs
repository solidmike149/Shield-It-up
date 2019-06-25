using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    //30 Valore minimo per controllare la posizione verticale
    public float minGroundNormalY = .65f;

    //3 Variabile per il modificatore di gravita
    public float gravityModifier = 1f;

    //44 Adesso ci occupiamo del movimento orizzantale che verrà modificato da fuori la classe (in PlayerPlatformerController) tramite una funzione di override
    // Variabile per il movimento orizzantale
    protected Vector2 targetVelocity;

    //31 Variabile per settare se ci troviamo sul terreno o no
    public bool grounded;

    //35 Variabile dove salvare i valori desiderati di currentNormal
    protected Vector2 groundNormal;

    //9 Variabile per referenziare il rigidbody
    protected Rigidbody2D rb2d;

    //2 Variabile dove salvare la velocita sotto forma di un vettore (origine e direzione implicita quindi possiamo considerare le sue proprieta come posizione successiva all'applicarsi di altre forze vettori)
    protected Vector2 velocity;

    //18 Filtro per decidere i collider di quali GameObject tenere in considerazione durante il cast e che setteremo in start()
    public ContactFilter2D contactFilter;

    //21 Variabile che dobbiamo passare come parametro al cast per salvarne i risultati
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    //22 Lista dove copiare i risultaty di hitbuffer 
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //15 Variabile costante per controllare il movimento minimo
    protected const float minMoveDistance = 0.001f;

    //23 Variabile costante da aggiungere a distance durante il cast per non finire dentro un altro collider
    protected const float shellRadius = 0.01f;


    //10 Aggiungiamo OnEnable dove inizializzare il rigidbody
    void OnEnable()
    {

        //11 Referenziamo il rigidbody
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

        //19 Non controlliamo i contatti trigger
        contactFilter.useTriggers = false;

        //19 Impostiamo il layer da tenere in considerazione come quello del gameObject corrente (possiamo modificarne le impostazione dal collision matrix)
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

        //20 Usiamo il settaggio del layer
        contactFilter.useLayerMask = true;
    }


    //50
    void Update()
    {

        //51 Azzeriamo targetVelocity
        targetVelocity = Vector2.zero;

        //52 Chiamiamo la funzione di override e andiamo a creare la classe PlayerPlatformerController con una funzione override ComputeVelocity()
        ComputeVelocity();
    }


    //49 Creiamo una funzione di override da usare nella classe child per modificare il valore di targetVelocity a seconda degli input
    protected virtual void ComputeVelocity()
    {
    }


    //1 Prima cosa bisogna aggiungere FixedUpdate() dove mettere le istruzioni per il movimento, creiamo prima il movimento verticale e poi quello orizzantale
    void FixedUpdate()
    {

        //4 Aggiungiamo a velocity il valore modificatore di gravita, la gravita base (physics2d.gravity è un vettore) e time.deltatime
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        //45 Impostiamo il valore x di velocity con quello di input (targetVelocity avrà i valori da input che prendiamo nella classe child)
        velocity.x = targetVelocity.x;

        //32 Inizializziamo grounded
        grounded = false;

        //5 inizializziamo un nuovo vettore per la successiva posizione rispetto a time.deltatime 
        Vector2 deltaPosition = velocity * Time.deltaTime;

        //46 Creiamo un nuovo vettore per trovare la direzione in cui vogliamo muoverci in modo da evitare bug quando si cammina su un dislivello
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        //47 Calcoliamo la prossima posizione sull'asse x rispetto alla direzione di moveAlongGround
        Vector2 move = moveAlongGround * deltaPosition.x;

        //48 Applichiamo il movimento con il secondo parametro falso perche non ci stiamo muovendo sull'asse y, applichiamo prima il movimento orizzontale e poi verticale
        Movement(move, false);

        //6 calcoliamo il movimento verticale per il prossimo movimento
        move = Vector2.up * deltaPosition.y;

        //7 applichiamo il movimento (prima creiamo la funzione)
        Movement(move, true);
    }


    //8 Vogliamo muovere l'oggetto a seconda dell'argomento passato riposizionando il rigidbody 

    //13 Adesso vogliamo aggiungere le collisioni: usiamo rigidbody2d.cast per controllare overlaps(sovrapposizioni) e aggiustare velocity applicando collisioni se la distanza dove vogliamo muoverci è maggiore di un determinato valore in modo da non controllare costantemente per collisioni
    void Movement(Vector2 move, bool yMovement)
    {

        //14 Creiamo una variabile float dove salvare la distanza per cui vogliamo muoverci (move.magnitude è un parametro float della classe Vector2)
        float distance = move.magnitude;

        //16 controlliamo se dobbiamo muoverci prima di lancire il raycast evitando di appesantire fixedupdate lanciando i raycast anche da fermi
        if (distance > minMoveDistance)
        {

            //17 Con Vector2.cast controlliamo se un collider del rigidbody andrà a sbattere contro qualcosa passandogli 4 parametri ma prima dobbiamo aggiungere delle variabili
            // direzione, castando il box nel prossimo frame
            //filtro per i risultati controllando i layer
            //array vuoto di raycasts2d
            //distanza dove castare
            //return int, numero di contatti
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            //24 Puliamo la lista
            hitBufferList.Clear();

            //25 vogliamo copiare solo gli indici che hanno colpito qualcosa, facciamo un loop sulla lista fino a count (numero di contatti)
            for (int i = 0; i < count; i++)
            {

                //26 copia
                hitBufferList.Add(hitBuffer[i]);
            }

            //27 Controlliamo la normale di ogni valore per vedere l'angolo della cosa a cui stiamo andando a sbattere
            for (int i = 0; i < hitBufferList.Count; i++)
            {

                //28 Troviamo la normale
                Vector2 currentNormal = hitBufferList[i].normal;

                //29 La vogliamo comparare a un valore minimo per vedere se stiamo sul terreno ma dobbiamo aggiungere delle variabili
                if (currentNormal.y > minGroundNormalY)
                {

                    //33 Impostiamo grounded
                    grounded = true;

                    //34 Se ci muoviamo sull'asse y (argomento passato alla chiamata di void movement)
                    if (yMovement)
                    {

                        //36 Salviamo il valore
                        groundNormal = currentNormal;

                        //37
                        currentNormal.x = 0;
                    }
                }

                //38 prendiamo la differenza tra velocity e currentnormal e controlliamo che non entri in un altro collider
                float projection = Vector2.Dot(velocity, currentNormal);

                //39 Se il dot product < 0
                if (projection < 0)
                {

                    //40 cancelliamo la velocita che verrà stoppata dalla collisione
                    velocity = velocity - projection * currentNormal;
                }

                //41 Creiamo e inizializziamo una nuova distanza con l'aggiunta del guscio
                float modifiedDistance = hitBufferList[i].distance - shellRadius;

                //42 impostiamo il valore di distance = modifiedDistance if distance < modifieddistance else distance
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        //12 riposizioniamo il rigidbody (a questo punto la stringa di codice era rb2d.position=rb2d.position + move e successivamente la modifichiamo)

        //43 Modifichiamo il riposizionamento del rigidbody con le nuove variabile trovate
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}

