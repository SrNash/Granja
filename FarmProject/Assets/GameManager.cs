using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject optionsButtonsMenu;
    [SerializeField]
    GameObject messageCamMov;
    [SerializeField]
    Camera cam;

    [SerializeField]
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(canMove)
        {
            SelectorState(.5f);
        }
    }


    void SelectorState(float state)
    {
        switch (state)
        {
            case 0f:
                DesactiveUICanvas(optionsButtonsMenu);
                canMove = true;
                break;
            case 0.5f:
                ActiveUICanvas(messageCamMov);
                CamMovement();
                break;
            case .75f:
                DesactiveUICanvas(messageCamMov);
                SelectorState(1);
                break;
            case 1f:
                DesactiveUICanvas(messageCamMov);
                ActiveUICanvas(optionsButtonsMenu);
                break;
            default:
                ActiveUICanvas(optionsButtonsMenu);
                break;
        }
    }
    public void SelectionOption(int value)
    {
        SelectorState(value);
    }
    void CamMovement()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 curCamPos = cam.transform.position;
            Vector3 mousePos = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
            Vector3 currentPos = curCamPos + mousePos;
            
            cam.transform.position = currentPos;
            
            //No regresa
            if (Input.GetMouseButtonUp(0))
            {
                DesactiveUICanvas(messageCamMov);
                canMove = false;
                SelectorState(.75f);
            }

        }
    }

    public void DesactiveUICanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }
    void ActiveUICanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
}
