using UnityEngine;
using TMPro;
using System.Collections;

public class WarningUI : MonoBehaviour
{
    public static WarningUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI warningText; // 警告テキスト用のUI要素

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 警告メッセージを表示する関数
    public void ShowWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);  // テキストを表示
        StartCoroutine(HideWarningAfterDelay(2f)); // 2秒後に非表示にするコルーチンを開始
    }

    // 警告メッセージを非表示にする関数
    private IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 2秒待つ
        warningText.gameObject.SetActive(false); // テキストを非表示にする
    }

    // 警告メッセージを非表示にする関数（外部から呼び出し可能）
    public void HideWarning()
    {
        StopAllCoroutines(); // コルーチンを停止
        warningText.gameObject.SetActive(false);  // 即座に非表示にする
    }
}