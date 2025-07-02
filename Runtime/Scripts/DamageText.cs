using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DamageText : MonoBehaviour
{
    [Header("Movement")]
    public float floatHeight = 1.5f;
    public float floatSpeed = 1f;
    
    [Header("Fade")]
    public float fadeStartDelay = 0.5f;
    public float fadeSpeed = 1f;
    
    [Header("Critical Hit")]
    public Color criticalColor = Color.red;
    public float criticalScaleMultiplier = 1.5f;
    public float criticalShakeIntensity = 0.1f;

    private TextMeshProUGUI text;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private float timer;
    private bool isCritical;
    private Vector3 shakeOffset;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    public void Initialize(int damage, bool isCritical = false)
    {
        this.isCritical = isCritical;
        timer = 0;
        
        // 重置狀態
        text.text = damage.ToString();
        text.color = isCritical ? criticalColor : Color.white;
        transform.localScale = originalScale * (isCritical ? criticalScaleMultiplier : 1f);
        
        // 初始化隨機震動偏移
        if (isCritical)
        {
            shakeOffset = new Vector3(
                Random.Range(-criticalShakeIntensity, criticalShakeIntensity),
                Random.Range(-criticalShakeIntensity, criticalShakeIntensity),
                0
            );
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        // 漂浮動畫
        float progress = timer * floatSpeed;
        transform.position = originalPosition + 
                           new Vector3(0, progress * floatHeight, 0) + 
                           (isCritical ? shakeOffset : Vector3.zero);
        
        // 淡出效果
        if (timer > fadeStartDelay)
        {
            float fadeProgress = (timer - fadeStartDelay) * fadeSpeed;
            text.color = new Color(
                text.color.r,
                text.color.g,
                text.color.b,
                1 - Mathf.Clamp01(fadeProgress)
            );
            
            // 完全透明後回歸物件池
            if (fadeProgress >= 1)
            {
                DamageTextPool.Instance.ReturnToPool(gameObject);
            }
        }
    }
}