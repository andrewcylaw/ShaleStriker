using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class handles the player selecting which organism to play as
// TO ADD: proper canvas hiding, better buttons, etc
public class UI_SelectionMenu : MonoBehaviour {

    private GameController gameController;
    public Canvas canvas;
    public GameObject player;
    public RuntimeAnimatorController[] controllers; // 0 = anomalocaris, 1 = nectocaris, 2 = opabinia, 3 = metaspriggina
    public Sprite[] idleSprites;
    public Button[] buttons;

    void Awake()
    {
        Debug.Log("Created!");
    }

    void Start()
    {
        player.SetActive(false);
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    /*
     *  Attaches the appropriate animator controller, based on the array indices,
     *  to the player GameObject.
     */
    public void SelectPlayer(int index)
    {
        //Debug.Log("selected!");
        // 0: Anomalocaris, 1: Nectocaris, 2: Opabinia, 3: Metaspprigina
        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().sprite = idleSprites[index];
        player.GetComponent<Animator>().runtimeAnimatorController = controllers[index];
        PlayerController pc = player.GetComponent<PlayerController>();
        switch (index) {
            case 2:    // dmg order 0.5, 1, 1.5, 2
                pc.setDmg(1.5f); // divide this by exp of corresponding creature, then /100 ex. 0.1exp means 0.1 dmg, 0.05exp means 0.2 dmg.
                pc.toEvolve = 0;
                break;
            case 3:
                pc.setDmg(0.5f);
                pc.toEvolve = 1;
                break;
            case 1:
                pc.setDmg(1f);
                pc.toEvolve = -1;
                break;
            case 0:
                pc.setDmg(2f);
                pc.toEvolve = -1;
                break;
            default:
                break;
        }
        gameController.SetCreatureSelected(true);             

        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = false;
        }

        canvas.enabled = false;
    }
}
