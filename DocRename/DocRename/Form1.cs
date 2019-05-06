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
         * Argument:
         *  object     sender
         *  EventArgs  e
         * 
         * Return:
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
         * Argument:
         *  object     sender
         *  EventArgs  e
         * 
         * Return:
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
         * Argument:
         *  object     sender
         *  EventArgs  e
         * 
         * Return:
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
         * Argument:
         *  object     sender
         *  EventArgs  e
         * 
         * Return:
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
         * Argument:
         *  object     sender
         *  EventArgs  e
         * 
         * Return:
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
         * Argument:
         *  String   str        : The string that will be checked for illegal characters
         *  ComboBox combo_box  : The combo_box which the method will change its text color
         * 
         * Return:
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
         * Argument:
         *  String modified : modified filename (only the file name, no path prefix, no extensions)
         *  String original : original path (has full path and extensions are present)
         * 
         * Return:
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

        /* getFileExtension
         * 
         * Argument:
         *  String filename : The filename that is to be processed
         * 
         * Return:
         *  String          : The file extension that the filename has
         * 
         * Extracts the file extension from the filename to be processed
         * 
         */
        private String getFileExtension(String filename)
        {
            String[] all_tokens = filename.Split('.');
            return all_tokens[all_tokens.Length - 1];
        }

        /* fields_Update
         * 
         * Argument:
         *  String[] fields : { Project, Name, Version, Date }
         * 
         * Return:
         *  None
         * 
         * Update both the class's fields variable,
         * and the comboBox's and dateTimePicker's Text
         * 
         */
        private void fields_Update(String[] fields)
        {
            String date;

            // Update class variable
            this.fields = fields;

            // Update comboBox Text
            project_comboBox.Text = fields[0];
            name_comboBox.Text = fields[1];
            version_comboBox.Text = fields[2];

            // Convert 'MMDDYYYY' to 'MM/DD/YYYY 00:00 AM'
            date = ParserTool.Parser.date_FormatToDateTimePicker(fields[3]);

            // If date isn't null, then update dateTimePicker's Text
            if (date != null)
                date_dateTimePicker.Text = date;

            return;
        }

        /* fields_ConstructFullName
         * 
         * Argument:
         *  None
         * 
         * Return:
         *  None
         * 
         * Uses this.fields and constructs a full name,
         * the full name will be put into preview_textBox.Text
         * 
         */
        private void fields_ConstructFullName()
        {
            String output = "";

            // Iterate through each field
            for (int i = 0; i < 4; i++)
            {
                // Add the fields into output if fields is not ""
                if (this.fields[i] != "")
                {
                    // If output is currently "", dont add an underscore
                    if (output == "")
                        output = this.fields[i];
                    // If output isn't "", then add an underscore to seperate the fields
                    else
                        output = output + "_" + this.fields[i];
                }
            }

            // Update textBox
            preview_textBox.Text = output;
            return;
        }

        /* output_button_Click
         * 
         * Argument:
         *  object sender   :
         *  EventArgs e     :
         *  
         * Return:
         *  None
         * 
         */
        private void output_button_Click(object sender, EventArgs e)
        {
            String preview_filename = preview_textBox.Text;

            // Check if filename is valid
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

            // Save the fields into their respective comboBoxes
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

            // Gather data from the comboBoxes
            data[0] = project_comboBox.Items;
            data[1] = name_comboBox.Items;
            data[2] = version_comboBox.Items;

            // Save the information from data into the file
            MemoryManagementTool.MemoryTool.fields_SaveToFile(data);

            return;
        }
    }
}

/* ParserTool
 * 
 * Classes:
 *  Parser
 * 
 * Group of class(es) that is responsible for parsing a large string
 * (such as a filename path) and checking its sub-sections (tokens)
 * for any invalid characters or formatting errors.
 * 
 */
namespace ParserTool
{
    public class Parser
    {
        /* isTokenVersion
         * 
         * Argument:
         *  String token    : The String (usually the subsection of the filename) that is to be checked
         *  
         * Return:
         *  bool            : true if argument is the same format as 'version', else false
         * 
         * Returns true if argument has the format of 'vXXX...X'
         * where X is either a numeric character or '.'
         * 
         */
        private static bool isTokenVersion(String token)
        {
            // if the first character of the argument isn't 'v', fail immediately
            if (token[0] != 'v')
                return false;

            // cut off the first letter of the argument ('v')
            token = token.Substring(1);

            // Split the string by '.'s, which should only result in substrings with numeric characters
            String[] sub_tokens = token.Split('.');

            // for each substring, try to turn it into an integer with Int32.TryParse(...),
            // IF Int32.TryParse(...) fails, then return false (argument failed)
            foreach (String sub in sub_tokens)
                if (!Int32.TryParse(sub, out int num))
                    return false;

            // If passed through all the filters, then return true
            return true;
        }

        /* isTokenDate
         * 
         * Argument:
         *  String token    : The String (usually the subsection of the filename) that is to be tested
         * 
         * Return:
         *  bool            : true if format is that of a date, else false
         * 
         * Checks if the given argument is in the format of 'MMDDYYYY',
         * where each letter represents a numeric character
         * 
         */
        private static bool isTokenDate(String token)
        {
            // Immediately fail if...
            //  argument's string length is NOT of len('MMDDYYYY') = 8
            //  argument's string has a '+' or '-' sign in front of it
            if ((token.Length != 8) || ((token[0] == '+') || (token[0] == '-')))
                return false;

            // Return the value of Int32.TryParse(...), since correctly formatted dates
            // only have 'numeric' characters, then those that are correctly formatted
            // return true, and those that aren't return false
            return Int32.TryParse(token, out int num);
        }

        /* isTokenValid
         * 
         * Argument:
         *  String token    : The String (usually the subsection of the filename) that is to be tested
         *  
         * Return:
         *  bool            : true if argument is 'valid', else false
         *  
         * Checks if the argument is valid (doesn't contain illegal characters)
         * or not, and returns a boolean output reflecting on its findings
         * 
         */
        public static bool isTokenValid(String token)
        {
            // Get char array of illegal / invalid characters
            char[] illegal_chars = Path.GetInvalidFileNameChars();

            // For each illegal character, return false if the argument contains it
            foreach (char ic in illegal_chars)
                if (token.Contains(ic)) return false;

            // If argument doesn't contain any of those illegal characters, return true
            return true;
        }

        /* FileName_Parse
         * 
         * Argument:
         *  String filename     : filename that is to be parsed into its different subsections
         * 
         * Return:
         *  String[] output     : a string array containing all the relevant information from the argument
         * 
         * Parses the given argument and returns an output
         * in the format of { Project, Name, Version, Date }
         * 
         */
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

                // If token isn't valid or none, then ignore, as it cannot be used for any field
                if ((!isTokenValid(token)) || (token == ""))
                    continue;
                // If this is the last token out of all_tokens,
                // then take the first subtoken that is split by '.'
                // (used to take out file extensions)
                else if (index == 0)
                    tmp = token.Split('.')[0];
                else
                    tmp = token;

                // If token has the format of token date and the date field hasn't been filled,
                // put it in as part of output and change the flag gotDateField to true
                if (isTokenDate(tmp) && !gotDateField)
                {
                    output[3] = tmp;
                    gotDateField = true;
                }
                // If token has the format of version and version field hasn't been filled,
                // put it in as part of output and change the flag gotDateField to true
                else if (isTokenVersion(tmp) && !gotVersionField)
                {
                    output[2] = tmp;
                    gotVersionField = true;
                }
                // If gotProjectField hasn't already been filled,
                // then put it in output and change gotProjectField to true
                else if (!gotProjectField)
                {
                    output[0] = tmp;
                    gotProjectField = true;
                }
                // If getNameField hasn't already been filled,
                // then put it in output and change gotNameField to true
                else if (!gotNameField)
                {
                    output[1] = tmp;
                    gotNameField = true;
                }
                // If all fields have been filled and there are still more tokens,
                // break the loop pre-maturely since our first guess is good enough
                else
                {
                    break;
                }

            }

            return output;
        }

        /* date_SplitNum
         * 
         * Argument:
         *  String date     : the string that has month, day, and year undivided
         *  
         * Return:
         *  String[]        : the string array of the date divided into month, day, and year
         *  
         * Section off the argument from the format
         * of 'MMDDYYYY' to { "MM", "DD", "YYYY" }
         * 
         */
        private static String[] date_SplitNum(String date)
        {
            String[] output = new String[3];

            output[0] = date.Substring(0, 2);   // Section off the 'month' field
            output[1] = date.Substring(2, 2);   // Section off the 'day' field
            output[2] = date.Substring(4);      // Section off the 'year' field

            return output;
        }

        /* date_FormatToDateTimePicker
         * 
         * Arguments:
         *  String date     : 'MMDDYYYY'
         * 
         * Return:
         *  String          : 'MM/DD/YYYY 00:00 AM' or null if failed to parse
         * 
         * Converts the the format of the argument
         * from 'MMDDYYYY' to 'MM/DD/YYYY 00:00 AM'
         * 
         */
        public static String date_FormatToDateTimePicker(String date)
        {
            // If date is null or the date.Length is less than 8, return null
            if ((date == null) || (date.Length < 8))
                return null;

            String output = "";
            String[] all_tokens = date_SplitNum(date);

            // Both check if all_tokens[0] is a valid number assign its integer value to 'month'
            if (!Int32.TryParse(all_tokens[0], out int month))
                return null;
            // If month is NOT within the range of [1, 12], then show an error and return null
            if ((0 >= month) || (month > 12))
            {
                MessageBox.Show(String.Format("Error: Month '{0}' is out of the range [1 - 12], Please Try Again", month));
                return null;
            }
            // Both check if all_tokens[1] is a valid number assign its integer value to 'day'
            if (!Int32.TryParse(all_tokens[1], out int day))
                return null;
            // If month is NOT within the range of [1, 31], then show an error and return null
            if ((0 >= day) || (day > 31))
            {
                MessageBox.Show(String.Format("Error: Day '{0}' is out of the range [1 - 31], Please Try Again", day));
                return null;
            }
            // Both check if all_tokens[2] is a valid number assign its integer value to 'day'
            if (!Int32.TryParse(all_tokens[2], out int year))
                return null;
            // If year is NOT within the range of [1753, 9998], then show an error and return null
            if ((1753 > year) || (year > 9998))
            {
                MessageBox.Show(String.Format("Error: Year '{0}' is out of the range [1753 - 9998], Please Try Again", year));
                return null;
            }
            
            output = month.ToString() + '/' + day.ToString() + '/' + year.ToString() + " 00:00 AM";
            return output;
        }

        /* date_SplitSlash
         * 
         * Argument:
         *  String date     : 'MM/DD/YYYY XX:XX AM / PM'
         * 
         * Return:
         *  String[]        : { 'MM', 'DD', 'YYYY' }
         *  
         *  Converts the format of the argument from
         *  'MM/DD/YYYY XX:XX AM / PM' to { 'MM', 'DD', 'YYYY' }
         * 
         */
        private static String[] date_SplitSlash(String date)
        {
            String intermediate_value = date.Split(' ')[0];
            return intermediate_value.Split('/');
        }

        /* date_FormatToNumericalValue
         * 
         * Argument:
         *  String date     : 'MM/DD/YYYY XX:XX AM / PM'
         * 
         * Return:
         *  String output   : 'MMDDYYYY'
         *  
         *  Converts the format of the argument from
         *  'MM/DD/YYYY XX:XX AM / PM' to 'MMDDYYYY'
         * 
         */
        public static String date_FormatToNumericalValue(String date)
        {
            String[] all_tokens = date_SplitSlash(date);
            String tmp = "";
            String output = "";

            // For each 'tmp' from 'all_tokens', if the string length is less than 2,
            // then add "0" to the prefix of 'tmp' and append the processed string to 'output'
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

/* MemoryManagementTool
 * 
 * Classes:
 *  MemoryTool
 * 
 * Group of class(es) that is responsible for
 * saving and loading data from a designated text file
 * 
 */
namespace MemoryManagementTool
{
    class MemoryTool
    {
        /* getSaveFileDirectory
         * 
         * Argument:
         *  None
         * 
         * Return:
         *  String output   : The folder path to where the text file (save file) is supposed to be
         * 
         */
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

        /* convert_ObjectCollectionToText
         * 
         * Argument:
         *  ComboBox.ObjectCollection[] data:
         *      An array of ObjectCollection with the format of { Project, Name, Version }
         * 
         * Return:
         *  String[]:
         *      An array of strings that each entry is an individual line
         */
        private static String[] convert_ObjectCollectionToText(ComboBox.ObjectCollection[] data)
        {
            List<String> list = new List<String>();

            ComboBox.ObjectCollection d = null;

            String[] categories = new String[] { "</Project>", "</Name>", "</Version>" };

            /*
             example of what the for loops construct
             list[  0  ] = "</Project>"
             list[  1  ] = "Project Names"
              ...
             list[n - 1] = "<//>"
             list[  n  ] = "</Name>"
             list[n + 1] = "File Names"
              ...
             list[v - 1] = "<//>"
             list[  v  ] = "</Version>"
             list[v + 1] = "Version Names"
              ...
             list[  x  ] = "<//>"
             */
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

        /* fields_SaveToFile
         * 
         * Argument:
         *  ComboBox.ObjectCollection[] data:
         *      Array of ObjectCollections in the format of { Project, Name, Version }
         * 
         * Return:
         *  None
         * 
         * Converts the array of object collections into a string array,
         * which then is used to write into the save file
         * 
         */
        public static void fields_SaveToFile(ComboBox.ObjectCollection[] data)
        {
            // Construct file path to memory.txt
            String file_dir = getSaveFileDirectory();
            file_dir = file_dir + '\\' + "memory.txt";

            try
            {
                // Open the file path to write into it
                StreamWriter sw = new StreamWriter(file_dir);

                // Turn the data into a String array
                String[] saved_text = convert_ObjectCollectionToText(data);

                // Write each entry of the string array as a new line into the file
                foreach (String text in saved_text)
                    sw.WriteLine(text);

                // Finish and terminate the process to write into the file path
                sw.Close();
            }
            catch (Exception e)
            {
                // If there is an exception caught, display an error
                MessageBox.Show("Exception: " + e.Message);
            }
            return;
        }

        /* fields_LoadFromFile
         * 
         * Argument:
         *  None
         * 
         * Return:
         *  List<List<String>> list: 
         *      A list of lists that has the format of { project_name_list, file_name_list, version_list }
         * 
         * Read from the file path of memory.txt, parse its inputs, and return with
         * three lists of strings denoting Projects, Name, and Version
         * 
         */
        public static List<List<String>> fields_LoadFromFile()
        {
            List<List<String>> list = new List<List<String>>();

            String file_dir;
            String line;

            int obj_index = 0;

            // Add 3 lists into the central list
            list.Add(new List<String>());
            list.Add(new List<String>());
            list.Add(new List<String>());

            // Construct file path
            file_dir = getSaveFileDirectory();
            file_dir = file_dir + '\\' + "memory.txt";

            try
            {
                // Open the file and begin reading from the save file
                StreamReader sr = new StreamReader(file_dir);

                // Read the first line
                line = sr.ReadLine();

                // Read the file line by line until there is no more to be read
                while (line != null)
                {
                    if (line == "</Project>")
                        obj_index = 0;
                    else if (line == "</Name>")
                        obj_index = 1;
                    else if (line == "</Version>")
                        obj_index = 2;
                    else if (line != "<//>")
                        // If line isn't "<//>" add it into list[ obj_index ]
                        list[obj_index].Add(line);

                    line = sr.ReadLine();
                }

                // Terminate and close file
                sr.Close();

            }
            catch (Exception e)
            {
                // Display error message
                MessageBox.Show("Exception: " + e.Message);
            }

            return list;
        }

    }
}


