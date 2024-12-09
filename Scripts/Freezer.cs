using UnityEngine;
using System.Collections;

public class Freezer : MonoBehaviour
{
    float pendingFreezeDuration = 0.0f;
    bool isFrozen = false;

    private void Update()
    {   
        if (pendingFreezeDuration > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze(pendingFreezeDuration));
        }
    }

    public void Freeze(float duration)
    {
        pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze(float duration)
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0.0f;
        
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        pendingFreezeDuration = 0;
        isFrozen = false;
    }
}
