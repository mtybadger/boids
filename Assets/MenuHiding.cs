using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHiding : MonoBehaviour
{
    public bool menuDown = true;
    public void ToggleMenu()
    {
        if (menuDown)
        {
            LeanTween.rotateZ(gameObject, 180f, 0.5f).setEaseInOutQuad();
            LeanTween.moveY(transform.parent.parent.GetComponent<RectTransform>(), 288f, 0.5f).setEaseInOutQuad();
        }
        else
        {
            LeanTween.rotateZ(gameObject, 0f, 0.5f).setEaseInOutQuad();
            LeanTween.moveY(transform.parent.parent.GetComponent<RectTransform>(), 0f, 0.5f).setEaseInOutQuad();
        }

        menuDown = !menuDown;

    }
}
