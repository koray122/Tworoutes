using TMPro;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel; // Diyalog penceresinin ana paneli
    public TMP_Text dialogueText;    // Diyalog metninin gösterileceği alan

    public Button choiceButton1;     // 1. seçim butonu
    public Button choiceButton2;     // 2. seçim butonu
    public Button choiceButton3;     // 3. seçim butonu
    public Button choiceButton4;     // 4. seçim butonu

    public TMP_Text choiceText1;     // 1. seçim metni
    public TMP_Text choiceText2;     // 2. seçim metni
    public TMP_Text choiceText3;     // 3. seçim metni
    public TMP_Text choiceText4;     // 4. seçim metni

    public ThirdPersonController playerController; // Oyuncu hareketini kontrol eden script

    private DialogueChoice[] nextChoicesBuffer; // Sonraki seçimleri geçici olarak tutar

    private void Start()
    {
        dialoguePanel.SetActive(false); // Oyun başında diyalog panelini gizle
        HideChoices();                  // Seçenek butonlarını gizle
        LockCursor(true);               // İmleci kilitle ve gizle
    }

    // Diyalog penceresini açar ve mesaj ile seçimleri gösterir
    public void ShowDialogue(string message, DialogueChoice choice1 = null, DialogueChoice choice2 = null, DialogueChoice choice3 = null, DialogueChoice choice4 = null)
    {
        dialoguePanel.SetActive(true);      // Diyalog panelini aç
        dialogueText.text = message;        // Diyalog metnini göster
        LockCursor(false);                  // İmleci serbest bırak ve görünür yap
        playerController.enabled = false;   // Oyuncu hareketini devre dışı bırak

        // Her bir seçim için ilgili buton ve metni ayarla
        SetChoice(choiceButton1, choiceText1, choice1);
        SetChoice(choiceButton2, choiceText2, choice2);
        SetChoice(choiceButton3, choiceText3, choice3);
        SetChoice(choiceButton4, choiceText4, choice4);
    }

    // Diyalog penceresini ve seçimleri gizler, oyuncu kontrolünü geri verir
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);     // Diyalog panelini kapat
        dialogueText.text = "";             // Diyalog metnini temizle
        HideChoices();                      // Seçenekleri gizle
        LockCursor(true);                   // İmleci tekrar kilitle ve gizle
        playerController.enabled = true;    // Oyuncu hareketini tekrar aktif et
    }

    // Bir seçim butonunu ve metnini ayarlar
    private void SetChoice(Button button, TMP_Text text, DialogueChoice choice)
    {
        if (choice == null)
        {
            button.gameObject.SetActive(false); // Seçim yoksa butonu gizle
            return;
        }

        button.gameObject.SetActive(true);      // Butonu göster
        text.text = choice.choiceText;          // Seçim metnini yaz
        button.onClick.RemoveAllListeners();    // Önceki tıklama olaylarını temizle
        button.onClick.AddListener(() =>
        {
            dialogueText.text = choice.responseText; // Seçim yapıldığında cevabı göster
            HideChoices();                           // Seçenekleri gizle

            // Eğer yeni seçimler varsa, onları buffer'a al ve kısa bir süre sonra göster
            if (choice.nextChoices != null && choice.nextChoices.Length > 0)
            {
                nextChoicesBuffer = choice.nextChoices;
                Invoke(nameof(ShowNextChoices), 0.1f); // 0.5 saniye sonra yeni seçimleri göster
            }
        });
    }

    // Sonraki seçimleri ekrana getirir
    private void ShowNextChoices()
    {
        if (nextChoicesBuffer == null) return;

        // Her bir seçim için ilgili buton ve metni ayarla
        SetChoice(choiceButton1, choiceText1, nextChoicesBuffer.Length > 0 ? nextChoicesBuffer[0] : null);
        SetChoice(choiceButton2, choiceText2, nextChoicesBuffer.Length > 1 ? nextChoicesBuffer[1] : null);
        SetChoice(choiceButton3, choiceText3, nextChoicesBuffer.Length > 2 ? nextChoicesBuffer[2] : null);
        SetChoice(choiceButton4, choiceText4, nextChoicesBuffer.Length > 3 ? nextChoicesBuffer[3] : null);

        nextChoicesBuffer = null; // Buffer'ı temizle
    }

    // Tüm seçim butonlarını gizler
    private void HideChoices()
    {
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        choiceButton3.gameObject.SetActive(false);
        choiceButton4.gameObject.SetActive(false);
    }

    // İmleci kilitleyip gizler veya serbest bırakır
    private void LockCursor(bool locked)
    {
        Cursor.visible = !locked; // Kilitliyse görünmez, değilse görünür
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None; // Kilit durumunu ayarla
    }
}