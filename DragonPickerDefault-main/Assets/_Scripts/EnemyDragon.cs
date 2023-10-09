using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed = 1;
    public float timeBetweenEggDrops = 0.5f;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.1f;

    [SerializeField] private bool _useSpeedUp;
    [SerializeField] private bool _useChangeYDir;
    [SerializeField] private bool _dropSecondEgg;

    [SerializeField] [Range(0, 10)] private float _distanceBetweenEggs = 5f;
    [SerializeField] [Range(0.3f, 1)] private float _eggScale = 1f;
    void Start()
    {
        Invoke("DropEgg", 2f);
    }

    void DropEgg(){
        Vector3 myVector = new Vector3(Random.Range(-_distanceBetweenEggs, 0f), 5.0f, 0.0f);
        GameObject egg1 = Instantiate<GameObject>(dragonEggPrefab);
        egg1.transform.localScale = new Vector3(dragonEggPrefab.transform.localScale.x * _eggScale,
                                               dragonEggPrefab.transform.localScale.y * _eggScale,
                                               dragonEggPrefab.transform.localScale.z * _eggScale);

        egg1.transform.position = transform.position + myVector;
        if (_dropSecondEgg)
        {
            GameObject egg2 = Instantiate<GameObject>(dragonEggPrefab);
            egg2.transform.localScale = new Vector3(dragonEggPrefab.transform.localScale.x * _eggScale,
                                                   dragonEggPrefab.transform.localScale.y * _eggScale,
                                                   dragonEggPrefab.transform.localScale.z * _eggScale);

            egg2.transform.position = transform.position + new Vector3(Random.Range(-_distanceBetweenEggs, 0f), 1.5f, 0.0f);
        }
        Invoke("DropEgg", timeBetweenEggDrops);
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;    
        transform.position = pos;

        if (pos.x < -leftRightDistance){
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistance){
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate() {
        if (Mathf.Abs(speed) >= 12) return;
        if (Random.value < chanceDirection)
        {
            if (_useChangeYDir) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + Random.Range(-2f, 2f), transform.position.z), 0.5f);
            if (_useSpeedUp) speed *= -1.05f;
            else speed *= -1;
        }
    }
}
