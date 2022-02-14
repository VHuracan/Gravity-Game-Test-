using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float speed;
  
    private bool isMove;
    private bool isDown;
    private Rigidbody rb;
    private Menu _menu;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _menu = GameObject.FindWithTag("Canvas").GetComponent<Menu>();
    }

   public void ClickChanger()
    {
        if (isMove)
        {
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
 
    private void Update()
    {

        

        if (isDown)
        {
           rb.AddForce(0, speed*Time.deltaTime, 0, ForceMode.Acceleration);
        }

        if (!isDown)
        {
            rb.AddForce(0, -speed*Time.deltaTime, 0,ForceMode.Acceleration);
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
            _menu.GameOver();
        }
    }
}
