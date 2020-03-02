using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1フレームごとにカメラを右に移動
        transform.position = new Vector3(transform.position.x + 0.01f,transform.position.y,transform.position.z);
    }
}
