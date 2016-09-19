using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MouthPlayer : MonoBehaviour {

    public int scoreValue;
    private Done_GameController gameController;
    public Transform playerStats;

    //public GameObject shot;
    //public Transform shotSpawn;
    public float biteRate;
    private float cooldown;
    private bool entered;
    private Transform prey;
    public GameObject player;
    private float dmg;

    private int enterCount;
    private List<GameObject> ObjectsInRange = new List<GameObject>();

    void Start() {
        cooldown = 0;
        enterCount = 0;
    }

    // Update is called once per frame
    void Update() {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        } else {
            if (Input.GetKey("space")) {
                bite();
                cooldown = biteRate;
                //GetComponent<AudioSource>().Play();
            }
        }
    }

    void bite() {
        //play bite animation
        player.GetComponent<Animator>().SetTrigger("attack");
        /*
        RaycastHit2D foundHit = Physics2D.Raycast(transform.position, transform.right, 1, 1 << 8);
        if (foundHit) {
            Debug.Log("bite!");

            damageEnemy(foundHit.transform);
        }
        */
        // if enemy at mouth, bite it!
        if (enterCount > 0) {
            damageEnemy(ObjectsInRange[enterCount-1].transform);
        }
    }
    public void OnTriggerEnter2D(Collider2D col) {
        ObjectsInRange.Add(col.gameObject);
        enterCount++;
    }
    public void OnTriggerExit2D(Collider2D col) {
        //Probably you'll have to calculate wich object it is
        ObjectsInRange.Remove(col.gameObject);
        enterCount--;
    }
    /*
    // attack the enemy in contact.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Creature") {
            prey = other.transform;
            //gameController.GameOver();
        }
        // add to the score.
        //gameController.AddScore(scoreValue);
    }

    void OnTriggerExit2D(Collider2D other) {
        prey = null;
    }
    */

    void damageEnemy(Transform enemy) {
        if(enemy.tag == "Coral")
        {
            player.GetComponent<AudioSource>().Play();
            playerStats.GetComponent<PlayerStats>().healPlayer();
            Destroy(enemy.gameObject);
        } else if (enemy.tag.Equals("Creature"))
        {
            player.GetComponent<AudioSource>().Play();
            Debug.Log("bite!" + Time.time);
            dmg = player.GetComponent<PlayerController>().getDmg();
            enemy.GetComponent<HealthBar>().damageDisplay(0.3f);//dmg / 100 / enemy.GetComponent<HealthBar>().exp);
        }

    }
}
