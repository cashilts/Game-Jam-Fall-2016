using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
    const float width = 1600;   
    const float height =1600;
	// Use this for initialization
	void Start () {
        

        GameObject cardImg = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-img"));
        cardImg.name = gameObject.name + "-card-img";
        Vector3 cardPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cardImg.transform.position = cardPos;
        cardImg.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/sword");
        cardImg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        Vector3 imgSize = cardImg.GetComponent<SpriteRenderer>().sprite.bounds.size;
        float widthScale = (width * 0.5f)/(imgSize.x * cardImg.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        float heightScale = (height * 0.5f) / (imgSize.y * cardImg.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        cardImg.transform.localScale = new Vector3(widthScale, heightScale, 1);

        GameObject cardText = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-text"));
        cardText.name = gameObject.name + "-card-text";
        Vector3 textPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        textPos.x -= width / 2 / 100 ;
        textPos.y -= (height / 100) / 2;
        cardText.transform.position = textPos;


        GameObject cardBorder = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-border"));
        cardBorder.name = gameObject.name + "-card-border";

        Vector3 borderPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cardBorder.transform.position = borderPos;

        widthScale = width / (imgSize.x * cardBorder.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        heightScale = height / (imgSize.y * cardBorder.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        cardBorder.transform.localScale = new Vector3(widthScale, heightScale, 1);

    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
