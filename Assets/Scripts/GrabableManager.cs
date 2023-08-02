using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabableManager : MonoBehaviour
{
    [SerializeField] GameObject Selector;

    AudioSource audioSource;
    [SerializeField] AudioClip grapFx, releaseFX;

    public enum state
    {
        waitSelected,
        changePosition
    }

    public state currentState { get; private set; }

    GameObject selectedGameObject;

    // singleton
    public static GrabableManager Instance { get; private set; }

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
        currentState = state.waitSelected;
    }

    public void SetSelectorPos(GameObject selectedObject)
    {
        audioSource = selectedObject.GetComponent<AudioSource>();
        if (GameTimeManager.Instance.currentState == GameTimeManager.states.play)
        {
            switch (currentState)
            {
                case state.waitSelected:
                    ShowSelector(selectedObject);
                    break;
                case state.changePosition:
                    ChangePositions(selectedObject);
                    break;
            }
        }
    }

    public void ShowSelector(GameObject selectedObject)
    {
        audioSource.PlayOneShot(grapFx);
        Vector2 pos = selectedObject.transform.position;
        Selector.gameObject.SetActive(true);
        Selector.transform.position = pos; 
        selectedGameObject = selectedObject;
        currentState = state.changePosition;
    }

    public void ChangePositions(GameObject selectedObject)
    {
        audioSource.PlayOneShot(releaseFX);
        Selector.gameObject.SetActive(false);
        Vector2 pos = selectedObject.transform.position;
        Vector2 posTemp = selectedGameObject.transform.position;
        selectedGameObject.transform.position = pos;
        selectedObject.transform.position = posTemp;
        currentState = state.waitSelected;
    }
}
