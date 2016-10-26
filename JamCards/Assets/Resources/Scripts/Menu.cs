using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public static string updateMenuText = "";
    public static bool update = false;
  

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (update) {
            GameObject.Find("HostList").GetComponent<Text>().text = updateMenuText;
            update = false;
        }
	}

    public void GameStart() {
        GameObject referance = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/CameraController"));
        referance.name = "CameraController";
        referance =  (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Board"));
        referance.name = "Board";
        Destroy(gameObject);
    }

    public void OnlineStart() {
        GameObject reference = (GameObject)Instantiate(Resources.Load("Prefab/ServerMenu"));
        reference.transform.FindChild("Canvas").FindChild("HostList").gameObject.GetComponent<Text>().text = "Waiting for server...";
        NetworkClient.RequestHosts();
        Destroy(gameObject);
    }
}
