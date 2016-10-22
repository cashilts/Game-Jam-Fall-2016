using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts
{

    
    public class CardData
    {
        public enum cardType
        {
            UNIT, ABILITY, BUILDING
        };
        public string cardName;
        public cardType cType;

    }

    public class UnitDat : CardData {
        public enum unitType{
            MELEE,SWORDSMAN,FIGHTER,ARCHER,BEAST
        };
        public int attack;
        public int health;
        public int speed;
        public unitType uType;
        public UnitDat(string name, int atk, int hp, int spd, unitType type){
            cardName = name;
            cType = cardType.UNIT;
            uType = type;
            attack = atk;
            health = hp;
            speed = spd;
        }
    }
}
