using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip pickFx, releaseFX;

    // singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        // persistence
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PickFx()
    {
        if(GrabableManager.Instance.currentState == GrabableManager.state.waitSelected)
        {
            audioSource.PlayOneShot(pickFx);
        }
        else
        {
            audioSource.PlayOneShot(releaseFX);
        }
    }

    #region EventSuscription
    private void OnEnable()
    {
        GrabableManager.onPickEvent += PickFx;
    }

    private void OnDisable()
    {
        GrabableManager.onPickEvent -= PickFx;
    }
    #endregion
}
