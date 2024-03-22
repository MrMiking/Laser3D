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
            if (!hit.transform.CompareTag("Battery"))
            {
                hit.transform.gameObject.GetComponentInParent<RotateManager>().RotateMirror();
            }
        }
    }
}