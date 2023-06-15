using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPos;

    private Vector3 randomTargetPos;
    public float randomTargetDistMin = 2;
    public float randomTargetDistMax = 10;
    public bool useRandomTarget = false;

    public float distStop = 1;
    public float distSlowDown = 2;
    private float vitesse = 0;
    public float vitesseMax = 1.0f;
    private float vitesseMin = 0.1f;
    public float acceleration = 1.0f;

    private bool atDestination = false;

    public bool drawLineTarget = true;

    public int maxCollisions = 3;
    private int collisionCount = 0;
    public float waitSecondsBeforeDestroy = 0f;
    public AudioClip soundFile;
    private AudioSource audioSrc;

    private UIScript userInterface;

    void Start()
    {
        SetRandomTargetPos();
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = soundFile;
        userInterface = GameObject.FindObjectOfType<UIScript>();
    }

    void SetRandomTargetPos()
    {
        randomTargetPos = Random.onUnitSphere;
        randomTargetPos.y = 0;
        randomTargetPos = randomTargetPos.normalized * Random.Range(randomTargetDistMin, randomTargetDistMax);
    }

    // Update is called once per frame
    void Update()
    {
        //Mise a jour du comportement
        if (target == null)
            useRandomTarget = true;

        //Calcul du point de destination
        if (useRandomTarget)
            targetPos = randomTargetPos;
        else
            targetPos = target.position;

        //Distance au point
        Vector3 deplacement = targetPos - transform.position;
        float distance = deplacement.magnitude;
        float distanceRestante = distance - distStop;
        atDestination = distanceRestante <= 0;

        //Debug (apr�s le calcul de targetPos)
        if (drawLineTarget)
            Debug.DrawLine(transform.position, targetPos, GetComponent<MeshRenderer>().material.color);

        //Deplacement
        if (!atDestination)
        {
            //On cherche � aller le plus vite vers la destination, mais � ralentir quand on arrive
            //On reste entre vitesse min et max
            float vitesseVoulue = vitesseMax;
            if (distanceRestante < distSlowDown - distStop)
                vitesseVoulue = Mathf.Lerp(vitesseMax, vitesseMin, 1.0f - distanceRestante / (distSlowDown - distStop));

            //Prise en compte de l'acc�l�ration
            if (vitesseVoulue > vitesse)
                vitesse = Mathf.Min(vitesse + acceleration * Time.deltaTime, vitesseVoulue);
            else
                vitesse = vitesseVoulue; //On freine parfaitement bien

            //D�placement
            deplacement = deplacement.normalized * vitesse * Time.deltaTime;
            transform.position += deplacement;

            //Rotation vers la target
            transform.rotation = Quaternion.LookRotation(new Vector3(deplacement.x, 0.0f, deplacement.z).normalized, Vector3.up);
        }
        else
        {
            SetRandomTargetPos();
        }
    }

    public bool drawGizmoTarget = true;

    void OnDrawGizmosSelected()
    {
        if (drawGizmoTarget)
        {
            // Sph�re rouge � la distance de stop
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            Gizmos.DrawWireSphere(targetPos, distStop);
            // Sph�re bleue � la distance de freinage
            Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            Gizmos.DrawWireSphere(targetPos, distSlowDown);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            collisionCount++;

            if (collisionCount >= maxCollisions)
            {
                if (soundFile != null && audioSrc != null && !audioSrc.isPlaying)
                    audioSrc.Play();

                string playerTag = collision.gameObject.tag;
                int playerId = (playerTag == "Player") ? 0 : 1;

                if (userInterface != null)
                {
                    // add points to the player
                    userInterface.AddPoints(playerId, 50);
                }

                Destroy(gameObject, waitSecondsBeforeDestroy);
            }
        }
    }
}