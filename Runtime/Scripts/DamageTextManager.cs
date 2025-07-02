using UnityEngine;

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
        if (DamageTextPool.Instance == null)
        {
            Debug.LogError("DamageTextPool.Instance is null! 請確認場景中有 DamageTextPool。");
            return;
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