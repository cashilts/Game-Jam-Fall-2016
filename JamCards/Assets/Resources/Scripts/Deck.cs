using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts;


public class Deck : MonoBehaviour {

    int numberOfCards = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            UnitDat unitData = new UnitDat("Warrior", 1, 2, 1, UnitDat.unitType.ARCHER);
            GameObject cardReference = GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card"));
            Card createdCard = cardReference.GetComponent<Card>();
            Vector3 cardPos = GameObject.Find("Hand").transform.position;
            cardPos.x += (Card.cardUnitWidth - 3) * numberOfCards;
            cardReference.transform.position = cardPos;
            createdCard.changeCard(unitData);
            numberOfCards++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
