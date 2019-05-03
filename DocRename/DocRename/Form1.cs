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

        String[] fields = new String[] { "", "", "", "" };

        public docRenameForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get title_label to be centered to the window as best as possible
            int x_value = (Width / 2) - (title_label.Width / 2) - 3;
            title_label.Location = new Point(x_value, 10);

            // Load information from memory.txt from within the project into the items of the combobox
            List<List<String>> data = MemoryManagementTool.MemoryTool.fields_LoadFromFile();
            List<ComboBox> combo_box = new List<ComboBox> { project_comboBox, name_comboBox, version_comboBox };
            List<String> field;

            for (int i = 0; i < 3; i++)
            {
                field = data[i];
                for (int j = 0; j < field.Count; j++)
                {
                    combo_box[i].Items.Add(data[i][j]);
                }
            }


            return;
        }

        private void input_button_Click(object sender, EventArgs e)
        {
            String filename = "";

            // Open dialog box to get file path
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                input_textBox.Text = openFileDialog.FileName;

            String[] all_tokens = input_textBox.Text.Split('\\');

            filename = all_tokens[all_tokens.Length - 1];

            String[] result = ParserTool.Parser.FileName_Parse(filename);

            this.fields_Update(result);

            return;
        }

        private void output_button_Click(object sender, EventArgs e)
        {
            String filename = preview_textBox.Text;
            String[] result = ParserTool.Parser.FileName_Parse(filename);

            this.fields_Update(result);
        }

        private void project_comboBox_TextChanged(object sender, EventArgs e)
        {
            this.fields[0] = project_comboBox.Text;

            this.fields_ConstructFullName();
            return;
        }

        private void name_comboBox_TextChanged(object sender, EventArgs e)
        {
            this.fields[1] = name_comboBox.Text;

            this.fields_ConstructFullName();
            return;
        }

        private void version_comboBox_TextChanged(object sender, EventArgs e)
        {
            this.fields[2] = version_comboBox.Text;

            this.fields_ConstructFullName();
            return;
        }

        private void date_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            String tmp = date_dateTimePicker.Value.ToString();
            this.fields[3] = ParserTool.Parser.date_FormatToNumericalValue(tmp);

            this.fields_ConstructFullName();
            return;
        }

        private void fields_Update(String[] fields)
        {
            this.fields = fields;

            project_comboBox.Text = fields[0];
            name_comboBox.Text = fields[1];
            version_comboBox.Text = fields[2];

            String date = ParserTool.Parser.date_FormatToDateTimePicker(fields[3]);
            if (date != null)
                date_dateTimePicker.Text = date;

            return;
        }

        private void fields_ConstructFullName()
        {
            String output = "";
            for (int i = 0; i < 4; i++)
            {
                if (this.fields[i] != "")
                {
                    if (output == "")
                        output = this.fields[i];
                    else
                        output = output + "_" + this.fields[i];
                }
            }

            preview_textBox.Text = output;
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComboBox.ObjectCollection[] data = new ComboBox.ObjectCollection[3];
            data[0] = project_comboBox.Items;
            data[1] = name_comboBox.Items;
            data[2] = version_comboBox.Items;

            MemoryManagementTool.MemoryTool.fields_SaveToFile(data);
        }
    }
}

namespace ParserTool
{
    public class Parser
    {
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

        private static bool isTokenDate(String token)
        {
            if ((token.Length != 8) || ((token[0] == '+') || (token[0] == '-')))
                return false;

            return Int32.TryParse(token, out int num);
        }

        private static bool isTokenValid(String token)
        {
            char[] illegal_chars = Path.GetInvalidFileNameChars();

            foreach (char ic in illegal_chars)
                if (token.Contains(ic)) return false;

            return true;
        }

        public static String[] FileName_Parse(String filename)
        {
            bool gotProjectField = false;
            bool gotNameField = false;
            bool gotVersionField = false;
            bool gotDateField = false;

            String[] all_tokens = filename.Split('_');
            String[] output = new String[] { "", "", "", "" };

            String tmp = "";

            int index = all_tokens.Length;

            foreach (String token in all_tokens)
            {
                index--;

                // Conditions to ignore tokens and exit for loop
                if ((!isTokenValid(token)) || (token == ""))
                    continue;
                else if (index == 0)
                    tmp = token.Split('.')[0];
                else
                    tmp = token;

                // Check
                if (isTokenDate(tmp) && !gotDateField)
                {
                    output[3] = tmp;
                    gotDateField = true;
                }
                else if (isTokenVersion(tmp) && !gotVersionField)
                {
                    output[2] = tmp;
                    gotVersionField = true;
                }
                else if (!gotProjectField)
                {
                    output[0] = tmp;
                    gotProjectField = true;
                }
                else if (!gotNameField)
                {
                    output[1] = tmp;
                    gotNameField = true;
                }
                else
                {
                    break;
                }

            }

            return output;
        }

        private static String[] date_SplitNum(String date)
        {
            String[] output = new String[3];

            output[0] = date.Substring(0, 2);   // Section off the 'month' field
            output[1] = date.Substring(2, 2);   // Section off the 'day' field
            output[2] = date.Substring(4);      // Section off the 'year' field

            return output;
        }

        public static String date_FormatToDateTimePicker(String date)
        {
            if ((date == null) || (date.Length < 8))
                return null;

            String[] all_tokens = date_SplitNum(date);

            if (!Int32.TryParse(all_tokens[0], out int month))
                return null;

            if ((0 >= month) || (month > 12))
            {
                MessageBox.Show(String.Format("Error: Month '{0}' is out of the range [1 - 12], Please Try Again", month));
                return null;
            }

            if (!Int32.TryParse(all_tokens[1], out int day))
                return null;

            if ((0 >= day) || (day > 31))
            {
                MessageBox.Show(String.Format("Error: Day '{0}' is out of the range [1 - 31], Please Try Again", day));
                return null;
            }

            if (!Int32.TryParse(all_tokens[2], out int year))
                return null;

            if ((1753 > year) || (year > 9998))
            {
                MessageBox.Show(String.Format("Error: Year '{0}' is out of the range [1753 - 9998], Please Try Again", year));
                return null;
            }

            return month.ToString() + '/' + day.ToString() + '/' + year.ToString() + " 00:00 AM";
        }

        private static String[] date_SplitSlash(String date)
        {
            String intermediate_value = date.Split(' ')[0];
            return intermediate_value.Split('/');
        }

        public static String date_FormatToNumericalValue(String date)
        {
            String[] all_tokens = date_SplitSlash(date);
            String tmp = "";
            String output = "";

            for (int i = 0; i < 3; i++)
            {
                tmp = all_tokens[i];
                if ((i < 2) && (tmp.Length < 2))
                    tmp = "0" + tmp;

                output = output + tmp;
            }

            return output;
        }
    }
}

namespace MemoryManagementTool
{
    class MemoryTool
    {
        private static String getSaveFileDirectory()
        {
            String[] all_tokens = Directory.GetCurrentDirectory().Split('\\');

            String output = "";

            int max = all_tokens.Length - 4;

            for (int i = 0; i < all_tokens.Length; i++)
            {
                output = output + all_tokens[i];
                if (i == max) break;
                output = output + '\\';
            }

            return output;
        }

        private static String[] convert_ObjectCollectionToText(ComboBox.ObjectCollection[] data)
        {
            List<String> list = new List<String>();

            ComboBox.ObjectCollection d = null;

            String[] categories = new String[] { "</Project>", "</Name>", "</Version>" };

            for (int i = 0; i < 3; i++)
            {
                list.Add(categories[i]);

                d = data[i];
                for (int j = 0; j < d.Count; j++)
                {
                    list.Add(d[j].ToString());
                }

                list.Add("<//>");
            }

            return list.ToArray();
        }

        public static void fields_SaveToFile(ComboBox.ObjectCollection[] data)
        {
            String file_dir = getSaveFileDirectory();
            file_dir = file_dir + '\\' + "memory.txt";

            try
            {
                StreamWriter sw = new StreamWriter(file_dir);

                String[] saved_text = convert_ObjectCollectionToText(data);

                foreach (String text in saved_text)
                    sw.WriteLine(text);

                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            return;
        }

        public static List<List<String>> fields_LoadFromFile()
        {
            List<List<String>> list = new List<List<String>>();

            String file_dir = getSaveFileDirectory();
            String line;

            int obj_index = 0;

            list.Add(new List<String>());
            list.Add(new List<String>());
            list.Add(new List<String>());

            file_dir = file_dir + '\\' + "memory.txt";

            try
            {
                StreamReader sr = new StreamReader(file_dir);

                line = sr.ReadLine();

                while (line != null)
                {
                    if (line == "</Project>")
                        obj_index = 0;
                    else if (line == "</Name>")
                        obj_index = 1;
                    else if (line == "</Version>")
                        obj_index = 2;
                    else if (line != "<//>")
                        list[obj_index].Add(line);

                    line = sr.ReadLine();
                }

                sr.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }


            return list;
        }

    }
}


