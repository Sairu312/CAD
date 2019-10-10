using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public float CamRotateSpeed = 1;
    public float CamTransferSpeed = 0.2f;

    private bool isShift = false;
    private Vector3 originPoint = new Vector3(0f, 0f, 0f);
    private float camDistance = 4f;
    public float xzTheta = 0f;
    public float yPhi = 0f;

    public float raySize = 0.1f;

    public Camera cam;
    private RaycastHit hit;
    private Vector3 prevPos;
    private Vector3 prePosVert;

    bool choiceVert = false;

    int choiceVertNum;
    ObjectMaker objMaker;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraInput();
        CameraLookAt(camDistance, xzTheta, yPhi);
        if (Input.GetKey(KeyCode.C))
        {
            Vector3 aTest = new Vector3(0.5f, 0.5f, 0.5f);
            Vector3 bTest = new Vector3(100f, 100f, 0f);
            Vector3 cTest = new Vector3(100f, 100f, 3.5f);
            Vector3 staTest = cam.WorldToScreenPoint(aTest);
            Vector3 stbTest = cam.ScreenToWorldPoint(cTest);
            Vector3 sumSTTest = staTest + bTest;
            Vector3 reSumSTTest = cam.ScreenToWorldPoint(sumSTTest);
            Debug.Log("A:" + aTest);
            Debug.Log("B:" + bTest);
            Debug.Log("ScreenA" + staTest);
            Debug.Log("ScreenB" + stbTest);
            Debug.Log("ScreenSum" + sumSTTest);
            Debug.Log("WorldSum" + reSumSTTest);
        }
    }

    void CameraInput()
    {
        isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (isShift)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) originPoint -= transform.right * CamTransferSpeed;
            if (Input.GetKey(KeyCode.RightArrow)) originPoint += transform.right * CamTransferSpeed;
            if (Input.GetKey(KeyCode.UpArrow)) originPoint += transform.up * CamTransferSpeed;
            if (Input.GetKey(KeyCode.DownArrow)) originPoint -= transform.up * CamTransferSpeed;
            //Debug.Log("Shift");
            //Debug.Log(cam.WorldToViewportPoint(new Vector3(0, 0, 0)));
            //Debug.Log(cam.WorldToScreenPoint(new Vector3(0, 0, 0)));

            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) xzTheta -= CamRotateSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) xzTheta += CamRotateSpeed;
        if (Input.GetKey(KeyCode.UpArrow)) yPhi += CamRotateSpeed;
        if (Input.GetKey(KeyCode.DownArrow)) yPhi -= CamRotateSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            choiceVert = false;
            prevPos = Input.mousePosition;
            //Debug.Log("MousePos"+Input.mousePosition);
            VertexChoice();
        }
        if (Input.GetMouseButton(0))
        {
            MoveVert();
        }
        //Debug.Log("not Shift");
    }

    void VertexChoice()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.SphereCast(ray,raySize,out hit))
        {
            Debug.Log(hit);
            string objectName = hit.collider.gameObject.name;
            GameObject hitObj = hit.collider.gameObject;
            objMaker = hitObj.GetComponent<ObjectMaker>();
            for (int i = 0; i < objMaker.vertextList.Count; i++)
            {
                if (Vector3.Distance(Input.mousePosition, cam.WorldToScreenPoint(objMaker.vertextList[i]+objMaker.transform.position)) < raySize * 100)
                {
                    //Debug.Log(cam.WorldToScreenPoint(objMaker.vertextList[i]));
                    choiceVertNum = i;
                    choiceVert = true;
                }
            }
            if(choiceVert)prePosVert = cam.WorldToScreenPoint(objMaker.vertextList[choiceVertNum]);
        }
    }

    void MoveVert()
    {
        Vector3 deltaPos = Input.mousePosition - prevPos;
        //Debug.Log(deltaPos);
        if (objMaker != null && choiceVert)
        {
            objMaker.vertextList[choiceVertNum] = cam.ScreenToWorldPoint(prePosVert + deltaPos);
        }
    }



    void CameraLookAt(float distance, float theta, float phi)
    {
        Vector3 pos = transform.position;
        pos.x = distance * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Cos(phi * Mathf.PI / 180f);
        pos.y = distance * Mathf.Sin(phi * Mathf.PI / 180f);
        pos.z = distance * Mathf.Cos(phi * Mathf.PI / 180f) * Mathf.Sin(theta * Mathf.PI / 180f);
        transform.position = pos + originPoint;
        transform.rotation = Quaternion.Euler(phi, -1 * theta - 90, 0);
    }
}

