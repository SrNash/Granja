using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTAnimationsUIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField]
    GameObject canvasActions;
    [SerializeField]
    GameObject canvasMessage;
    [SerializeField]
    GameObject canvasCreate;

    [Header("Botones")]
    [SerializeField]
    Button tree1;
    [SerializeField]
    Button tree2;
    [SerializeField]
    Button tree3;
    [SerializeField]
    Button tree4;

    [Header("Tiempos")]
    [SerializeField]
    float timeScaleUp;
    [SerializeField]
    float timeScaleDown;
    [SerializeField]
    float timeColor;
    [SerializeField]
    float timeAlpha;

    [Header("Escalados")]
    [SerializeField]
    Vector3 scaleUp;
    [SerializeField]
    Vector3 scaleDown;

    [Header("Materiales")]
    [SerializeField]
    Color defColor;
    [SerializeField]
    Color clickColor;

    public void AlphaOne(GameObject go)
    {
        LeanTween.alphaCanvas(go.GetComponent<CanvasGroup>(), 1f, timeAlpha);
        go.SetActive(true);
    }
    public void AlphaZero(GameObject go)
    {
        LeanTween.alphaCanvas(go.GetComponent<CanvasGroup>(), 0f, timeAlpha);
        go.SetActive(false);
    }
    

    void ActiveObject(GameObject go)
    {
        go.SetActive(true);
    }
    void DesactiveObject(GameObject go)
    {
        go.SetActive(false);
    }


    public void ScaleUp(GameObject go)
    {
        LeanTween.scale(go, scaleUp, timeScaleUp).setEaseOutBounce();
        go.LeanColor(clickColor, timeColor).setEaseInOutQuart();
    }
    public void ScaleDown(GameObject go)
    {
        LeanTween.scale(go, scaleDown, timeScaleDown).setEaseInBounce();
        go.LeanColor(defColor, timeColor).setEaseInOutQuart();
    }

    public void CreateMenu()
    {
        LeanTween.moveLocalX(tree1.gameObject, 0f, .5f);
        LeanTween.moveLocalX(tree2.gameObject, 0f, .65f);
        LeanTween.moveLocalX(tree3.gameObject, 0f, .8f);
        LeanTween.moveLocalX(tree4.gameObject, 0f, .95f);
    }
    public void CancelCreateMenu()
    {
        LeanTween.moveLocalX(tree1.gameObject, 775f, .5f);
        LeanTween.moveLocalX(tree2.gameObject, 775f, .65f);
        LeanTween.moveLocalX(tree3.gameObject, 775f, .8f);
        LeanTween.moveLocalX(tree4.gameObject, 775f, .95f);
    }
}
