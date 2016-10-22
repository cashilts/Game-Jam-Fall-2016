using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts;


public class Deck : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnitDat unitData = new UnitDat("Warrior", 1, 2, 1, UnitDat.unitType.MELEE);
        GameObject cardReference =  GameObject.Instantiate<GameObject>((GameObject)Resources.Load("Prefab/Card"));
        Card createdCard =cardReference.GetComponent<Card>();
        cardReference.transform.Translate(Card.cardUnitWidth, 0, 0);
        createdCard.changeCard(unitData);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
