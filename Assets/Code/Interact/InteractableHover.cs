using System.Threading;
using UnityEngine;
using UnityEngine.UI;
/// 
/// Author: Marsahll Turner
/// Created: 2/12/26
/// Purpose: Have a Crosshair Appear When Hovering Over Interactables
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class InteractableHover : MonoBehaviour
{
    [SerializeField] CanvasGroup crosshairImage;
    public Image crosshairImageSmall;
    public float detectionRange = 100f;
    public LayerMask interactableLayer;
    public LayerMask hitMasks;
    public Camera UIcamera;
    public bool hint = true;

    void Update()
    {
        Ray ray = UIcamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange, hitMasks))
        {
            if ((((1 << hit.collider.gameObject.layer) & interactableLayer.value) != 0) && !crosshairImage.isActiveAndEnabled)
            //if (!crosshairImage.enabled )
            {
                crosshairImage.gameObject.SetActive(true);
                crosshairImageSmall.enabled = false;
                if(GameManager.Instance.hintsEnabled)
                {
                    TutorialManager.Instance.TriggerTutorial(3,0); // interact
                }
            } 
            else
            {
                if (crosshairImage.enabled && !(((1 << hit.collider.gameObject.layer) & interactableLayer.value) != 0) )
                {
                    crosshairImage.gameObject.SetActive(false);
                    crosshairImageSmall.enabled = true;
                }
            }
            
        }
        else
        {
            if (crosshairImage.isActiveAndEnabled)
            {
                crosshairImage.gameObject.SetActive(false);
                crosshairImageSmall.enabled = true;
            }
        }
    }
}