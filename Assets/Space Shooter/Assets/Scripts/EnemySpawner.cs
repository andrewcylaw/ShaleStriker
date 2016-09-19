using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    private GameController gameController;

    //public GameObject enemyPrefab;
    public GameObject[] enemyPrefabs;
    public GameObject[] coralSprites;
    public GameObject player;

	float spawnDistance = 9f;

	float enemyRate = 4;
    float coralRate = 0.8f;
	float nextEnemy = 1;
    float nextCoral = 0.8f;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

	// Update is called once per frame
	void Update () {
        if (gameController.GetCreatureSelected())
        {

            nextEnemy -= Time.deltaTime;
            if (player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude != 0)
            {
                nextCoral -= Time.deltaTime;
            }

            if (nextCoral <= 0)
            {
                nextCoral = coralRate;
                Vector3 offset = Random.onUnitSphere;
                offset.z = 0;
                offset = offset.normalized * spawnDistance;
                int rand = Random.Range(0, coralSprites.Length);
                Instantiate(coralSprites[rand], transform.position + offset, Quaternion.identity);

                if (Random.Range(0.0f, 1.0f) >= 0.5f)
                {
                    Instantiate(coralSprites[Random.Range(0, coralSprites.Length)], transform.position - offset, Quaternion.identity);
                }

            }
            if (nextEnemy <= 0)
            {
                nextEnemy = enemyRate;
                // enemy spawn rate increases with game time!
                enemyRate *= 0.9f;
                if (enemyRate < 2)
                    enemyRate = 2;

                Vector3 offset = Random.onUnitSphere;
                offset.z = 0;
                offset = offset.normalized * spawnDistance;

                GameObject temp = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position + offset, Quaternion.identity) as GameObject;
                //temp.transform.Find("GRAPHICS").GetComponent<SpriteRenderer>().sprite = enemySprites[Random.Range(0, enemySprites.Length)];
                // HERE -- we know for sure we have a player. Turn to face it!
                Vector3 dir = player.transform.position - temp.transform.position;
                //Debug.Log(Mathf.Abs(dir.sqrMagnitude));


                dir.Normalize();
                float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
                temp.transform.rotation = Quaternion.RotateTowards(temp.transform.rotation, desiredRot, 360f);


            }
        }
    }
}
