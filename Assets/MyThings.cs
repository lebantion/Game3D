using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThings : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // 물리적으로 충돌하는 이벤트
    private void OnCollisionEnter(Collision coll) 
    {
        if(coll.gameObject.name == "CapSule")
        {
            mat.color = new Color(0, 0, 0);
        }
    }

    private void OnCollisionExit(Collision coll)
    {

    }

    private void OnCollisionStay(Collision coll)
    {

    }

    //콜라이더 충돌 발생 이벤트
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
