using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mergbutt : MonoBehaviour
{
    public TreeMaker TM;
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>(); //선택된 노드 찾기용
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttons()
    {
        TM.mergmode = true;
    }
}
