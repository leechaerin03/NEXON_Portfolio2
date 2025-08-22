using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteb : MonoBehaviour
{
    // Start is called before the first frame update
    public TreeMaker TM;
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject cltmp,clbranchroot;
    public line cl;
    public void buttons()
    {
        GameObject cltmp = TM.click_obj;
        cl = cltmp.GetComponent<line>();
        clbranchroot = cl.parents;
        cl = clbranchroot.GetComponent<line>();
        cltmp = cl.reef;
        cl = cltmp.GetComponent<line>();  //reef 찾는 과정

        cl.destoryall(clbranchroot);
        //Destroy(myInstance);
    }

    public void buttons(string name)
    {
        GameObject cltmp = GameObject.Find(name);
        cl = cltmp.GetComponent<line>();
        clbranchroot = cl.parents;
        cl = clbranchroot.GetComponent<line>();
        cltmp = cl.reef;
        cl = cltmp.GetComponent<line>();  //reef 찾는 과정

        cl.destoryall(clbranchroot);
    }
}
