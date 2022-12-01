using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Material hitMaterial;
    public AudioClip shotSound;
    private AudioSource gunAudioSource;
    int playerScore = 0;


    [SerializeField]
    TextMeshProUGUI playerScoreUI, difficultyScoreUI;

    //Barrel Spawner variables
    [SerializeField]
    GameObject barrelPrefab;
    public float globalClock = 10f;
    public float difficultyMultiplier = 1f;

    private void Start()
    {
        playerScore = 0;
        difficultyMultiplier = 1f;
        playerScoreUI.text = "SCORE = " + playerScore;
        difficultyScoreUI.text = "Diff = " + difficultyMultiplier;
    }
    //Audio Component.
    void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();
    }

   
        
    // Update is called once per frame
    void Update()
    {
        //------------------------------------------------------------------------------------------------------
        //Shooting Mechanics
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
                    Rigidbody rigidbodyLata = hitInfo.collider.GetComponent<Rigidbody>();
                    rigidbodyLata.AddForce(rayo.direction * 500f, ForceMode.VelocityChange);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                    playerScore++;
                    Debug.Log(playerScore);

                }

            }
            // else
            //{
            //  playerScore--;
            //  if (playerScore <= 0)
            //  {
            //     playerScore = 0;
            // }
            // Debug.Log(playerScore);
            // }

            playerScoreUI.text = "SCORE = " + playerScore;
            difficultyScoreUI.text = "Diff = " + difficultyMultiplier;

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lata")
        {
            Destroy(collision.gameObject);
            playerScore--;
            if (playerScore <= 0)
            {
                playerScore = 0;
            }
            Debug.Log(playerScore);
            Debug.Log("DELETUS");
            playerScoreUI.text = "SCORE = " + playerScore;
        }
    }

   public void DifficultyMultiplier()
    {
        //Dificulty Increase
        if (playerScore > 4)
        {
            difficultyMultiplier -= 0.2f;
            Debug.Log(difficultyMultiplier);
            if (difficultyMultiplier < 0.2f)
            {
                difficultyMultiplier = 0.2f;
            }
        }
        
    }

}
