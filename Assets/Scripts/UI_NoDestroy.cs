using UnityEngine;
using System.Collections;

public class UI_NoDestroy : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}
