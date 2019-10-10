using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour {
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshCollider meshCollider;

    private Mesh visualMesh;
    private Mesh coliderMesh;
    public List<Vector3> vertextList = new List<Vector3>();
    private List<Vector2> uvList = new List<Vector2>();
    private List<int> wireFrameIndexList = new List<int>();
    private List<int> triangleIndexList = new List<int>();

    private void CreateCubeMesh()
    {
        visualMesh = new Mesh();
        coliderMesh = new Mesh();

        vertextList.Add(new Vector3(0.5f,0.5f,0.5f));
        vertextList.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        vertextList.Add(new Vector3(0.5f, -0.5f, 0.5f));
        vertextList.Add(new Vector3(0.5f, 0.5f, -0.5f));
        vertextList.Add(new Vector3(-0.5f, -0.5f, 0.5f));
        vertextList.Add(new Vector3(0.5f, -0.5f, -0.5f));
        vertextList.Add(new Vector3(-0.5f, 0.5f, -0.5f));
        vertextList.Add(new Vector3(-0.5f, -0.5f, -0.5f));

        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0)); 
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 0));

        wireFrameIndexList.AddRange(new[] { 7,6,6,3,3,5,5,7,
                                   4,1,1,6,6,7,7,4,
                                   2,0,0,1,1,4,4,2,
                                   3,0,2,5,
                                   });

        triangleIndexList.AddRange(new[] { 7,6,5,3,5,6,
                                           4,1,7,6,7,1,
                                           2,0,4,1,4,0,
                                           5,3,2,0,2,3,
                                           6,1,3,0,3,1,
                                           4,7,2,5,2,7
                                           });

        visualMesh.SetVertices(vertextList);
        coliderMesh.SetVertices(vertextList);
        visualMesh.SetUVs(0, uvList);
        coliderMesh.SetUVs(0, uvList);
        coliderMesh.SetIndices(triangleIndexList.ToArray(), MeshTopology.Triangles, 0);
        visualMesh.SetIndices(wireFrameIndexList.ToArray(), MeshTopology.Lines, 0);

    }




	// Use this for initialization
	void Start () {
        CreateCubeMesh();
        meshFilter.mesh = visualMesh;
        meshCollider.sharedMesh = coliderMesh;
       
		
	}
	
	// Update is called once per frame
	void Update () {

        visualMesh.SetVertices(vertextList);
        coliderMesh.SetVertices(vertextList);
        meshFilter.mesh = visualMesh;
        meshCollider.sharedMesh = coliderMesh;
        if (Input.GetKey(KeyCode.E))
        {
            string log = "";
            for(int i = 0; i < vertextList.Count; i++)
            {
                log += vertextList[i].ToString();
            }
            Debug.Log(log);
        }
    }
}
