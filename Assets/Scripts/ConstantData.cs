using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantData
{

}
public enum TYPE_ATTACK
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
    PLAYING,
    END,
    PAUSE,
    NEXT_LEVEL
}

public enum TYPE_DAMAGE
{
    FIRE,
    POISON,
    PHYSIC,
    MAD,
    ICE
}
