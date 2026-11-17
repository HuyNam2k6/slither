using UnityEngine;

namespace SlitherGame.Map
{
    /// <summary>
    /// Defines a circular arena boundary and provides helper checks for snakes and food.
    /// </summary>
    public class MapBoundary : MonoBehaviour
    {
        public static MapBoundary Instance { get; private set; }

        [Header("Arena Settings")]
        [SerializeField] private float arenaRadius = 50f;
        [SerializeField] private Color boundaryColor = Color.red;

        public float ArenaRadius => arenaRadius;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public bool IsInsideArena(Vector2 position)
        {
            return position.sqrMagnitude <= arenaRadius * arenaRadius;
        }

        public Vector2 GetRandomPositionInside(float margin = 2f)
        {
            float maxR = Mathf.Max(1f, arenaRadius - margin);
            return Random.insideUnitCircle * maxR;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = boundaryColor;
            int segments = 64;
            float angleStep = 360f / segments;
            Vector3 prevPoint = new Vector3(arenaRadius, 0f, 0f);

            for (int i = 1; i <= segments; i++)
            {
                float rad = i * angleStep * Mathf.Deg2Rad;
                Vector3 nextPoint = new Vector3(Mathf.Cos(rad) * arenaRadius, Mathf.Sin(rad) * arenaRadius, 0f);
                Gizmos.DrawLine(transform.position + prevPoint, transform.position + nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}
