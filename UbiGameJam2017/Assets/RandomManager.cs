using UnityEngine;
using System.Collections.Generic;

public class RandomManager : MonoBehaviour {

    public static RandomManager Instance;

    public Vector2 boundaries;

    public List<GameObject> bonusList = new List<GameObject>();

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

    private float test = 0;
    public void Update()
    {
        test += Time.deltaTime;

        Debug.LogError(test);
        if(test > 10f)
        {
            test = 0;
            int randomItem = Random.Range(0, bonusList.Count - 1);
            var bonusToSpawn = bonusList[randomItem];

            Instantiate(bonusToSpawn, SetRandomPosition(), Quaternion.identity);
        }
    }

    public Vector2 SetRandomPosition()
    {
        return new Vector2(Random.Range(0, boundaries.x), Random.Range(0, boundaries.y));
    }

}
