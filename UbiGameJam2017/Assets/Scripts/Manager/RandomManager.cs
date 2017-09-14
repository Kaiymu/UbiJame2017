using UnityEngine;
using System.Collections.Generic;
using System;

public class RandomManager : MonoBehaviour {

    public static RandomManager Instance;

    public GameObject parentCollectible;

    public Vector2 boundaries;

    public RandomCollectible bonusPrefab;

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

    private float _timeCreateCollectible = 0;
    public void Update()
    {
        _CreateRandomCollectible();
    }

    private void _CreateRandomCollectible()
    {
        _timeCreateCollectible += Time.deltaTime;

        if (_timeCreateCollectible > 3f)
        {
            _timeCreateCollectible = 0;

            var o = Instantiate(bonusPrefab, _SetRandomPosition(bonusPrefab.transform.position.z), Quaternion.identity);
            o.transform.parent = parentCollectible.transform;
            o.SetBonus(_GetRandomCollectibleType());
        }
    }

    private Vector3 _SetRandomPosition(float boundariesZ)
    {
        return new Vector3(UnityEngine.Random.Range(0, boundaries.x), UnityEngine.Random.Range(0, boundaries.y), boundariesZ);
    }

    private GameManager.Bonus _GetRandomCollectibleType()
    {
        Array values = Enum.GetValues(typeof(GameManager.Bonus));
        // We don't want to get a none type bonus
        return (GameManager.Bonus)values.GetValue(UnityEngine.Random.Range(1, values.Length));
    }

}
