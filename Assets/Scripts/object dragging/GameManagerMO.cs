using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMO : MonoBehaviour
{
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
        {
            Selection();
        }
        else
        {
            if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
            {
                Release();
            }
            else
            {
                Move();
            }
        }
    }
    void Selection()
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
    }
    void Release()
    {
        targetObject = null;
    }
}
