using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Transform ball;
    private Vector3 startMousePos, startBallPos;
    private bool moveTheBall;
    [Range(0f ,10f)]public float maxSpeed;
    [Range(0f ,1f)]public float camSpeed;
    [Range(0f ,50f)]public float pathspeed;
    private float velocity,camVelocity;
    private Camera mainCam;
    public Transform path;

    public GameObject EndScreen;
    public GameObject GScreen;
    public GameObject sliderrrr;
    public float speedcont = 0.01f;

    public Text Scoretext;
    public int score = 0 ;
    public Text scorefinal ;
    
    
    public int scoreat;
    public Text scorealltime;
    
    public Slider slider;

    



    // Start is called before the first frame update
     void Start()
    {
        score = 0; 
        ball = transform;
        mainCam = Camera.main;

        if (PlayerPrefs.HasKey("scoreat"))
        {
            score= PlayerPrefs.GetInt("scoreat"); 
        }

        scorealltime.text = " Pepsi Collected :  " + score;



    }

// Update is called once per frame
    void Update()
 {

   if (Input.GetMouseButtonDown(0))
     {
         moveTheBall = true;
         Plane newPlan = new Plane(Vector3.up, 0f);
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         if (newPlan.Raycast(ray,out var distance ))
         {
             startMousePos = ray.GetPoint(distance);
             startBallPos = ball.position;
         }

   }
   else if (Input.GetMouseButtonUp(0) && MenuManager.MenuManagerInstance.GameState)
     {
         moveTheBall=false;  
     }

   if (moveTheBall)
     {
         Plane newplan = new Plane(Vector3.up, 0f);
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             if (newplan.Raycast(ray, out var distance))
             { 
                 Vector3 mouseNewPos = ray.GetPoint(distance);
                 Vector3 MouseNewPos = mouseNewPos - startMousePos;
                 Vector3 DesireBallPos = MouseNewPos + startBallPos;

             DesireBallPos.x = Mathf.Clamp(DesireBallPos.x, -1.5f, 1.5f);

             ball.position = new Vector3(Mathf.SmoothDamp(ball.position.x,  DesireBallPos.x, ref velocity , maxSpeed )
             , ball.position.y, ball.position.z);

             }

     }
            if (MenuManager.MenuManagerInstance.GameState)
            { 
            var pathNewPos = path.position;
            path.position = new Vector3(pathNewPos.x,pathNewPos.y, Mathf.MoveTowards(pathNewPos.z, -1000f, pathspeed * Time.deltaTime));
            
            
            
            
            
        }
        var CameraNewPos = mainCam.transform.position;

        mainCam.transform.position = new Vector3(Mathf.SmoothDamp(CameraNewPos.x, ball.transform.position.x, ref camVelocity, camSpeed)
            , CameraNewPos.y, CameraNewPos.z);
        
        
            
            
        
    }


   
      private void OnTriggerEnter(Collider other )
    {
        if (other.CompareTag("obstacle"))
        {
            gameObject.SetActive(false);
            MenuManager.MenuManagerInstance.GameState = false;
            EndScreen.SetActive(true);
            
            
        }
        if (other.CompareTag("pepsi"))
        {
            slider.value++;
            pathspeed += 0.01f;
            score++;
            Scoretext.text = score.ToString() ;
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("scoreat", score);
            scorealltime.text = " Pepsi Collected : " + score; 


        }
        if (other.CompareTag("won"))
        {
            Debug.Log("win");
            gameObject.SetActive(false);
            MenuManager.MenuManagerInstance.GameState = false;
            GScreen.SetActive(true);
            scorefinal.text = score.ToString();
            
        }

    }


}
