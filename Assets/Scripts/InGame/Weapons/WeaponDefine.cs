using UnityEngine;

namespace WeaponDefine
{
//  キャラの武器取得時の判別用
    public enum WeaponType
    {
        NONE = 0,
        BUFF = 1,
        ELEMENT = 2,
        ATTACHMENT = 3,
        MAX = 4,
    }   

    public enum WeaponID 
    {
        NONE = 0,
        HEALTH_UP = 1,    
        POWER_UP = 2,
        SPEED_UP = 3,
        MAX = 4,
    }

    [System.Serializable]
    public struct WeaoponData
    {
        public int reality;

        public WeaponType weaponType;

        public WeaponID weaponID;
    }

    [System.Serializable]
    public struct BuffParams
    {
        public int helth;

        public int power;

        public int speed;
    }

    public enum ElementType
    {
        NONE = 0,
        
        FIRE = 1,

        THUNDER = 2,

        WIND = 3,

        ICE = 4,

        EXPLOSION = 5,

        MAX = 6,
    }
}

