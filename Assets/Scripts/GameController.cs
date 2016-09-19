using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool creatureSelected = false; // restricts player movement until they select a creature
    private bool gameOver;
    private bool restart;

    public Canvas gameOverCanvas;
    public Canvas youWinCanvas;
    public Transform player;
    

    void Start() {
        youWinCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameOver = false;
        restart = false;
    }

    void Update() {
        if (restart) {
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(0);
            }
        }
        if (player == null) {
            GameOver();
        }
    }

    public void GameOver() {
        gameOverCanvas.enabled = true;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        gameOver = true;
        restart = true;
    }


    // you win
    public void GameCompleted()
    {
        youWinCanvas.enabled = true;
        StartCoroutine(Delay());
    }

    // Sets creatureSelected to val
    public void SetCreatureSelected(bool val)
    {
        creatureSelected = val;
    }

    public bool GetCreatureSelected()
    {
        return creatureSelected;
    }

}