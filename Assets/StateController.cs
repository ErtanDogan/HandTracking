using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class StateController : MonoBehaviour
{

    public MeshFilter mesh;

    public Mesh[] meshes;
    public int state = 0;

    public List<GameObject> inRange;
    public GameObject holding;

    public Transform holdPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh.mesh = meshes[0];
        StartCoroutine(ProcessWeb());
        inRange = new List<GameObject>();
        holding = null;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Keyboard.current.spaceKey.wasPressedThisFrame){
        //     state++;
        //     state = state % 3;
        //     this.changeState();

        // }

        if(holding && holding.GetComponent<Rigidbody>() != null){
            holding.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }


    }

    IEnumerator ProcessWeb(){
        while(true){
            using (UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:5000/")){
                yield return request.SendWebRequest();
                if(request.result == UnityWebRequest.Result.Success){
                    string[] input = request.downloadHandler.text.Split();
                    int newState = int.Parse(input[0]);
                    changeState(newState);

                    float posX = float.Parse(input[1]);
                    if (posX == 0.5){
                        continue;
                    }
                    posX = -Mathf.Clamp(posX * 10 - 5, -5, 5);
                    float posY = float.Parse(input[2]);
                    posY = -Mathf.Clamp(posY * 7 - 4, -4, 3);
                    transform.position = new Vector3(posX, posY, transform.position.z);

                } else {
                    Debug.Log(request.error);
                }
            }
        }  
    }

    void changeState(int newState){
        if(newState != state){
            if(state == 1 && newState == 0){
                if(inRange.Count > 0){
                    holding = inRange[0];
                    holding.transform.position = holdPos.position;
                    holding.transform.SetParent(holdPos);
                    if(holding.GetComponent<Rigidbody>() != null){
                        holding.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                        holding.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            } else if (state == 0 && newState != 0){
                if(holding != null){
                    holding.transform.SetParent(null);
                    if(holding.GetComponent<Rigidbody>() != null){
                        holding.GetComponent<Rigidbody>().useGravity = true;
                    }
                    holding = null;
                }
            }
            state = newState;
            mesh.mesh = meshes[state];
        }
        
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Object"){
            inRange.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Object"){
            inRange.Remove(other.gameObject);
        }
    }
}
