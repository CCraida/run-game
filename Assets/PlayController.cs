using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    float max_speed = 10f;
    Rigidbody2D rb;
    Vector2 right_force = new Vector2(10f,0);
    Vector2 left_force = new Vector2(-10f,0);
    Vector2 jump_force = new Vector2(0,300f);
    private bool jumping = false;
    private Camera _mainCamera;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        //RigidbodyとAnimatorコンポを取得
        rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        // カメラオブジェクトを取得します
        GameObject obj = GameObject.Find ("Main Camera");
        _mainCamera = obj.GetComponent<Camera> ();

    }

    // Update is called once per frame
    void Update()
    {
        //あらかじめ設定していたintパラメーター「runs」の値を取り出し、0で初期化
        int runs = animator.GetInteger("runs");
        runs = 0;

        //現在のX方向の速度を取得
        float speedx = Mathf.Abs(this.rb.velocity.x);

        //最高速度を上回っていない場合最高速度にする
        if(speedx < max_speed)
        {
        //右→を押されたら右。左なら左に移動、
            if(Input.GetKey(KeyCode.RightArrow))
            {
                runs = 1;
                this.rb.AddForce(right_force);
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                runs = 1;
                this.rb.AddForce(left_force);
            }
        }
        
        //スペースが押されてy軸の速度ベクトルが０ならジャンプ
        if(Input.GetKeyDown(KeyCode.Space) && (jumping == false))
        {
            this.rb.AddForce(jump_force);
            jumping = true;
        }

        //向き反転処理
        int key =0;
        if(Input.GetKey(KeyCode.RightArrow)) key = 1;
        if(Input.GetKey(KeyCode.LeftArrow)) key = -1;

        if(key != 0)
        {
            transform.localScale = new Vector3(0.25f *key,0.25f,1);
        }

        this.animator.speed = 1.0f;
        //画面外への移動を制限
        Clamp();

        //runsパラメーターの値を設定する.
        animator.SetInteger("runs", runs);      
    }

    //オブジェクトに当たったらジャンプ判定フラグをfalseに設定
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("フラグ反転");
        jumping = false;
    }

    //オブジェクトの移動範囲制限
    void Clamp()
    {
        // 画面の左下の座標を取得 (左上じゃないので注意)
        Vector3 screen_LeftBottom = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 画面の右上の座標を取得 (右下じゃないので注意)
        Vector3 screen_RightTop = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector2 pos = transform.position;

        //プレイヤが画面外に出ないように移動を制限
//        Mathf.Clamp(transform.position.x,screen_RightTop.x,screen_LeftBottom.x);
        pos.x = Mathf.Clamp(transform.position.x,screen_LeftBottom.x,screen_RightTop.x);
        pos.y = Mathf.Clamp(transform.position.y,screen_LeftBottom.x,screen_RightTop.y);

        transform.position = pos;

    }



}
