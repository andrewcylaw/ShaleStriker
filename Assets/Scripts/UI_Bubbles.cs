using UnityEngine;
using System.Collections;

public class UI_Bubbles : MonoBehaviour {

    private Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
        Destroy(transform.gameObject, 10.0f);

        rb.velocity = new Vector2(0.0f, Random.Range(0.5f, 3.0f));
	}
}
