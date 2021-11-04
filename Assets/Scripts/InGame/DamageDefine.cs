namespace DamageDefine
{
    public enum DamageType
    {
        NONE = 0,
        
        FIRE = 1,

        THUNDER = 2,

        WIND = 3,

        ICE = 4,

        STAN = 5,
        MAX = 99,
    }

    public struct DamageParam
    {
        public int damageValue;

        public DamageType damageType;

        public int level;
    }
}