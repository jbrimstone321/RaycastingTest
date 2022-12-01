using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerST : MonoBehaviour
{
    public AudioClip shotSound;
    private AudioSource gunAudioSource;
    public Material hitMaterial;
    public Material defaultMaterial;
    public bool targetInteractionDetV;
    




    // Start is called before the first frame 
    void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //------------------------------------------------------------------------------------------------------
        //Shooting 
        if(targetInteractionDetV == false)
        {
            if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
            {
                gunAudioSource.PlayOneShot(shotSound);
                Vector3 pos = Input.mousePosition;
                if (Application.platform == RuntimePlatform.Android)
                {
                    pos = Input.GetTouch(0).position;
                }

                Ray rayo = Camera.main.ScreenPointToRay(pos);
                RaycastHit hitInfo;
                if (Physics.Raycast(rayo, out hitInfo))
                {
                    if (hitInfo.collider.tag.Equals("Lata"))
                    {
                        if (hitInfo.collider.GetComponent<TargetInteractionDet>().hasBeenShot == false)
                        {
                            GameObject target = hitInfo.collider.gameObject;
                            hitInfo.collider.GetComponent<TargetInteractionDet>().hasBeenShot = true;
                            hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                            LeanTween.scale(target, Vector3.one * 4f, 0.1f);
                        }
                        else
                        {
                            GameObject target = hitInfo.collider.gameObject;
                            hitInfo.collider.GetComponent<TargetInteractionDet>().hasBeenShot = false;
                            hitInfo.collider.GetComponent<MeshRenderer>().material = defaultMaterial;
                            LeanTween.scale(target, Vector3.one * 1f, 0.1f);
                        }
                        

                    }

                }
            }
        }

    }
}


        
