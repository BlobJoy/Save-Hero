using UnityEngine;

public class BeeController : MonoBehaviour
{
    protected Rigidbody2D mRigidbody;

    protected Transform target;

    public GameObject deathVfx;

    public enum STATE
    {
        WAIT,MOVE,ATTACK
    };

    public STATE currentState;

    public float charingTime;

    private float timer;

    public AudioSource beeSound;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        currentState = STATE.WAIT;
    }
    // Start is called before the first frame update
    void Start()
    {
        int dogIndexRandom = Random.RandomRange(0, GameController.instance.currentLevel.dogList.Count);
        target = GameController.instance.currentLevel.dogList[dogIndexRandom];
        timer = 0.0f;
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            beeSound.volume = 0.2f;
        }
        else
        {
            beeSound.volume = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        switch(currentState)
        {
            case STATE.WAIT:
                break;

            case STATE.MOVE:
                mRigidbody.AddForce(Vector3.Normalize(target.position - (transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)))) * 5);
                Vector3 dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 15);
                break;

            case STATE.ATTACK:
                //Debug.Log("ATTCKING");
                timer += Time.deltaTime;

                if(timer >= charingTime)
                {
                    mRigidbody.AddForce(Vector3.Normalize(target.position - (transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)))) * 10);
                    Vector3 dir2 = target.position - transform.position;
                    float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg - 90;
                    Quaternion q2 = Quaternion.AngleAxis(angle2, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime * 15);
                }
                break;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Line")
        {
            StartToAttack();
        }

        if (collision.gameObject.tag == "Bee")
        {
            StartToAttack();
        }

        if (collision.gameObject.tag == "Dog")
        {
            StartToAttack();
            collision.gameObject.GetComponent<DogController>().Hurt();
        }

        if (collision.gameObject.tag == "Lava" || collision.gameObject.tag == "Water")
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void StartToAttack()
    {
        currentState = STATE.ATTACK;
        timer = 0.0f;
    }
}
