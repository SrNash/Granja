using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject go;
    [SerializeField]
    Vector3 initPosCam;
    [SerializeField]
    GameObject initPosCreate;
    [SerializeField]
    GameObject camHolder;
    public Camera cam;

    [Header("Otros Scripts")]
    [SerializeField]
    LTAnimationsUIManager uiManager;

    [Header("Canvas")]
    [SerializeField]
    GameObject canvasActions;
    [SerializeField]
    GameObject canvasMessage;
    [SerializeField]
    GameObject canvasCreate;

    [Header("Variables de Suavizado-Control")]
    [SerializeField]
    float smoothDrag;

    [Header("Eliseo")]
    [SerializeField]
    Vector3 initPos;

    public enum SelectorState
    {
        Waiting,
        WaitingDrag,
        WaitingCreate,
        CreateObject,
        DraginObject,
        DragCamera,
        StopDrag,
        Drag
    }

    [SerializeField]
    SelectorState currentState = SelectorState.Waiting;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case SelectorState.Waiting:
                ActiveCanvas(canvasActions);
                break;
            case SelectorState.WaitingDrag:
                currentState = SelectorState.Drag;
                break;
            case SelectorState.WaitingCreate:
                DesactiveCanvas(canvasActions);
                break;
            case SelectorState.DragCamera:
                CameraDrag();
                //EliseoCameraMove();
                break;
            case SelectorState.Drag:
                Drag();
                break;
            case SelectorState.CreateObject:
                currentState = SelectorState.WaitingCreate;
                break;
            default:
                ActiveCanvas(canvasActions);
                break;
        }
    }

    /// <summary>
    /// Funciones de desplazamiento de C�MARA
    /// </summary>

    public void CameraDrag()
    {
        currentState = SelectorState.DragCamera;
        ActiveCanvas(canvasMessage);
        DesactiveCanvas(canvasActions);

        go = camHolder;

        if (Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.WaitingDrag;
        }
    }
    void Drag()
    {
        Vector3 defPos = Input.mousePosition;
        Vector3 goDefPos = go.transform.position;

        //Al clicar
        if (Input.GetMouseButtonDown(0))
        {
            //Almacenamos la posici�n del puntero
            initPosCam = Input.mousePosition;
        }//Al mantener pulsado
        else if (Input.GetMouseButton(0))
        {
            //Almnacenamos la posici�n inicial del puntero menos la posici�n actual del puntero
            Vector3 delta = initPosCam - Input.mousePosition;

            //Crearemos otro Vector3 con la posici�n de delta en X e Y
            Vector3 deltaPos = new Vector3(delta.x, 0f, delta.y);
            go.transform.position += deltaPos * smoothDrag;

            initPosCam = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.Waiting;
            CancelAction();
        }
        
        if (Input.GetMouseButton(1))
        {
            //Almnacenamos la rotaci�n inicial del puntero menos la posici�n actual del puntero
            Quaternion delta = go.transform.rotation;

            Quaternion deltaRot = new Quaternion();
            deltaRot.Set(go.transform.rotation.x, (go.transform.rotation.y * Input.GetAxis("Mouse X") * 45f), go.transform.rotation.z, .5f);

            go.transform.Rotate(new Vector3(0f, deltaRot.y, 0f));
            //go.transform.rotation = deltaRot;

        }//Al dejar de hacer click
        else if (Input.GetMouseButtonUp(1))
        {
            currentState = SelectorState.Waiting;
            CancelAction();
        }         
    }

    /// <summary>
    /// Esta funcion se deber�a de sustituir por las funciones de arriba
    /// </summary>

    public void EliseoCameraMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Almacenamos la posicion inicial de la camara
            initPos = Input.mousePosition;
        }else if(Input.GetMouseButton(0))
        {
            //Creamos la variable delta para almacenar la posici�n del cursor
            Vector3 delta = initPos - Input.mousePosition;

            //creamos la variable que determinar� la posici�n a la que desplazaremos la c�mara
            Vector3 desiredPos = new Vector3(delta.x, 0f, delta.y);

            //desplazamos la c�mara y reseteamos la posici�n inicial
            cam.transform.position += desiredPos;
            initPos = Input.mousePosition;
        }
    }
    /// <summary>
    /// Funciones para la creaci�n de los �RBOLES
    /// </summary>

    public void CreateTree()
    {
        ActiveCanvas(canvasCreate);
        DesactiveCanvas(canvasActions);
        uiManager.CreateMenu();
        currentState = SelectorState.CreateObject;
    }

    public void CreateSelected(GameObject prefab)
    {
        Vector3 initPos = new Vector3(0f, prefab.transform.localScale.y / 2f, 0f);
        go = Instantiate(prefab, initPosCreate.transform.position + initPos, Quaternion.identity);

        /*Ray ray;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray,out hitInfo))
        {
            if(hitInfo.collider.tag == "Ground")
            {
                Vector3 initPos = new Vector3(hitInfo.point.x, prefab.transform.localScale.y / 2f, hitInfo.point.y);
                //Vector3 initPos = new Vector3(0f, prefab.transform.localScale.y / 2f, 0f) + new Vector3(hitInfo.point.x, 0f, hitInfo.point.y);
                go = Instantiate(prefab, Vector3.zero + initPosCreate.transform.position + initPos, Quaternion.identity);
            }
        }
*/

        currentState = SelectorState.WaitingDrag;
    }

    /// <summary>
    /// Funci�n para cancelar el desplazamiento de C�MARA
    /// </summary>

    public void CancelAction()
    {
        currentState = SelectorState.Waiting;
        uiManager.CancelCreateMenu();
        DesactiveCanvas(canvasMessage);
        DesactiveCanvas(canvasCreate);
    }
    
    /// <summary>
    /// Funciones de Activar/Desactivar los CANVAS
    /// </summary>

    void ActiveCanvas(GameObject go)
    {
        uiManager.AlphaOne(go);
    }
    void DesactiveCanvas(GameObject go)
    {
        uiManager.AlphaOne(go); uiManager.AlphaZero(go);
    }
}
