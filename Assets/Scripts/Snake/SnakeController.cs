using System.Collections.Generic;
using UnityEngine;
using SlitherGame.Food;
using SlitherGame.Map;
using SlitherGame.Skins;

namespace SlitherGame.Snake
{
    /// <summary>
    /// Controls 2D snake movement, boosting, body segment following, and death loot drops.
    /// </summary>
    public class SnakeController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] private float boostSpeed = 9f;
        [SerializeField] private float turnSpeed = 220f;
        [SerializeField] private bool isPlayer = true;

        [Header("Body & Growth")]
        [SerializeField] private GameObject bodySegmentPrefab;
        [SerializeField] private int initialSegments = 8;
        [SerializeField] private float segmentSpacing = 0.4f;
        [SerializeField] private int foodPerSegment = 3;

        [Header("Skin")]
        [SerializeField] private SnakeSkinData skinData;

        private readonly List<Transform> bodySegments = new List<Transform>();
        private readonly List<Vector2> pathHistory = new List<Vector2>();
        private int storedFood;
        private bool isBoosting;
        private bool isDead;
        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
            pathHistory.Add(transform.position);

            for (int i = 0; i < initialSegments; i++)
            {
                AddBodySegment();
            }
        }

        private void Update()
        {
            if (isDead) return;

            HandleInput();
            MoveHead();
            UpdateBodySegments();
            CheckArenaBoundary();
        }

        private void HandleInput()
        {
            if (!isPlayer || mainCam == null) return;

            Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)mouseWorld - (Vector2)transform.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRot = Quaternion.Euler(0f, 0f, targetAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
            }

            isBoosting = (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && bodySegments.Count > 4;
        }

        private void MoveHead()
        {
            float speed = isBoosting ? boostSpeed : baseSpeed;
            transform.position += transform.up * (speed * Time.deltaTime);

            if (Vector2.Distance(pathHistory[0], transform.position) >= 0.05f)
            {
                pathHistory.Insert(0, transform.position);
            }
        }

        private void UpdateBodySegments()
        {
            int maxHistory = Mathf.CeilToInt((bodySegments.Count + 2) * segmentSpacing / 0.05f) + 10;
            if (pathHistory.Count > maxHistory)
            {
                pathHistory.RemoveRange(maxHistory, pathHistory.Count - maxHistory);
            }

            float distanceNeeded = segmentSpacing;
            int historyIndex = 0;

            for (int i = 0; i < bodySegments.Count; i++)
            {
                while (historyIndex < pathHistory.Count - 1 &&
                       Vector2.Distance(transform.position, pathHistory[historyIndex]) < distanceNeeded)
                {
                    historyIndex++;
                }

                bodySegments[i].position = Vector2.Lerp(
                    bodySegments[i].position,
                    pathHistory[Mathf.Min(historyIndex, pathHistory.Count - 1)],
                    Time.deltaTime * 20f
                );

                distanceNeeded += segmentSpacing;
            }
        }

        private void CheckArenaBoundary()
        {
            if (MapBoundary.Instance != null && !MapBoundary.Instance.IsInsideArena(transform.position))
            {
                Die();
            }
        }

        private void AddBodySegment()
        {
            if (bodySegmentPrefab == null) return;

            Vector3 spawnPos = bodySegments.Count > 0
                ? bodySegments[bodySegments.Count - 1].position
                : transform.position - transform.up * segmentSpacing;

            GameObject seg = Instantiate(bodySegmentPrefab, spawnPos, Quaternion.identity);
            var sr = seg.GetComponent<SpriteRenderer>();
            if (sr != null && skinData != null && skinData.bodyPatternColors.Length > 0)
            {
                sr.color = skinData.bodyPatternColors[bodySegments.Count % skinData.bodyPatternColors.Length];
            }

            bodySegments.Add(seg.transform);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDead) return;

            if (other.TryGetComponent(out FoodItem food))
            {
                storedFood += food.NutritionValue;
                if (storedFood >= foodPerSegment)
                {
                    storedFood -= foodPerSegment;
                    AddBodySegment();
                }

                if (FoodSpawner.Instance != null)
                {
                    FoodSpawner.Instance.NotifyFoodEaten();
                }
                Destroy(food.gameObject);
            }
            else if (other.CompareTag("SnakeBody") && !bodySegments.Contains(other.transform))
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;
            isDead = true;

            foreach (Transform seg in bodySegments)
            {
                if (FoodSpawner.Instance != null)
                {
                    FoodSpawner.Instance.SpawnFoodAt(seg.position, 2, 0.55f);
                }
                Destroy(seg.gameObject);
            }

            bodySegments.Clear();
            Destroy(gameObject);
        }
    }
}
