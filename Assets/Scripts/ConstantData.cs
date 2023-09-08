using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantData
{

}
public enum ATTACK_TYPE
{
    MELEE,
    SHOOTING
}

public enum WEAPON_TYPE
{
    MACHINE_GUN,
    FIRE_GUN,
    BOW,
    SWORD,
    BOMMERANG,
    AXE,
    DARTS
}

public enum ATTACK_STAGE
{
    START,
    DURATION,
    FINISHED,
    END
}

public enum STATE_OF_CHARACTER
{
    LIVING,
    DIE,
    FIRING,
    POISON,
    MAD,
    ATTACK
}

public enum STATE_MOVE_CHARACTER
{
    IDLE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum STATE_OF_GAME
{
    START,
    PLAYING,
    SHOPPING,
    END,
    PAUSE,
    NEXT_LEVEL
}

public enum DAMAGE_TYPE
{
    MELEE,
    RANGED,
    FIRE,
    POISON
}

public enum ITEM_TYPE
{
    Weapon,
    Costume,
    Item
}

public enum ENEMY_TYPE
{
    
}

public enum RARE_TYPE
{
    NORMAL,
    BLUE,
    VIOLET,
    ORANGE,
    RED

}
