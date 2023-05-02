using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMoveType : MonoBehaviour
{
    public Sprite straightShort;
    public Sprite straightFar;
    public Sprite diagonalShort;
    public Sprite diagonalFar;
    public Sprite horizontalShort;
    public Sprite horizontalFar;
    public Sprite round;

    public SpriteRenderer moveType;
    public UnitCard unitCard;

    public void SetMoveTypeImage()
    {
        switch (unitCard.GetUnitMoveType())
        {
            case UnitCardStat.MoveType.StraightShort:
                moveType.sprite = straightShort;

                break;
            case UnitCardStat.MoveType.StraightFar:
                moveType.sprite = straightFar;

                break;
            case UnitCardStat.MoveType.DiagonalShort:
                moveType.sprite = diagonalShort;

                break;
            case UnitCardStat.MoveType.DiagonalFar:
                moveType.sprite = diagonalFar;

                break;
            case UnitCardStat.MoveType.Round:
                moveType.sprite = round;

                break;
            case UnitCardStat.MoveType.HorizontalShort:
                moveType.sprite = horizontalShort;

                break;
            case UnitCardStat.MoveType.HorizontalFar:
                moveType.sprite = horizontalFar;

                break;
            default:
                break;
        }

    }

}
