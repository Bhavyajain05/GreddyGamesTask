using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    private Vector2Int input;
    public int initialSize = 2;

    public float speed = 20f;
    float nextUpdate;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2Int.down;
            }
        }

        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2Int.left;
            }
        }
    }

    public void MoveVertical(int i)
    {
        if (direction.y != 0f)
        {
            if (i == 1)
            {
                input = Vector2Int.right;
            }
            else if (i == -1)
            {
                input = Vector2Int.left;
            }
        }
    }

    public void MoveHorizontal(int i)
    {
        if (direction.x != 0f)
        {
            if (i == 1)
            {
                input = Vector2Int.up;
            }
            else if (i == -1)
            {
                input = Vector2Int.down;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.time < nextUpdate)
        {
            return;
        }

        if (input != Vector2.zero)
        {
            direction = input;
        }

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        float x = Mathf.Round(transform.position.x) + direction.x;
        float z = Mathf.Round(transform.position.z) + direction.y;

        transform.position = new Vector3(x,transform.position.y ,z);

        nextUpdate = Time.time + (1f / speed);
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }

        if (other.CompareTag("Obstacle") || other.CompareTag("SnakeSegment"))
        {
            ResetState();
            ScoreManager.instance.Score = 0;
        }
    }
}
