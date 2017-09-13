using UnityEngine;

public class RandomManager : MonoBehaviour {

    public static RandomManager Instance;

    public Vector2 boundaries;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public Transform SetRandomPosition()
    {
        return gameObject.transform;
    }

}
