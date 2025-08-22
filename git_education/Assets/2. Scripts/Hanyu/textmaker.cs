using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textmaker : MonoBehaviour
{
    public TextMeshProUGUI tmp2;
    public string names;
    void Start()
    {
        tmp2.text = names;
        GetComponent<RectTransform>().SetAsFirstSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
