using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<GameObject> grabables = new List<GameObject>();
    private List<Vector2> grabablesInitPos = new List<Vector2>();
    private List<Vector2> grabablesFinalPos = new List<Vector2>();
    private List<Vector2> grabablesUserPos = new List<Vector2>();

    private int score;

    // singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        score = 0;
        grabables.AddRange(GameObject.FindGameObjectsWithTag("Grabable"));
        SaveInitialPos(); 
    }

    #region GameLogic
    private void LateUpdate()
    {
        if (GameTimeManager.Instance.instructions1Timer._isFinish && GameTimeManager.Instance.currentState == GameTimeManager.states.instructions1)
        {
            grabablesInitPos = ShuffleList(grabablesInitPos);
            SetGrabablePos(grabablesInitPos);
        }
        if (GameTimeManager.Instance.memorizeTimer._isFinish && GameTimeManager.Instance.currentState == GameTimeManager.states.memorize)
        {
            grabablesFinalPos = ShuffleList(grabablesFinalPos);
        }
        if (GameTimeManager.Instance.instructions2Timer._isFinish && GameTimeManager.Instance.currentState == GameTimeManager.states.instructions2)
        {
            SetGrabablePos(grabablesFinalPos);
        }
        if (GameTimeManager.Instance.organiceTimer._isFinish && GameTimeManager.Instance.currentState == GameTimeManager.states.play)
        {
            GetUserPos();
            CalculateScore();
        }
    }

    private void SaveInitialPos()
    {
        for (int i = 0; i < grabables.Count; i++)
        {
            grabablesInitPos.Add(grabables[i].transform.position);
            grabablesFinalPos.Add(grabablesInitPos[i]);
        }
    }

    private void SetGrabablePos(List<Vector2> tempPos)
    {
        for (int i = 0; i < grabables.Count; i++)
        {
            grabables[i].transform.position = tempPos[i];
        }
    }

    private void GetUserPos()
    {
        for (int i = 0; i < grabables.Count; i++)
        {
            grabablesUserPos.Add(grabables[i].transform.position);
        }
    }

    private List<Vector2> ShuffleList(List<Vector2> tempPos)
    {
        System.Random _random = new System.Random();

        for(int i = tempPos.Count - 1; i > 0; i--)
        {
            int randIndex = _random.Next(0, i);
            Vector2 temp = tempPos[randIndex];
            tempPos[randIndex] = tempPos[i];
            tempPos[i] = temp;
        }

        return tempPos;
    }

    private void CalculateScore()
    {
        for(int i = 0; i < grabablesUserPos.Count; i++)
        {
            if (grabablesUserPos[i] == grabablesInitPos[i])
                score++;
        }
    }

    public int returnScore()
    {
        return score;
    }
    #endregion
}
