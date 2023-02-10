using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider gridArea;

    public bool isMoveable;
    
    bool startedMoving;
    public float speed = 20f;
    float nextUpdate;

    [SerializeField] Vector2Int direction;
    
    
    Bounds bounds;
    private void Awake()
    {
        RandomizePosition();
        bounds = gridArea.bounds;
    }

    private void Update()
    {
        if (isMoveable)
        {
            if (Time.time < nextUpdate)
            {
                return;
            }

            if (!startedMoving)
            {
                int randNum = Random.Range(0, 2);
                startedMoving = true;
                
                if (randNum == 0)
                {
                    direction = Vector2Int.up;
                }
                else
                {
                    direction = Vector2Int.right;
                }
            }
            float x = Mathf.Round(transform.position.x) + direction.x;
            float z = Mathf.Round(transform.position.z) + direction.y;

            transform.position = new Vector3(x, transform.position.y, z);

            nextUpdate = Time.time + (1f / speed);
        }
    }

    public void RandomizePosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        x = Mathf.Round(x);
        z = Mathf.Round(z);

        transform.position = new Vector3(x, transform.position.y ,z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            direction *= -1;
        }
        
        if(other.CompareTag("Player"))
        {
            RandomizePosition();
            isMoveable = !isMoveable;
            startedMoving = false;

            ScoreManager.instance.Score += ScoreManager.instance.ScoreMultipliar;
        }
    }
}
