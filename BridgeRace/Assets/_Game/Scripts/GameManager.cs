using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public void DelayByTime(float time, UnityAction callback, GameObject target)
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(time);
            if (callback != null && target != null)
            {
                callback.Invoke();
            }
        }
    }
}
