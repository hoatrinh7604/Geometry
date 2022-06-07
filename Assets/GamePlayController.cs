using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private UIController uiController;

    private float time;
    [SerializeField] float timeOfGame;

    [SerializeField] NumberContentController numberContentController;
    [SerializeField] ContentController contentController;

    [SerializeField] List<int> currentArr;
    [SerializeField] List<int> currentArrSorted;
    [SerializeField] int currentUserValue;
    [SerializeField] int leng;

    [SerializeField] int listShape;
    private int numberOfRightShape;
    private int indexOfRightShape;

    public int currentTriangle;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();

        if(time < 0)
        {
            GameOver();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int value)
    {
        if(currentTriangle == value)
        {
            UpdateScore();
            StartCoroutine(StartNextTurn());
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        if(highscore <= score)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", highscore);
            uiController.UpdateHighScore(highscore);
        }
        uiController.UpdateScore(score);
    }

    IEnumerator StartNextTurn()
    {
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }

    public void NextTurn()
    {
        currentUserValue = 0;

        leng = 4;

        indexOfRightShape = Random.Range(0, listShape);
        numberOfRightShape = Random.Range(1, 4);

        currentArr = new List<int>();
        currentArrSorted = new List<int>();

        numberContentController.Spaw(indexOfRightShape);

        for(int i = 0; i < leng; i++)
        {
            int index = Random.Range(3, 13);
            while(index == currentTriangle)
            {
                index = Random.Range(3, 13);
            }
            currentArr.Add(index);
        }
        currentArr[Random.Range(0, currentArr.Count)] = currentTriangle;

        contentController.SpawButton(currentArr);

        //currentArrSorted.Sort();
        time = timeOfGame;
    }

    public void Reset()
    {
        Time.timeScale = 1;

        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetInt("score");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);

        NextTurn();
    }

}
