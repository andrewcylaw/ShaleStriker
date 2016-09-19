using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manages all bubble spawning and swaying in the main menu
// Started 4:41am, 18 Sept 2016
public class UI_MainMenu : MonoBehaviour {

    public GameObject bubblePrefab;
    public GameObject bubbleSpawner;
    public float minBound, maxBound;

    public Image stalagmite_front; // 0 = front, 1 = middle, 2 = back
    public Image stalagmite_mid;
    public Image stalagmite_back;
    // front moves the most, with back moving the least (rocks move the least too)

    public Image text;
    public Image rocks;
    public Button play;

    public Canvas instructions;
    public Canvas context;

    private int instrCounter = 0;

	// Use this for initialization
	void Start () {
        instructions.GetComponent<CanvasGroup>().alpha = 0;
        context.GetComponent<CanvasGroup>().alpha = 0;
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        play.image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        play.interactable = false;
        StartCoroutine(UpDownSway());
        StartCoroutine(MoveTitle());
        StartCoroutine(FadeTitle(true));
        StartCoroutine(SpawnBubbles());
    }

    // Continuously spawn bubbles
    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            float randSize = Random.Range(1.0f, 2.0f);
            GameObject tempBubble = Instantiate(bubblePrefab, new Vector3(Random.Range(minBound, maxBound), -6.0f, 0.0f), bubbleSpawner.transform.rotation) as GameObject;
            tempBubble.transform.localScale = new Vector3(randSize, randSize, randSize);
            tempBubble.AddComponent<UI_Bubbles>();
            yield return new WaitForSeconds(2.0f);
        }
    }

    // If dir is true, fade in, else, fade out
    IEnumerator FadeTitle(bool dir)
    {        
        if (dir)
        {
            yield return new WaitForSeconds(2.0f);
            while (text.color.a < 0.9f)
            {
                text.color = new Color(1.0f, 1.0f, 1.0f, text.color.a + 0.05f);
                yield return new WaitForSeconds(0.15f);
            }

            yield return new WaitForSeconds(2.0f);

            while (play.image.color.a < 0.9f)
            {
                play.interactable = true;
                play.image.color = new Color(1.0f, 1.0f, 1.0f, play.image.color.a + 0.05f);
                yield return new WaitForSeconds(0.15f);
            }
        } else
        {
            while (text.color.a > 0.01f)
            {
                text.color = new Color(1.0f, 1.0f, 1.0f, text.color.a - 0.15f);                
                yield return new WaitForSeconds(0.15f);
            }
        }

        
    }

    // Animates the title
    IEnumerator MoveTitle()
    {        
        int counter = 0;
        while (true)
        {
            if(counter < 3)
            {
                text.rectTransform.anchoredPosition = new Vector2(text.rectTransform.anchoredPosition.x, text.rectTransform.anchoredPosition.y + 2f);
                play.image.rectTransform.anchoredPosition = new Vector2(play.image.rectTransform.anchoredPosition.x, play.image.rectTransform.anchoredPosition.y + 2f);
                counter++;
            } else if (counter < 9)
            {
                text.rectTransform.anchoredPosition = new Vector2(text.rectTransform.anchoredPosition.x, text.rectTransform.anchoredPosition.y - 2f);
                play.image.rectTransform.anchoredPosition = new Vector2(play.image.rectTransform.anchoredPosition.x, play.image.rectTransform.anchoredPosition.y - 2f);
                counter++;
            } else if (counter < 12)
            {
                play.image.rectTransform.anchoredPosition = new Vector2(play.image.rectTransform.anchoredPosition.x, play.image.rectTransform.anchoredPosition.y + 2f);
                text.rectTransform.anchoredPosition = new Vector2(text.rectTransform.anchoredPosition.x, text.rectTransform.anchoredPosition.y + 2f);
                counter++;
            } else
            {
                counter = 0;
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
	
    // Sways images up and down to achieve a basic animation effect
	IEnumerator UpDownSway()
    {
        int counter = 0;
        while (true)
        {
            if(counter < 3)
            {
                stalagmite_front.rectTransform.anchoredPosition = new Vector2(stalagmite_front.rectTransform.anchoredPosition.x, stalagmite_front.rectTransform.anchoredPosition.y + 3f);
                stalagmite_mid.rectTransform.anchoredPosition = new Vector2(stalagmite_mid.rectTransform.anchoredPosition.x, stalagmite_mid.rectTransform.anchoredPosition.y + 2f);
                stalagmite_back.rectTransform.anchoredPosition = new Vector2(stalagmite_back.rectTransform.anchoredPosition.x, stalagmite_back.rectTransform.anchoredPosition.y + 1f);
                rocks.rectTransform.anchoredPosition = new Vector2(rocks.rectTransform.anchoredPosition.x, rocks.rectTransform.anchoredPosition.y + 1f);
                counter++;
            } else if (counter < 9)
            {
                stalagmite_front.rectTransform.anchoredPosition = new Vector2(stalagmite_front.rectTransform.anchoredPosition.x, stalagmite_front.rectTransform.anchoredPosition.y - 3f);
                stalagmite_mid.rectTransform.anchoredPosition = new Vector2(stalagmite_mid.rectTransform.anchoredPosition.x, stalagmite_mid.rectTransform.anchoredPosition.y - 2f);
                stalagmite_back.rectTransform.anchoredPosition = new Vector2(stalagmite_back.rectTransform.anchoredPosition.x, stalagmite_back.rectTransform.anchoredPosition.y - 1f);
                rocks.rectTransform.anchoredPosition = new Vector2(rocks.rectTransform.anchoredPosition.x, rocks.rectTransform.anchoredPosition.y - 1f);
                counter++;
            } else if (counter < 12)
            {
                stalagmite_front.rectTransform.anchoredPosition = new Vector2(stalagmite_front.rectTransform.anchoredPosition.x, stalagmite_front.rectTransform.anchoredPosition.y + 3f);
                stalagmite_mid.rectTransform.anchoredPosition = new Vector2(stalagmite_mid.rectTransform.anchoredPosition.x, stalagmite_mid.rectTransform.anchoredPosition.y + 2f);
                stalagmite_back.rectTransform.anchoredPosition = new Vector2(stalagmite_back.rectTransform.anchoredPosition.x, stalagmite_back.rectTransform.anchoredPosition.y + 1f);
                rocks.rectTransform.anchoredPosition = new Vector2(rocks.rectTransform.anchoredPosition.x, rocks.rectTransform.anchoredPosition.y + 1f);               
                counter++;
            } else
            {
                counter = 0;
            }
            yield return new WaitForSeconds(0.6f);
        }

    }

    public void LoadLevel(int num)
    {
        StartCoroutine(FadeTitle(false));
        StartCoroutine(FadeInstructions());
        //SceneManager.LoadScene(num);
    }

    IEnumerator FadeInstructions()
    {
        // fade in instructions
        if(instrCounter == 0)
        {
            while (instructions.GetComponent<CanvasGroup>().alpha < 0.9f)
            {
                instructions.GetComponent<CanvasGroup>().alpha += 0.15f;
                yield return new WaitForSeconds(0.25f);
            }
            instrCounter++;
        }

        // fade out instructions, fade in context
        else if (instrCounter == 1)
        {
            while (instructions.GetComponent<CanvasGroup>().alpha > 0.0f)
            {
                instructions.GetComponent<CanvasGroup>().alpha -= 0.15f;
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.5f);

            while (context.GetComponent<CanvasGroup>().alpha < 0.9f)
            {
                context.GetComponent<CanvasGroup>().alpha += 0.15f;
                yield return new WaitForSeconds(0.25f);
            }
            instrCounter++;
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
}
