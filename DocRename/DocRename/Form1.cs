using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocRename
{
    public partial class docRenameForm : Form
    {
        public docRenameForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x_value = (Width / 2) - (title_label.Width / 2) - 3;
            title_label.Location = new Point(x_value, 10);

            return;
        }

        private void input_button_Click(object sender, EventArgs e)
        {
            String filename = "";

            // Open dialog box to get file path
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String full_filename = openFileDialog.FileName;
                // String simp_filename = openFileDialog.SafeFileName;
                input_textBox.Text = openFileDialog.FileName;
            }

            String[] all_tokens = input_textBox.Text.Split('\\');

            filename = all_tokens[all_tokens.Length - 1];

            parse_filename(filename);


            return;
        }

        // Not used yet
        private static bool isTokenVersion(String token)
        {
            if (token[0] != 'v')
                return false;

            token = token.Substring(1);

            String[] sub_tokens = token.Split('.');

            foreach (String sub in sub_tokens)
                if (!Int32.TryParse(sub, out int num))
                    return false;

            return true;
        }
        // Not used yet
        private static bool isTokenDate(String token)
        {
            if ((token.Length != 8) || ((token[0] == '+') || (token[0] == '-')))
                return false;

            return Int32.TryParse(token, out int num);
        }
        // Not used yet
        private static bool isTokenValid(String token)
        {
            char[] illegal_chars = Path.GetInvalidFileNameChars();

            foreach (char ic in illegal_chars)
                if (token.Contains(ic)) return false;

            return true;
        }

        private bool parse_filename(String filename)
        {
            ComboBox combo_box = null;

            String[] all_tokens = filename.Split('_');

            if (all_tokens.Length != 4)
                return false;

            int index = 0;
            foreach (String token in all_tokens)
            {
                switch (index)
                {
                    case 0:
                        combo_box = project_comboBox;
                        break;
                    case 1:
                        combo_box = name_comboBox;
                        break;
                    case 2:
                        combo_box = version_comboBox;
                        break;
                    case 3:
                        combo_box = date_comboBox;
                        break;
                    default:
                        combo_box = null;
                        break;
                }

                if (combo_box == null)
                    break;
                else if (index == all_tokens.Length - 1)
                    combo_box.Text = token.Split('.')[0];
                else
                    combo_box.Text = token;

                index++;
            }


            return true;
        }
    }
}
/*
namespace ComboBoxManager
{
    public class ComboBoxManager
    {
        public ComboBox combo_box = null;
        // public ComboBox.ObjectCollection combo_items = null;

        public void Main(ComboBox box)
        {
            this.combo_box = box;
            // this.combo_items = box.Items;
        }

        private ComboBox.ObjectCollection getBoxItems()
        {
            return this.combo_box.Items;
        }

        private void addBoxItems(String item)
        {

            return;
        }

        
    }
}
*/
