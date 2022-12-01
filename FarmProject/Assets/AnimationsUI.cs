using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsUI : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject go;

    [Header("Tiempos")]
    [SerializeField]
    float timeScaleUp;
    [SerializeField]
    float timeScaleDown;
    [SerializeField]
    float timeColorChange;

    [Header("Escalados")]
    [SerializeField]
    Vector3 scaleUp;
    [SerializeField]
    Vector3 scaleDown;

    [Header("Materiales")]
    [SerializeField]
    Color defColor;
    [SerializeField]
    Color hitColor;

    [Header("Comprobador")]
    [SerializeField]
    bool clicked = false;

    private void Start()
    {
        go = this.gameObject;
    }

    public void Hittedgo()
    {
        if (!clicked)
        {
            clicked = true;
            ScaledUp();
        }
        else if (clicked)
        {
            clicked = false;
            ScaleDown();
        }
    }
    public void ScaledUp()
    {
        LeanTween.scale(go, scaleUp, timeScaleUp).setEaseOutBounce();
        go.LeanColor(hitColor, timeColorChange).setEaseInOutQuart();
    }
    public void ScaleDown()
    { 
        LeanTween.scale(go, scaleDown, timeScaleDown).setEaseInBounce();
        go.LeanColor(defColor, timeColorChange).setEaseInOutQuart();
    }
}

