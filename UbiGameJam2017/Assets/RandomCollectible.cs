using UnityEngine;

public class RandomCollectible : MonoBehaviour {

    private GameManager.Bonus _bonus;

    public GameManager.Bonus Bonus
    {
        get { return _bonus; }
    }

    public void SetBonus(GameManager.Bonus bonusType)
    {
        _bonus = bonusType;
    }
}
