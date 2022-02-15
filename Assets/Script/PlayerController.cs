using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float speed;
  
    private bool isMove;
    private bool isDown;
    private Rigidbody rb;
    private Menu _menu;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            _menu.GameOver();
        }
    }
}
