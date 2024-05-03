using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private char rotationAxys;

    private bool rotating = false;
    public void RotateObject()
    {
        if(!rotating)
        {
            StartCoroutine(Rotate(new Vector3(rotationAxys == 'y' ? 90f : 0f, rotationAxys == 'x' ? 90f : 0f, rotationAxys == 'z' ? 90f : 0f), 0.2f));
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

    private void EnabledLaserScript(bool state)
    {
        if (transform.CompareTag("Mirror") || transform.CompareTag("Source"))
        {
            GetComponent<CastLaser>().enabled = state;
        }
        if (transform.CompareTag("MultiMirror"))
        {
            GetComponent<MultiMirror>().enabled = state;
        }
        if (transform.CompareTag("Portal"))
        {
            GetComponent<PortalMirror>().enabled = state;
        }
    }
}