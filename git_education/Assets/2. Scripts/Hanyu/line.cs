using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    private LineRenderer lr; //선 렌더용 
    private Vector3[] linePoints = new Vector3[2]; //선만들기용 벡터배열 
    public GameObject connectedProduct;// 선을 연결할 노드
    public GameObject parents,reef; //노드의 부모브랜치, 브랜치의 마지막 커밋 
    public string comName;
    // Start is called before the first frame update

    private void Awake()
    {
        
        //GameObject test = Instantiate(connectedProduct, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public GameObject textbar;
    GameObject ntext;
    public TreeMaker TM;
    void Start()
    {
        //선 렌더용
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.material.color = Color.black;
        lr.widthMultiplier = 0.1f;
        lr.positionCount = linePoints.Length;
        //connectedProduct = GameObject.Find("Circle"); 오브젝트랑 연결 예제-회륜
        
        //이름 랜더
        ntext = Instantiate(textbar, GameObject.Find("Canvas").transform);
        ntext.GetComponent<textmaker>().names = name;
        ntext.SetActive(false);

        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        ntext.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y,0));

        if (connectedProduct)
        {
            linePoints[0] = this.transform.position; //생성된 ㄴㄷ
            linePoints[1] = connectedProduct.transform.position;  //연결할 ㄴㄷ
            lr.SetPositions(linePoints);
        }
        else
        {
            lr.enabled = false;
        }
        if(TM.branmode&& parents== transform.gameObject && !ntext.activeSelf)
        {
            ntext.SetActive(true);
        }
        else if(!TM.branmode && parents == transform.gameObject &&ntext.activeSelf)
        {
            ntext.SetActive(false);
        }
        if (TM.logmode && parents != transform.gameObject && !ntext.activeSelf)
        {
            ntext.SetActive(true);
        }
        else if (!TM.logmode && parents != transform.gameObject && ntext.activeSelf)
        {
            ntext.SetActive(false);
        }
    }


    public line cl;
    public void destoryall(GameObject branchP)
    {
        if(branchP== transform.gameObject)
        {
            Destroy(ntext);
            Destroy(transform.gameObject);
        }
        else
        {
            cl =connectedProduct.GetComponent<line>();
            cl.destoryall(branchP);
            Destroy(ntext);
            Destroy(transform.gameObject);
        }
    }
}
