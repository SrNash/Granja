using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void PressButtons(GameObject go)
    {
        Debug.Log("Clickado el bot�n de:   " + go.name);
    }
}
