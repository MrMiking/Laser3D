using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    private RaycastHit hit;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() 
            && Physics.Raycast(ray, out hit) && !hit.transform.gameObject.CompareTag("Battery"))
        {
            if (hit.transform.gameObject.CompareTag("Mirror"))
            {
                hit.transform.gameObject.GetComponentInChildren<Laser>().RotateMirror();
            }
            else
            {
                hit.transform.gameObject.GetComponentInParent<Laser>().RotateMirror();
            }
        }
    }
}
