using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1121538_徐霈綺_TSV
{
    public partial class frmWordCards : Form
    {
        WordCollection _WordList = new WordCollection();
        dynamic wmp;
        bool isPlay = false;
        string strWordFile = "WordCards.txt";

        public frmWordCards()
        {
            InitializeComponent();
            try
            {
                Type wmpType = Type.GetTypeFromProgID("WMPlayer.OCX");
                if (wmpType != null)
                {
                    wmp = Activator.CreateInstance(wmpType);
                }
            }
            catch { }
        }

        private void frmWordCards_Load(object sender, EventArgs e)
        {
            tsslMessage.Text = "請開啟檔案";
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files|*.txt|TSV Files|*.tsv|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    strWordFile = ofd.FileName;
                    LoadFile();
                }
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(strWordFile);
                _WordList.LoadFromStringArray(lines);

                lstWordList.Items.Clear();
                foreach (var word in _WordList)
                {
                    lstWordList.Items.Add(word);
                }

                tsslMessage.Text = $"單字總數: {_WordList.Count}";

                if (lstWordList.Items.Count > 0)
                {
                    lstWordList.SelectedIndex = 0;
                    PlaySelectedWord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取檔案失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowWord(WordItem word)
        {
            if (word == null) return;
            txtWord.Text = word.Word;
            txtPhonogram.Text = word.Phonogram;
            txtExplain.Text = word.Explain;
        }

        private void PlayWord(WordItem word)
        {
            if (word == null) return;
            if (File.Exists(word.SoundPath))
            {
                if (wmp != null)
                {
                    wmp.URL = word.SoundPath;
                    wmp.controls.play();
                }
            }
            else
            {
                tsslMessage.Text = $"找不到音效檔: {word.SoundPath}";
            }
        }

        private void PlaySelectedWord()
        {
            if (lstWordList.SelectedItem is WordItem word)
            {
                ShowWord(word);
                PlayWord(word);
            }
        }

        private void NextWordList()
        {
            if (lstWordList.Items.Count == 0) return;

            int index = lstWordList.SelectedIndex + 1;
            if (index >= lstWordList.Items.Count)
            {
                index = 0;
            }
            lstWordList.SelectedIndex = index;
            // 控制清單捲動讓選項盡量保持在中間
            int visibleItems = lstWordList.ClientSize.Height / lstWordList.ItemHeight;
            int topIndex = Math.Max(0, index - (visibleItems / 2));
            lstWordList.TopIndex = topIndex;
        }

        private void lstWordList_Click(object sender, EventArgs e)
        {
            if (isPlay)
            {
                isPlay = false;
                timPlayer.Stop();
                btnAutoPlay.Text = "Play";
            }
            PlaySelectedWord();
        }

        private void lstWordList_DoubleClick(object sender, EventArgs e)
        {
            if (lstWordList.SelectedItem is WordItem word)
            {
                using (var frm = new frmEditWord(word))
                {
                    if (frm.ShowDialog() == DialogResult.Yes)
                    {
                        PlaySelectedWord();
                        _WordList.SaveToFile(strWordFile);
                        // Refresh display if needed
                        lstWordList.DisplayMember = "";
                        lstWordList.DisplayMember = "Word";
                    }
                }
            }
        }

        private void timPlayer_Tick(object sender, EventArgs e)
        {
            NextWordList();
            PlaySelectedWord();
        }

        private void btnAutoPlay_Click(object sender, EventArgs e)
        {
            isPlay = !isPlay;
            if (isPlay)
            {
                timPlayer.Start();
                btnAutoPlay.Text = "Stop";
                PlaySelectedWord();
            }
            else
            {
                timPlayer.Stop();
                btnAutoPlay.Text = "Play";
            }
        }

        private void frmWordCards_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                NextWordList();
                PlaySelectedWord();
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Space)
            {
                PlaySelectedWord();
                e.Handled = true;
            }
        }
    }
}
