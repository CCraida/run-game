using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject gb_obj;              //背景
    float move_width = 0.05f;       //移動幅

    // Start is called before the first frame update
    void Start()
    {
        // 背景オブジェクトを見つける
        this.gb_obj = GameObject.Find("jungle-sky");
    }

    // Update is called once per frame
    void Update()
    {
        //1フレームごとにカメラを右に移動
        transform.position = new Vector3(transform.position.x + move_width,transform.position.y,transform.position.z);

        //1フレームごとに背景を右に移動
        gb_obj.transform.position = new Vector3(gb_obj.transform.position.x + move_width, gb_obj.transform.position.y, gb_obj.transform.position.z);
    }
}
