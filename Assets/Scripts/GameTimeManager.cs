using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    // singleton
    public static GameTimeManager Instance { get; private set; }

    [SerializeField] float instructionsTime;
    private float memorizeTime;
    private float organiceTime;

    public CountDown instructions1Timer { get; private set; }
    public CountDown memorizeTimer { get; private set; }
    public CountDown instructions2Timer { get; private set; }
    public CountDown organiceTimer { get; private set; }
    public enum states
    {
        instructions1,
        memorize,
        instructions2,
        play,
        results
    }
    public states currentState { get; private set; }


    private void Awake()
    {
        // singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.instructions1;

        memorizeTime = SettingsDataManager.instance.tMemorize;
        organiceTime = SettingsDataManager.instance.tOrganize;

        instructions1Timer = gameObject.AddComponent<CountDown>();
        memorizeTimer = gameObject.AddComponent<CountDown>();
        instructions2Timer = gameObject.AddComponent<CountDown>();
        organiceTimer = gameObject.AddComponent<CountDown>();
        
        StateMachine();
    }

    void StateMachine()
    {
        switch(currentState)
        {
            case states.instructions1:
                instructions1Timer.StartTimer(instructionsTime);
                break;
            case states.memorize:
                memorizeTimer.StartTimer(memorizeTime);
                break;
            case states.instructions2:
                instructions2Timer.StartTimer(instructionsTime);
                break;
            case states.play:
                organiceTimer.StartTimer(organiceTime);
                break;
            case states.results:
                CloudSaveManager.Instance.SaveDataCloud();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (instructions1Timer._isFinish && currentState == states.instructions1)
        {
            currentState = states.memorize;
            StateMachine();
        }
        if (memorizeTimer._isFinish && currentState == states.memorize)
        {
            currentState = states.instructions2;
            StateMachine();
        }
        if (instructions2Timer._isFinish && currentState == states.instructions2)
        {
            currentState = states.play;
            StateMachine();
        }
        if (organiceTimer._isFinish && currentState == states.play)
        {
            currentState = states.results;
            StateMachine();
        }
    }
}
