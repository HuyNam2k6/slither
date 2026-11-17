using UnityEngine;

namespace SlitherGame.Food
{
    /// <summary>
    /// Represents a single food pellet that grows a snake when eaten.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class FoodItem : MonoBehaviour
    {
        [SerializeField] private int nutritionValue = 1;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public int NutritionValue => nutritionValue;

        public void Initialize(int value, Color color, float scale)
        {
            nutritionValue = value;
            transform.localScale = Vector3.one * scale;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }

        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var col = GetComponent<CircleCollider2D>();
            col.isTrigger = true;
        }
    }
}
