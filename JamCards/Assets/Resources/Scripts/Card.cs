using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts;

public class Card : MonoBehaviour {
    const float width = 1600;   
    const float height =1600;
    public const float cardUnitWidth = 16f;
    public int cardAttack = 0;
    public int cardHealth = 0;
    public int cardSpeed = 0;

    public CardData cardData;

	// Use this for initialization
	void Start () {
        
        GameObject cardImg = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-img"));
        cardImg.name = gameObject.name + "-img";
        Vector3 cardPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cardImg.transform.position = cardPos;
        cardImg.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/sword");
        cardImg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        Vector3 imgSize = cardImg.GetComponent<SpriteRenderer>().sprite.bounds.size;
        float widthScale = (width * 0.5f)/(imgSize.x * cardImg.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        float heightScale = (height * 0.5f) / (imgSize.y * cardImg.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        cardImg.transform.localScale = new Vector3(widthScale, heightScale, 1);

        GameObject cardName = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-text"));
        cardName.name = gameObject.name + "-name";
        cardName.GetComponent<TextMesh>().text = gameObject.name;
        Vector3 namePos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        namePos.x -= width / 2 / 100 - 1;
        namePos.y += ((height/ 100) / 2) - 1;
        cardName.transform.position = namePos;


        GameObject cardText = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-text"));
        cardText.name = gameObject.name + "-text";
        cardText.GetComponent<TextMesh>().text = " ATK HLT SPD \n   " + cardAttack.ToString() + "     " + cardHealth.ToString() + "      " + cardSpeed.ToString();
        Vector3 textPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        float heightOfText = cardText.GetComponent<MeshRenderer>().bounds.size.y;
        textPos.x -= width / 2 / 100 ;
        textPos.y -= ((height / 100) / 2) - heightOfText;
        cardText.transform.position = textPos;


        GameObject cardBorder = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card-border"));
        cardBorder.name = gameObject.name + "-border";
       
        Vector3 borderPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cardBorder.transform.position = borderPos;

        widthScale = width / (imgSize.x * cardBorder.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        heightScale = height / (imgSize.y * cardBorder.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        cardBorder.transform.localScale = new Vector3(widthScale, heightScale, 1);
        cardBorder.transform.parent = gameObject.transform;
        cardText.transform.parent = gameObject.transform;
        cardName.transform.parent = gameObject.transform;
        cardImg.transform.parent = gameObject.transform;

        if (cardData != null){
            if (cardData.cType == CardData.cardType.UNIT){
                UnitDat tempData = (UnitDat)cardData;
                changeName(tempData.cardName);
                cardAttack = tempData.attack;
                cardHealth = tempData.health;
                cardSpeed = tempData.speed;
                Sprite imgSprite = new Sprite();
                switch (tempData.uType){
                    case UnitDat.unitType.SWORDSMAN:
                        imgSprite = Resources.Load<Sprite>("Graphics/sword");
                        break;
                    case UnitDat.unitType.MELEE:
                        imgSprite = Resources.Load<Sprite>("Graphics/melee");
                        break;
                    case UnitDat.unitType.ARCHER:
                        imgSprite = Resources.Load<Sprite>("Graphics/ranged");
                        break;
                    default:
                        imgSprite = Resources.Load<Sprite>("Graphics/melee");
                        break;
                }
                string searchWorld = gameObject.name + "-img";
                Transform obj = gameObject.transform.FindChild(searchWorld);
                obj.GetComponent<SpriteRenderer>().sprite = imgSprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<TextMesh>().text = " ATK HLT SPD \n   " + cardAttack.ToString() + "     " + cardHealth.ToString() + "      " + cardSpeed.ToString();
    }

    public void changeCard(CardData newCard){
        cardData = newCard;
    }

    public void changeName(string newName){
        string oldName = gameObject.name;
        gameObject.name = newName;
        foreach(Transform child in transform) {
            child.name = child.name.Replace(oldName, newName);
        }
        gameObject.transform.FindChild(newName + "-name").GetComponent<TextMesh>().text = newName;
        
    }
}
