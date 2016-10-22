using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update (){
        if (Input.GetKeyDown(KeyCode.Tab)){
            foreach(Camera cam in Resources.FindObjectsOfTypeAll(typeof(Camera))){
                cam.gameObject.SetActive(!cam.gameObject.activeInHierarchy);
            }
        }

        Camera gameCamera = Camera.main;
        if (gameCamera.name == "BoardCamera"){
            GameObject board = GameObject.Find("Board");
            Vector3 cameraBottomLeft = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 2));
            Vector3 cameraTopRight = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 2));
            if (cameraBottomLeft.x >= board.transform.position.x)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    gameCamera.transform.Translate(-0.1f, 0, 0);
                }
            }

            if (cameraTopRight.x <= board.GetComponent<Renderer>().bounds.size.x + board.transform.position.x)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    gameCamera.transform.Translate(0.1f, 0, 0);
                }
            }
            if (cameraBottomLeft.y >= board.transform.position.y)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    gameCamera.transform.Translate(0, -0.07f, 0);
                }
            }

            if (cameraTopRight.y <= board.GetComponent<Renderer>().bounds.size.y + board.transform.position.y)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    gameCamera.transform.Translate(0, 0.07f, 0);
                }
            }
        }
    }
}
