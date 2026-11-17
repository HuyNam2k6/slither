using UnityEngine;
using SlitherGame.Map;

namespace SlitherGame.Food
{
    /// <summary>
    /// Spawns ambient food across the arena and handles loot drops when snakes die.
    /// </summary>
    public class FoodSpawner : MonoBehaviour
    {
        public static FoodSpawner Instance { get; private set; }

        [Header("References")]
        [SerializeField] private FoodItem foodPrefab;

        [Header("Ambient Spawn Settings")]
        [SerializeField] private int maxAmbientFood = 300;
        [SerializeField] private float spawnInterval = 0.25f;
        [SerializeField] private Color[] foodColors = new Color[]
        {
            Color.cyan, Color.magenta, Color.yellow, Color.green, new Color(1f, 0.5f, 0f)
        };

        private int currentFoodCount;
        private float spawnTimer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (foodPrefab == null || MapBoundary.Instance == null) return;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval && currentFoodCount < maxAmbientFood)
            {
                spawnTimer = 0f;
                SpawnFoodAt(MapBoundary.Instance.GetRandomPositionInside(2f), 1, 0.35f);
            }
        }

        public void SpawnFoodAt(Vector2 position, int value, float scale)
        {
            if (foodPrefab == null) return;

            FoodItem item = Instantiate(foodPrefab, position, Quaternion.identity, transform);
            Color c = foodColors[Random.Range(0, foodColors.Length)];
            item.Initialize(value, c, scale);
            currentFoodCount++;
        }

        public void NotifyFoodEaten()
        {
            currentFoodCount = Mathf.Max(0, currentFoodCount - 1);
        }
    }
}
