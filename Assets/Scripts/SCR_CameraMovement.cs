using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CameraMovement : MonoBehaviour
{
    private float camMoveSpeed = 5f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 pos = gameObject.transform.position;
            pos.y += (camMoveSpeed * Time.deltaTime);
            gameObject.transform.position = pos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = gameObject.transform.position;
            pos.y -= (camMoveSpeed * Time.deltaTime);
            gameObject.transform.position = pos;
        }
        if(Input.GetKey(KeyCode.D))
        {
            Vector3 pos = gameObject.transform.position;
            pos.x += (camMoveSpeed * Time.deltaTime);
            gameObject.transform.position = pos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = gameObject.transform.position;
            pos.x -= (camMoveSpeed * Time.deltaTime);
            gameObject.transform.position = pos;
        }
    }
}
