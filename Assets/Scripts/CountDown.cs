using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public float _waitTime { get; private set; }
    public bool _isFinish { get; set; }

    private void Start()
    {
        _isFinish = false;
    }

    public void StartTimer(float waitTime)
    {
        _waitTime = waitTime;
        StartCoroutine("StopWatch");
    }

    IEnumerator StopWatch()
    {
        while (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        _isFinish = true;
    }
}
