using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public GameObject foothold;     //足場オブジェクト
    Camera mainCamObj;         　　　//Main camera
    float interval = 2.0f;          //足場生成間隔

    // Start is called before the first frame update
    void Start()
    {
        //Main camera取得
        mainCamObj = Camera.main;

        //足場生成コルーチン開始
        StartCoroutine("create_foothold");
    }

    // Update is called once per frame
    void Update()
    {
        //処理なし
    }

    IEnumerator create_foothold()
    {
        for(;;)
        {
            // 画面の右上の座標を取得 (右下じゃないので注意)
            Vector3 screen_RightTop = mainCamObj.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            //足場をローカル座標の右端+1に作成
            GameObject cloneObject = Instantiate(foothold,new Vector3(screen_RightTop.x + 1f,-4f,0f),Quaternion.identity);

            //生成したオブジェクトのサイズ変更
            change_obj_size(ref cloneObject);

            //生成したコライダのサイズ変更
            change_colider_size(ref cloneObject);

            //intervalだけストップ
            yield return new WaitForSeconds(interval);
        }
    }
    //
    // オブジェクトサイズ変更関数
    //
    void change_obj_size(ref GameObject cloneObject)
    {
        //x.y軸の変動値をランダム生成
        float x_size = Random.Range(0.2f,1f);
        float y_size = Random.Range(0.2f,1f);
        
        //現在のlocalscaleにランダム生成した値をセット
        Vector3 changedScale = transform.localScale;
        changedScale.x = x_size;
        changedScale.y = y_size;

        cloneObject.transform.localScale = changedScale;
    }

    //
    // コライダサイズ変更関数
    //
    void change_colider_size(ref GameObject cloneObject)
    {
        //SpriteRendererからサイズを取得
        Vector2 objectSize = cloneObject.GetComponent<SpriteRenderer>().bounds.size;

        //コライダを取得し、↑で取得したサイズを割り当て
        BoxCollider2D collider = cloneObject.GetComponent<BoxCollider2D> ();
        collider.size = objectSize;
    }

}
