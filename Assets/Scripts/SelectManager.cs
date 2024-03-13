using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hit;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            hit.transform.gameObject.GetComponentInParent<RotateManager>().RotateMirror();

            if (hit.transform.CompareTag("Mirror"))
            {
                hit.transform.GetComponentInParent<CastLaser>().StopLaser();
            }

            if (hit.transform.CompareTag("DoubleMirror"))
            {
                hit.transform.GetComponentInParent<DoubleMirror>().ResetDoubleMirror();
            }
        }
    }
}