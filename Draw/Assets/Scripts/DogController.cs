using UnityEngine;

public class DogController : MonoBehaviour
{
    public Animator mAnimator;

    public GameObject deathVfx;

    public Sprite[] spriteObj;

    private void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("key_IndexHead"));

        if (PlayerPrefs.HasKey("key_IndexHead"))
        {
            int i = PlayerPrefs.GetInt("key_IndexHead");
            GetComponent<SpriteRenderer>().sprite = spriteObj[i];
            print(i);
        }

    }

    public void Hurt()
    {
        AudioManager.instance.dogAudio.Play();
        mAnimator.SetBool("Hurt", true);
        GameController.instance.currentState = GameController.STATE.GAMEOVER;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Water2D")
        {
            GameController.instance.currentState = GameController.STATE.GAMEOVER;
            Instantiate(deathVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
