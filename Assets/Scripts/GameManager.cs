using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private RectTransform resultsPanel;
    [SerializeField] private Image[] stars = new Image[3];
    [SerializeField] private TextMeshProUGUI resultText,
                                             timerText;

    private int secondsLeft;
    private Coroutine timerInstance;

    public ResultsEnum SetScore { set { ResultsEnum temp = value;
                                        SetResult(temp); } }

    private void Start()
    {
        resultsPanel.gameObject.SetActive(false);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = new Color32(73, 64, 0, 255);
        }
        secondsLeft = 60;
        timerInstance = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        timerText.SetText(secondsLeft.ToString());
        yield return null;
        while (secondsLeft != 0)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
            timerText.SetText(secondsLeft.ToString());
        }
    }

    private void SetResult(ResultsEnum res)
    {
        resultsPanel.gameObject.SetActive(true);
        StopCoroutine(timerInstance);
        if(res.Equals(ResultsEnum.GameOver))
        {
            resultText.SetText("Game Over!!!");
        }
        else if(res.Equals(ResultsEnum.Parked))
        {
            if(secondsLeft > 0)
            {
                res = ResultsEnum.Perfect;
            }
            SetStars((int)res);
            resultText.SetText("You Won!!!");
        }
        StartCoroutine(RestartGame());

    }

    private void SetStars(int starCount)
    {
        for(int i = 0; i < starCount; i++)
        {
            stars[i].color = Color.white;
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
