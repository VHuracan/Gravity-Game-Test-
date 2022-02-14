using UnityEngine;

public class MapMover : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.position+=new Vector3(-speed*Time.fixedDeltaTime,0);
    
    }
}
