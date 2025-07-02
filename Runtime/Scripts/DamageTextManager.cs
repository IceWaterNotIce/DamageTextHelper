using UnityEngine;

namespace DamageTextHelper
{
    /// <summary>
    /// 管理傷害數字的顯示和位置。
    /// </summary>
    /// 
    /// <remarks>
    /// 使用 DamageTextPool 來獲取和管理傷害文本物件。
    /// 支持在指定世界位置顯示傷害數字，並可選擇是否為爆擊。
    /// </remarks>
    ///
    [RequireComponent(typeof(DamageTextPool))]

    public class DamageTextManager : MonoBehaviour
    {
        public static DamageTextManager Instance;

        [Header("Settings")]
        public Vector2 horizontalOffsetRange = new Vector2(-0.5f, 0.5f);
        public Vector2 verticalOffsetRange = new Vector2(0.2f, 0.5f);

        void Awake()
        {
            Instance = this;
        }

        public void ShowDamage(int damage, Vector3 worldPosition, bool isCritical = false)
        {
            if (DamageTextManager.Instance == null)
            {
                // 嘗試尋找現有的 DamageTextManager
                var mgrObj = GameObject.FindFirstObjectByType<DamageTextManager>();
                if (mgrObj == null)
                {
                    // 自動建立 DamageTextManager
                    GameObject mgrGO = new GameObject("DamageTextManager_AutoCreated");
                    mgrObj = mgrGO.AddComponent<DamageTextManager>();
                    Debug.LogWarning("自動建立 DamageTextManager。");
                }
            }

            if (DamageTextPool.Instance == null)
            {
                // 嘗試尋找現有的 DamageTextPool
                var poolObj = GameObject.FindFirstObjectByType<DamageTextPool>();
                if (poolObj == null)
                {
                    // 自動建立 DamageTextPool
                    GameObject poolGO = new GameObject("DamageTextPool_AutoCreated");
                    poolObj = poolGO.AddComponent<DamageTextPool>();
                    // 你需要手動指定 damageTextPrefab
                    // poolObj.damageTextPrefab = ...;
                    Debug.LogWarning("自動建立 DamageTextPool，請記得指定 damageTextPrefab。");
                }
            }
            // 獲取物件池實例
            GameObject textObj = DamageTextPool.Instance.GetDamageText();

            // 計算隨機偏移
            Vector3 offset = new Vector3(
                Random.Range(horizontalOffsetRange.x, horizontalOffsetRange.y),
                Random.Range(verticalOffsetRange.x, verticalOffsetRange.y),
                0
            );

            // 設置位置
            textObj.transform.position = worldPosition + offset;

            // 初始化文字效果
            textObj.GetComponent<DamageText>().Initialize(damage, isCritical);
        }
    }
}