using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {
    int MODE = 1;
    int PLAY_MODE = 1;
    //PLAY_MODE用定数
    const int NORMAL_MODE = 1;//通常
    const int GRIND_MODE = 2;//移動
    const int SCALE_MODE = 3;//拡大縮小
    const int ROTATION_MODE = 4;//回転

    const int EXTRUSION_MODE = 5;//押出
    const int LOOPCUT_MODE = 6;//ループカット

    //MODE用定数
    const int VERTEX_MODE = 1;//頂点
    const int EDGE_MODE = 2;//辺
    const int FACE_MODE = 3;//面
    bool isShift = false;

    GameObject obj;
    public Camera cam;
    private RaycastHit hit;
    int objNum = 0;

    // Use this for initialization
    void Start () {
        obj = (GameObject)Resources.Load("prefab/Object");
	}
	
	// Update is called once per frame
	void Update () {
        InputKey();
        SelectMode();

        //Debug.Log("MODE:" + MODE);
        //Debug.Log("PLAY_MODE" + PLAY_MODE);
	}

    void InputKey()
    {
        isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown("a") && isShift) MakeVert();//頂点の作製
        
        //Shiftなしの操作のインプット

        //モード変換用インプット　頂点，辺，面
        if (Input.GetKeyDown("tab"))
        {
            if (MODE == 1) MODE = 2;
            else if (MODE == 2) MODE = 3;
            else  MODE = 1;
        }

        if (Input.GetKeyDown("g")) PLAY_MODE = GRIND_MODE;
        if (Input.GetKeyDown("s")) PLAY_MODE = SCALE_MODE;
        if (Input.GetKeyDown("r")) PLAY_MODE = ROTATION_MODE;



    }

    void SelectMode()
    {

    }

    void MakeVert()
    {
        GameObject model = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity)　as GameObject;
        model.name = "Object" + objNum;
        objNum += 1;
    }
}
