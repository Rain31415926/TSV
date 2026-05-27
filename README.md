# 1121538_徐霈綺_TSV (單字卡應用程式)

這是一個使用 C# 與 Windows Forms (.NET Framework 4.7.2) 撰寫的單字卡小程式。它可以讀取 TSV 或 TXT 格式的字彙檔，並提供自動輪讀、發音播放以及編輯單字的功能。

## 功能特色 (Features)

*   **字彙資料載入**：支援透過「檔案 -> 開啟(O)」(tsmiOpen) 對話框，載入本機端的 `.txt` 或 `.tsv` 單字檔。
*   **字卡顯示區**：將單字（藍色、粗體字）、音標（綠色字）與解釋完整顯示於畫面右方中心面板。
*   **單字管理與預覽**：畫面左側為單字清單，可直接點選切換該單字解釋，並自動撥放單字讀音。
*   **播放控制**：
    *   **手動操作**：按 `Enter` 鍵跳至下一個單字並播放，按 `Space` 鍵重新播放當前單字發音。
    *   **讀音 (Read)**：點擊 `Read` 按鈕，重播當前選擇單字的讀音且不進行跳轉。
    *   **自動輪讀 (Auto Play)**：透過「自動輪讀」按鈕，可啟動計時器 (每隔2秒) 來自動跳轉並播放下一個單字的讀音 (再次點擊可停止輪讀)。
*   **發音支援**：整合了 `WindowsMediaPlayer` (WMPlayer.OCX) 元件，支援以相對路徑播放單字資料中配對的音效檔案。
*   **單字編輯**：對左側清單中的單字連按滑鼠左鍵兩下，將開啟編輯視窗 `frmEditWord`，更新內容後將存檔覆蓋回原本的詞彙檔案。
*   **程式資訊 (About)**：選單包含 `幫助 -> About`，會彈出 `frmAbout` 檢視本單字卡的開發者與版本等產品說明。

## 資料格式需求 (Data File Format)

被讀取的 `.txt` 或 `.tsv` 單字檔案中，每一行代表一個單字，欄位必須使用 **Tab (`\t`)** 鍵分隔，並且編碼至少要相容 ANSI (Big5) 或 UTF-8。資料欄位依序如下（超過四個欄位的擴充解釋，會透過合併成換行儲存於解釋中）：

1.  **Word (單字)**
2.  **Phonogram (音標)**
3.  **SoundPath (音效檔相對路徑)**
4.  **Explain (解釋)**

## 使用畫面展示
*   <img width="747" height="454" alt="image" src="https://github.com/user-attachments/assets/2e57f205-778d-4865-be63-d5270ea45060" />

*   <img width="745" height="453" alt="image" src="https://github.com/user-attachments/assets/3a43e7c6-607e-4be2-a032-00a102e0afb9" />

*  <img width="541" height="342" alt="image" src="https://github.com/user-attachments/assets/4794e14d-b219-4ba7-b486-d9d5efba953e" />


