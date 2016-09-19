using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    public Transform target;

    // Update is called once per frame
    void Update() {
        Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)target.transform.position, 5f * Time.deltaTime);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
