using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject go;
    [SerializeField]
    GameObject initPosCreate;
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
    /// Funciones de desplazamiento de CÁMARA
    /// </summary>

    public void CameraDrag()
    {
        currentState = SelectorState.DragCamera;
        ActiveCanvas(canvasMessage);
        DesactiveCanvas(canvasActions);
        
        go = cam.gameObject;

        if (Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.WaitingDrag;
        }
    }
    void Drag()
    {
        Vector3 defPos = Input.mousePosition;
        Vector3 goDefPos = go.transform.position;

        if (Input.GetMouseButton(0))
        {
            /*Vector3 curMousePos = Input.mousePosition;
            Vector3 desiredPos = new Vector3 (camDefPos.x - curMousePos.x, camDefPos.y, camDefPos.z - curMousePos.y);*/
            Vector3 curMousePos = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
            Vector3 desiredPos = goDefPos + curMousePos;

            go.transform.position = desiredPos;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.Waiting;
            CancelAction();
        }            
    }

    /// <summary>
    /// Esta funcion se debería de sustituir por las funciones de arriba
    /// </summary>

    public void EliseoCameraMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Almacenamos la posicion inicial de la camara
            initPos = Input.mousePosition;
        }else if(Input.GetMouseButton(0))
        {
            //Creamos la variable delta para almacenar la posición del cursor
            Vector3 delta = initPos - Input.mousePosition;

            //creamos la variable que determinará la posición a la que desplazaremos la cámara
            Vector3 desiredPos = new Vector3(delta.x, 0f, delta.y);

            //desplazamos la cámara y reseteamos la posición inicial
            cam.transform.position += desiredPos;
            initPos = Input.mousePosition;
        }
    }
    /// <summary>
    /// Funciones para la creación de los ÁRBOLES
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

        currentState = SelectorState.WaitingDrag;
    }

    /// <summary>
    /// Función para cancelar el desplazamiento de CÁMARA
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
