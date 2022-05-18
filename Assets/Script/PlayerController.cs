using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    public bool alive = true;
    private float creationTime;
    private bool isMove;
    private bool isDown;
    private Rigidbody rb;
    private Menu _menu;
    public float fitnes { get; private set; } //эффективность нейросети
    private int line1 = 0;
    private int line2 = 0;
    private int line3 = 0;
    private int line4 = 0;
    public NeuralNetwork brain;
    public float fitness = 0;
    private int jumpscore;

    public void SetBrait(NeuralNetwork brain)
    {
        this.brain = brain;
    }

    public void UseNeuralNetwork()
    {
        if (alive)
        {
            float[] inputs = new float[6];
            inputs[0] = line1;
            inputs[1] = line2;
            inputs[2] = line3;
            inputs[3] = line4;
            if (isDown)
            {
                inputs[4] = 1;
            }
            else
            {
                inputs[4] = 0;
            }

            inputs[5] = transform.position.y;

            var output = brain.FeedForward(inputs);
            if (output[0] > 0) ClickChanger();
        }
    }

    public void UpdateFitness()
    {
       
         brain.fitness = fitness; //updates fitness of network for sorting

    }

    private void Awake()
    {
        alive = true;
        creationTime = Time.time;
        rb = GetComponent<Rigidbody>();
        _menu = GameObject.FindWithTag("Canvas").GetComponent<Menu>();
        
    }




    private void Update()
    {
        Rays();
        if (Input.GetKeyDown("space"))
        {
            ClickChanger();
        }
if (brain != null) UseNeuralNetwork();
        if (alive)
        {
            fitness = Time.time - creationTime;// - jumpscore;
        }
    }
    
    public void Rays()
    {
    Vector3 ray1 = new Vector3(0,1,0);
    Vector3 ray2 = new Vector3(0,-1,0);
    Vector3 ray3 = new Vector3(1,1.25f,0);
    Vector3 ray4 = new Vector3(1,-1.25f,0);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position+ new Vector3(2,0), transform.TransformDirection( ray1), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("GameOver"))
            {
                line1 = 1;
                Debug.DrawRay(transform.position+ new Vector3(2,0), transform.TransformDirection( ray1) * hit.distance, Color.yellow);
            }
        }
        else
        {
            line1 = 0;
            Debug.DrawRay(transform.position+ new Vector3(0,2,0), transform.TransformDirection( ray1) * 1000, Color.red);
        }
        
        RaycastHit hit2;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position+ new Vector3(2,0), transform.TransformDirection(ray2), out hit2, Mathf.Infinity))
        {
            if (hit2.transform.CompareTag("GameOver"))
            {
                line2 = 1;
                Debug.DrawRay(transform.position, transform.TransformDirection(ray2) * hit2.distance, Color.yellow);
            }
        }
        else
        {
            line2 = 0;
            Debug.DrawRay(transform.position+ new Vector3(0,2,0), transform.TransformDirection( ray2) * 1000, Color.red);
        }
        RaycastHit hit3;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(ray3), out hit3, Mathf.Infinity))
        {
            if (hit3.transform.CompareTag("GameOver"))
            {
                line3 = 1;
                Debug.DrawRay(transform.position, transform.TransformDirection(ray3) * hit3.distance, Color.yellow);
            }
        }
        else
        {
            line3 = 0;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        RaycastHit hit4;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(ray4), out hit4, Mathf.Infinity))
        {
            if (hit4.transform.CompareTag("GameOver"))
            {
                line4 = 1;
                Debug.DrawRay(transform.position, transform.TransformDirection(ray4) * hit4.distance, Color.yellow);
            }
        }
        else
        {
            line4 = 0;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    public void ClickChanger()
    {
        if (isMove)
        {
            jumpscore++;
            isMove = false;
            if (isDown)
            {
                isDown = false;
            }
            else
            {
                isDown = true;
            }
        }
    }
 
    private void FixedUpdate()
    {
        if (isDown)
        {
           rb.AddForce(0, speed*Time.timeScale, 0, ForceMode.Acceleration);
        }

        if (!isDown)
        {
            rb.AddForce(0, -speed*Time.timeScale, 0,ForceMode.Acceleration);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        isMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("GameOver"))
        {
          //  Destroy(gameObject);
       //     _menu.GameOver();
       alive = false;
        }
    }
}
