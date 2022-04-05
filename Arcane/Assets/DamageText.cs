using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private Quaternion OrgRotation;
    private Vector3 OrgPosition;
    public TextMeshPro tmp;
    
    void Start()
    {
        tmp.text = "45";
        OrgRotation = transform.rotation;
        OrgPosition = transform.parent.transform.position - transform.position;
    }
    
    void LateUpdate()
    {
        transform.rotation = OrgRotation;
        transform.position = transform.parent.position - OrgPosition;
    }
}