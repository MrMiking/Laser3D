using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private char rotationAxys;

    private bool rotating = false;
    public void RotateMirror()
    {
        if(!rotating)
        {
            StartCoroutine(Rotate(new Vector3(rotationAxys == 'y' ? 90f : 0f, rotationAxys == 'x' ? 90f : 0f, rotationAxys == 'z' ? 90f : 0f), 0.25f));
        }
    }

    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        transform.rotation = endRotation;
        rotating = false;
    }
}