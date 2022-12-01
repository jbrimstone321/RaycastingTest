using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public Vector3 posicionModificada;
    public GameObject targetObject;
    [SerializeField]
    GameObject spawnTarget, uI;


    public enum EstadosSelector
    {
        Idle,
        SelectedMove,
        SelectedRotate,
        SelectedSpawning,
        Moving,
        Scaling,
        Rotating,
        Released,
      
    }

    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.Idle;
    // Update is called once per frame
    void Update()
    {
        switch (estadoActual)
        {
            case EstadosSelector.Idle:
                estadoActual = EstadosSelector.Idle;
                break;
            case EstadosSelector.SelectedMove:
                SelectionMove();
                break;
            case EstadosSelector.SelectedRotate:
                SelectionRotate();
                break;
            case EstadosSelector.Moving:
                Move();
                break;
            case EstadosSelector.Released:
                Release();
                break;
            case EstadosSelector.Rotating:
                Rotate();
                break;
            case EstadosSelector.SelectedSpawning:
                SelectionSpawn();
                break;
            


        }
    }
    public void ClickOnButtonM()
    {
        if (estadoActual == EstadosSelector.Idle)
        {
            estadoActual = EstadosSelector.SelectedMove;
            Debug.Log(estadoActual);
        }
    }
    public void ClickOnButtonR()
    {
        if (estadoActual == EstadosSelector.Idle)
        {
            estadoActual = EstadosSelector.SelectedRotate;
            Debug.Log("Oi");
        }
    }
    public void ClickOnButtonS()
    {
        if (estadoActual == EstadosSelector.Idle)
        {
            estadoActual = EstadosSelector.SelectedSpawning;
        }
    }
    void SelectionRotate()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            RaycastHit hitInfo;
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayo, out hitInfo))
            {
                GameObject target = hitInfo.transform.gameObject;
                if (target.CompareTag("target"))
                {
                    targetObject = hitInfo.collider.gameObject;
                    estadoActual = EstadosSelector.Rotating;
                }

            }
            else
            {
                estadoActual = EstadosSelector.Idle;

            }
        }
    }
    void SelectionMove()
    {
        Debug.Log("oi");
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown(0)))
        {
            RaycastHit hitInfo;
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayo, out hitInfo))
            {
                GameObject target = hitInfo.transform.gameObject;
                if (target.CompareTag("target"))
                {
                    targetObject = hitInfo.collider.gameObject;
                    estadoActual = EstadosSelector.Moving;
                }

            }
            else
            {
                estadoActual = EstadosSelector.Idle;
            
            }
        }

    }

    void SelectionSpawn()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown(0)))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayo, out hitInfo))
            {
                targetObject = spawnTarget;
                if (hitInfo.collider.gameObject)
                {
                    targetObject = Instantiate(spawnTarget, (hitInfo.point + Vector3.up * targetObject.transform.localScale.y / 2), Quaternion.identity);                    
                    estadoActual = EstadosSelector.Moving;
                    
                }
                
            }

        }

    }

    void Move()
    {

        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        targetObject.SetActive(false);
        if (Physics.Raycast(rayo, out hitInfo))
        {
            targetObject.transform.position = hitInfo.point + Vector3.up * targetObject.transform.localScale.y / 2;
        }
        targetObject.SetActive(true);
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown(0)))
        {
            estadoActual = EstadosSelector.Released;
        }
            
    }


    void Rotate()
    {
        
    }

    
    void Release()
    {
        targetObject = null;
        estadoActual = EstadosSelector.Idle;
    }
}
