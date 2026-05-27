using System;
using System.Windows.Forms;

namespace _1121538_徐霈綺_TSV
{
    public partial class frmEditWord : Form
    {
        public WordItem Word { get; set; }

        public frmEditWord(WordItem wordItem)
        {
            InitializeComponent();
            Word = wordItem;

            if (Word != null)
            {
                txtWord.Text = Word.Word;
                txtPhonogram.Text = Word.Phonogram;
                txtSoundPath.Text = Word.SoundPath;
                txtExplain.Text = Word.Explain;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Word != null)
            {
                Word.Word = txtWord.Text;
                Word.Phonogram = txtPhonogram.Text;
                Word.SoundPath = txtSoundPath.Text;
                Word.Explain = txtExplain.Text;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}