using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public Animator anim;

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizontalMove;

    Vector3 dirVec;
    GameObject scanObject;

    public GameManager manager;
    void Start()
    {
       anim = GetComponent<Animator>();
       rigid = GetComponent<Rigidbody2D>();
    }
  
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizontalMove = true;
        else if (vDown || hUp)
            isHorizontalMove = false;

        anim.SetInteger("hAxis", (int)h);
        anim.SetInteger("vAxis", (int)v);

        //방향
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        //사물인지

        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);
        Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //앞쪽 사물 스캔
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(this.gameObject.transform.position, dirVec, 1.0f, LayerMask.GetMask("object"));
        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;

      
    }
}
