using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    private Transform healthBar;
    private Image healthBarImg;
    private Transform expBar;
    private Image expBarImg;
    private float timer;
    public GameObject explosion;
    public GameObject playerExplosion;
    public Transform player;
    public Sprite[] evolutionSprites;
    public RuntimeAnimatorController[] evolutionAnimators;
    private int toEvolve;
    public Transform gameController;

    public AudioClip evolveSound;
    public AudioClip winSound;

    // Use this for initialization
    void Start () {
        expBar = transform.GetChild(1);
        expBarImg = expBar.GetChild(0).GetComponent<Image>();
        healthBar = transform.GetChild(0);
        healthBarImg = healthBar.GetChild(0).GetComponent<Image>();
        //Debug.Log(transform.GetChild(1));
        toEvolve = player.GetComponent<PlayerController>().toEvolve;
    }


    public void damageDisplay(float damage) {
        healthBarImg.fillAmount -= damage;
        // if player health <= 0, destroy the player.
        if (healthBarImg.fillAmount <= 0) {
            // explode this creature
            Instantiate(explosion, player.position, player.rotation);
            Destroy(player.gameObject);
        }
    }

    public void rewardExp(float exp) {
        float tempExp = expBarImg.fillAmount + exp;
        Debug.Log((int)(exp * 100));

        if (tempExp >= 1f) {
            if (toEvolve >= 0) {
                expBarImg.fillAmount = tempExp - 1;
                evolutionize(toEvolve);
                // play evolve sound
                GetComponent<AudioSource>().clip = evolveSound;
                GetComponent<AudioSource>().Play();
            } else {
                expBarImg.fillAmount = 1;
                // Game complete!
                // play win sound
                gameController.GetComponent<GameController>().GameCompleted();
                GetComponent<AudioSource>().clip = winSound;
                GetComponent<AudioSource>().Play();
            }
        } else {
            expBarImg.fillAmount = tempExp;
        }
    }

    void evolutionize(int index) {
        player.GetComponent<SpriteRenderer>().sprite = evolutionSprites[toEvolve];
        player.GetComponent<Animator>().runtimeAnimatorController = evolutionAnimators[toEvolve];
        if (toEvolve == 0) {
            player.GetComponent<PlayerController>().setDmg(2);
        } else {
            player.GetComponent<PlayerController>().setDmg(1);
        }
        toEvolve = -1;
    }

    public void healPlayer()
    {
        float temp = 0.1f + healthBarImg.fillAmount;
        if(temp > 1.0f)
        {
            healthBarImg.fillAmount = 1.0f;
        } else
        {
            healthBarImg.fillAmount = temp;
        }
    }
}
