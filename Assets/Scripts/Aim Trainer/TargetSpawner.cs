using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    int targetsNumber = 0;
    public GameObject aimTarget;
    private AudioSource gunAudioSource;
    public AudioClip shotSound;
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    int score = 0;



    void Start()
    {
        scoreTxt.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetsNumber < 4)
        {
            Spawner();
        }
        Shooting();
        scoreTxt.text = "score = " + score.ToString();
    }

    void Spawner()
    {
        var position = new Vector3(Random.Range(-17f, 17f), Random.Range(-10f, 10f), 20);
        Instantiate(aimTarget, position, Quaternion.identity);
        Debug.Log("FK OFF");
        targetsNumber++;
    }
    
    void Shooting()
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
                    Destroy(hitInfo.collider.gameObject);
                    targetsNumber--;
                    score++;
                    Debug.Log(targetsNumber);
                    Debug.Log(score);
                }

            }
        }
    }
}
