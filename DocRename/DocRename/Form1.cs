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

            // Initialize info_label
            info_label.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            info_label.Text = "";

            // Load information from memory.txt from within the project into the items of the combobox
            List<List<String>> data = MemoryManagementTool.MemoryTool.fields_LoadFromFile();
            List<ComboBox> combo_box = new List<ComboBox> { project_comboBox, name_comboBox, version_comboBox };
            List<String> field;

            // Put the information from data into the combo_box items
            /* "For each combo box, get data[ x ] (where x is the
             * index corresponding the the combo box) 
             * ( e.g. 0 -> project, 1 -> name, 2 -> version )
             * and add all the strings stored in data[ x ] into
             * the items of combo box.
             */
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

        /* input_button_Click
         * 
         * inputs:
         *  object     sender
         *  EventArgs  e
         * 
         * returns:
         *  None
         * 
         * Opens a Dialog box to retrieve the selected file path,
         * put the full path into input_textBox
         * parse the file name of the selected path into its respective fields
         * (e.g. Project, Name, Version, Date)
         * if possible.
         * 
         */
        private void input_button_Click(object sender, EventArgs e)
        {
            String filename = "";

            // Open dialog box to get file path
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                input_textBox.Text = openFileDialog.FileName;

            // Extract the filename from the path by splitting the string by '\'
            // and keeping the last section (which is the file name itself)
            String[] all_tokens = input_textBox.Text.Split('\\');
            filename = all_tokens[all_tokens.Length - 1];

            // Use the custom made ParserTool to convert the filename into an array of 4 strings
            // If a given string in the array is "", it is most likely because either the parser
            // can't fit a filename's section to that field, or the name did not yield enough fields 
            String[] fields = ParserTool.Parser.FileName_Parse(filename);

            // Update the 'global' fields variable in the class itself 
            this.fields_Update(fields);

            return;
        }

        /* project_comboBox_TextChanged
         * 
         * inputs:
         *  object     sender
         *  EventArgs  e
         * 
         * returns:
         *  None
         * 
         * Whenever the comboBox detects a change, it will call
         * this.illegal_character_Check(...) to check if the
         * text it contains is valid,
         *      IF the text is valid...
         *      THEN it will update its corresponding field to this.fields[ x ]
         *          AND update preview_textBox by calling this.fields_ConstructFullName();
         *      IF the text is invalid...
         *      THEN it will return and not perform any changes to fields and preview_textBox
         * 
         */
        private void project_comboBox_TextChanged(object sender, EventArgs e)
        {
            String project_text = project_comboBox.Text;

            if (!this.illegal_character_Check(project_text, project_comboBox))
                return;

            this.fields[0] = project_text;

            this.fields_ConstructFullName();
            return;
        }

        /* name_comboBox_TextChanged
         * 
         * inputs:
         *  object     sender
         *  EventArgs  e
         * 
         * returns:
         *  None
         * 
         * Whenever the comboBox detects a change, it will call
         * this.illegal_character_Check(...) to check if the
         * text it contains is valid,
         *      IF the text is valid...
         *      THEN it will update its corresponding field to this.fields[ x ]
         *          AND update preview_textBox by calling this.fields_ConstructFullName();
         *      IF the text is invalid...
         *      THEN it will return and not perform any changes to fields and preview_textBox
         * 
         */
        private void name_comboBox_TextChanged(object sender, EventArgs e)
        {
            String name_text = name_comboBox.Text;

            if (!this.illegal_character_Check(name_text, name_comboBox))
                return;

            this.fields[1] = name_text;

            this.fields_ConstructFullName();
            return;
        }

        /* name_comboBox_TextChanged
         * 
         * inputs:
         *  object     sender
         *  EventArgs  e
         * 
         * returns:
         *  None
         * 
         * Whenever the comboBox detects a change, it will call
         * this.illegal_character_Check(...) to check if the
         * text it contains is valid,
         *      IF the text is valid...
         *      THEN it will update its corresponding field to this.fields[ x ]
         *          AND update preview_textBox by calling this.fields_ConstructFullName();
         *      IF the text is invalid...
         *      THEN it will return and not perform any changes to fields and preview_textBox
         * 
         */
        private void version_comboBox_TextChanged(object sender, EventArgs e)
        {
            String version_text = version_comboBox.Text;

            if (!this.illegal_character_Check(version_text, version_comboBox))
                return;

            this.fields[2] = version_text;

            this.fields_ConstructFullName();
            return;
        }


        /* name_comboBox_TextChanged
         * 
         * inputs:
         *  object     sender
         *  EventArgs  e
         * 
         * returns:
         *  None
         * 
         * Whenever the dateTimePicker detects user interaction
         * (clicking on the calender icon or changing the date)
         * it will convert its value to string and convert it from
         * "MM/DD/YYYY HH:MM AM/PM" to "MMDDYYYY"
         * it uses the new formatted string to be put into its respective field
         * and updates preview_textBox by calling this.fields_ConstructFullName();
         * 
         */
        private void date_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            String tmp = date_dateTimePicker.Value.ToString();
            this.fields[3] = ParserTool.Parser.date_FormatToNumericalValue(tmp);

            this.fields_ConstructFullName();

            return;
        }

        /* illegal_character_Check
         * 
         * input:
         *  String   str        : The string that will be checked for illegal characters
         *  ComboBox combo_box  : The combo_box which the method will change its text color
         * 
         * return:
         *  bool     output     : IF true THEN no illegal characters (valid) ELSE illegal characters present (invalid)
         * 
         * Determines if the string is valid (no illegal characters present) or not,
         * also changes the field's combo_box and info_label to reflect the changes.
         * The changes to the objects in the windows is to help correct the user's mistakes
         * 
         */
        private bool illegal_character_Check(String str, ComboBox combo_box)
        {
            // Begin by initializing the variables so that the result is 
            // of the outcome: "string valid", so...
            // combo_box color is Black, info_label is hidden, and output = true
            String color = "#000000"; // Black
            String text = "";
            bool output = true;

            if (!ParserTool.Parser.isTokenValid(str))
            {
                // If string is not valid, then change the variables to where
                // combo_box color is Red, info_label warns user, and output = false
                color = "#FF0000"; // Red
                text = "Field contains illegal character(s), please remove and try again";
                output = false;
            }

            // Commit changes from variables to the objects in the window
            combo_box.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
            info_label.Text = text;

            return output;
        }

        /* modified_name_ConstructFullPath
         * 
         * input:
         *  String modified : modified filename (only the file name, no path prefix, no extensions)
         *  String original : original path (has full path and extensions are present)
         * 
         * return:
         *  modified_full_path  : modified file name with original's path prefix and file extension
         * 
         * Uses 'original'
         * 
         */
        private String modified_name_ConstructFullPath(String modified, String original)
        {
            String[] all_tokens = original.Split('\\');

            String file_extension = getFileExtension(all_tokens[all_tokens.Length - 1]);

            String modified_full_path = "";

            for (int i = 0; i < all_tokens.Length - 1; i++)
                modified_full_path = modified_full_path + all_tokens[i] + "\\";

            modified_full_path = modified_full_path + modified + "." + file_extension;

            return modified_full_path;
        }

        private String getFileExtension(String filename)
        {
            String[] all_tokens = filename.Split('.');
            return all_tokens[all_tokens.Length - 1];
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

        private void output_button_Click(object sender, EventArgs e)
        {
            String preview_filename = preview_textBox.Text;

            if (!ParserTool.Parser.isTokenValid(preview_filename))
            {
                String invalid_chars = "{ \", <, >, |, \\b, \\0, \\t }";

                MessageBox.Show(String.Format(
                    "Warning: New file name invalid from illegal characters {0}, please try again", invalid_chars));
                return;
            }

            String original_dir = input_textBox.Text;
            String modified_dir = this.modified_name_ConstructFullPath(preview_textBox.Text, original_dir);

            // Check if the original file exits before copying
            if (!File.Exists(original_dir))
            {
                MessageBox.Show("Warning: Selected path to file to be renamed does not exist, please try again and check for typos");
                return;
            }


            try
            {
                // Attempt to rename the file
                File.Move(original_dir, modified_dir);
            }
            catch (Exception ex)
            {
                // Prompt user with an exception and abort the process
                MessageBox.Show("Exception: " + ex.Message);
                return;
            }

            // Save the fields into the
            String[] preview_tokens = preview_textBox.Text.Split('_');
            if ((this.fields[0] == preview_tokens[0]) && (!project_comboBox.Items.Contains(this.fields[0])))
            {
                project_comboBox.Items.Add(this.fields[0]);
            }
            if (this.fields[1] == preview_tokens[1] && (!name_comboBox.Items.Contains(this.fields[1])))
            {
                name_comboBox.Items.Add(this.fields[1]);
            }
            if (this.fields[2] == preview_tokens[2] && (!version_comboBox.Items.Contains(this.fields[2])))
            {
                version_comboBox.Items.Add(this.fields[2]);
            }

            ComboBox.ObjectCollection[] data = new ComboBox.ObjectCollection[3];

            data[0] = project_comboBox.Items;
            data[1] = name_comboBox.Items;
            data[2] = version_comboBox.Items;

            MemoryManagementTool.MemoryTool.fields_SaveToFile(data);

            return;
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

        public static bool isTokenValid(String token)
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


