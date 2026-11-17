using UnityEngine;

namespace SlitherGame.Skins
{
    /// <summary>
    /// Defines a snake skin configuration for shop, unlocks, and rewarded ads.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSnakeSkin", menuName = "Slither/Snake Skin Data")]
    public class SnakeSkinData : ScriptableObject
    {
        [Header("Identity")]
        public string skinId;
        public string displayName;

        [Header("Visuals")]
        public Sprite headSprite;
        public Sprite bodySprite;
        public Sprite accessorySprite; // Optional hat/crown on head
        public Color[] bodyPatternColors = new Color[] { Color.green, Color.yellow };
        public bool useGlowEffect = false;

        [Header("Monetization & Unlock")]
        public bool unlockedByDefault = false;
        public int coinCost = 100;
        public bool canUnlockWithRewardedAd = true;
    }
}
