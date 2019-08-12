using System;
using System.Windows.Forms;
using iMacros.Component;
using iMacros.Component.EventHelpers;
using System.Security.Permissions;
using System.Text;

namespace TestComponent_cs
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            string licenseKey = @"CMPNTJJH3W";
            InitializeComponent();
            iim = iMacrosControl.Create(licenseKey);
            iim.Dock = DockStyle.Fill;
            // Now that the iMacros browser control has been initialized, we can attach to the browser events.
            iim.BrowserStatusUpdated += iim_BrowserStatusUpdated;
            browserPane.Controls.Add(iim);
        }

        private iMacrosControl iim;

        #region iMacros specific event handlers

        private void iim_MasterPasswordRequested(object sender, MasterPasswordEventArgs e)
        {
            // Handle the MasterPasswordRequested event, when iMacros needs a TEMPKEY do decrypt a temporary password

            // Set the master password 
            e.MasterPassword = "other";
        }

        private void iim_BrowserStatusUpdated(object sender, EventArgs e)
        {
            // Handle the BrowserStatusUpdated event by displaying the BrowserStatus in a text box 
            // when it is updated (most common to see in the status bar)
            browserBox.Text = iim.BrowserStatus;
        }

        private void iim_PlayerStatusUpdated(object sender, EventArgs e)
        {
            // Handle the PlayerStatusUpdated event by displaying the updated PlayerStatus (macro, command and error messages)
            playerBox.Text = iim.PlayerStatus;
        }

        private void iim_iMacrosError(object sender, EventArgs e)
        {
            // Handle the iMacrosError event by displaying  the ErrorCode and ErrorText in a text box.
            errorBox.Text = String.Format("Error {0} occurred: {1}", iim.ErrorCode, iim.ErrorText);
        }

        private void iim_BrowserResize(object sender, BrowserResizeEventArgs e)
        {
            // Handle the BrowserResize event when iMacros Player requests the browser to be resized (due to a SIZE command, for instance)
            var oldSize = iim.Size;
            if (oldSize != e.Size)
            {
                Width += e.Size.Width - oldSize.Width;
                Height += e.Size.Height - oldSize.Height;
            }
            Application.DoEvents();
        }

        private void iim_Prompt(object sender, PromptEventArgs e)
        {
            // If the PROMPT command does not require any user input, a simple MessageBox can be used to communicate with the user.
            if (!e.NeedsInput)
                MessageBox.Show(e.Message);
            else
            {
                // Otherwise it is necessary to implement a modal dialog to obtain user input. 
                // Here we use the MessageBox as a simple alternative, which only provides us with very limited user feedback.
                var result = MessageBox.Show(e.Message, "TestComponent", MessageBoxButtons.YesNo);
                e.Value = result.ToString();
                e.Handled = true;
            }
        }

        #endregion

        #region Other event handlers

        private void mainForm_Load(object sender, EventArgs e)
        {
            ChangeEmulationMode();
        }

        private void play_Click(object sender, EventArgs e)
        {
            errorBox.Clear();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                // Play the macro file
                PlayMacro(openFile.FileName); // The macro could also have been ofuscated using iMacros Enterprise Edition
        }

        private void fillform_Click(object sender, EventArgs e)
        {
            errorBox.Clear();
            // The C# string for an existing macro can be easily generated in the iMacros editor
            StringBuilder macro = new StringBuilder();
            macro.AppendLine(@"VERSION BUILD=11.5.497.9113");
            macro.AppendLine(@"TAB T=1");
            macro.AppendLine(@"TAB CLOSEALLOTHERS");
            macro.AppendLine(@"URL GOTO=http://demo.imacros.net/Automate/TestForm1");
            macro.AppendLine(@"'We use quotation marks for a content with spaces");
            macro.AppendLine(@"TAG POS=1 TYPE=INPUT:TEXT ATTR=NAME:name CONTENT=""iMacros User""");
            macro.AppendLine(@"'Selecting a drop down");
            macro.AppendLine(@"TAG POS=1 TYPE=SELECT ATTR=NAME:food CONTENT=%French<SP>Fries");
            macro.AppendLine(@"TAG POS=1 TYPE=SELECT ATTR=NAME:drink CONTENT=%Coke");
            macro.AppendLine(@"'Selecting a radio button");
            macro.AppendLine(@"TAG POS=2 TYPE=INPUT:RADIO ATTR=NAME:drinksize");
            macro.AppendLine(@"");
            macro.AppendLine(@"TAG POS=1 TYPE=SELECT ATTR=NAME:dessert CONTENT=%chocolate<SP>cake");
            macro.AppendLine(@"TAG POS=1 TYPE=INPUT:RADIO ATTR=NAME:Customer");
            macro.AppendLine(@"SET !ENCRYPTION NO");
            macro.AppendLine(@"TAG POS=1 TYPE=INPUT:PASSWORD ATTR=NAME:Reg_code CONTENT=pwd");
            macro.AppendLine(@"'In a quoted content, we can use \n for a new line, \t for tab and \"" for literal quotes");
            macro.AppendLine(@"TAG POS=1 TYPE=TEXTAREA ATTR=NAME:Remarks CONTENT=""Hi!\n\n \t iMacros can fill  forms;-)\n\nTom""");
            macro.AppendLine(@"TAG POS=1 TYPE=BUTTON:SUBMIT ATTR=TXT:Click<SP>to<SP>order<SP>now");

            // This time the macro is a string starting with "CODE:"
            string macroCode = "CODE:" + macro.ToString();
            PlayMacro(macroCode);
        }

        private void search_Click(object sender, EventArgs e)
        {
            errorBox.Clear();
            // A previously recorded macro which performs a simple search in the iMacros Wiki
            StringBuilder macro = new StringBuilder();
            macro.AppendLine(@"URL GOTO=http://wiki.imacros.net/Main_Page");
            macro.AppendLine(@"TAG POS=1 TYPE=INPUT:TEXT ATTR=NAME:search CONTENT=component");
            macro.AppendLine(@"TAG POS=1 TYPE=BUTTON:SUBMIT ATTR=NAME:button");
            string macroCode = "CODE:" + macro.ToString();
            // Play the macro string
            PlayMacro(macroCode);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (iim == null)
                return;

            // To stop the iMacros player, just call Stop()
            iim.Stop();
        }

        private void pause_Click(object sender, EventArgs e)
        {
            if (iim == null)
                return;
            if (pause.Text == "Pause")
                pause.Text = "Continue";
            else
                pause.Text = "Pause";
            // PauseOrContinue() pauses a playing instance, or resumes playback mode if the instance is paused
            iim.PauseOrContinue();
        }

        #endregion

        #region Control iMacros

        private void PlayMacro(string macro)
        {

            if (iim == null)
                return;

            // Take care that the player is in idle mode before starting to play a macro
            if (iim.PlayerMode != PlaybackModes.Idle)
                return;
            // Subscribe to the events which are fired by iMacros player.
            iim.PlayerStatusUpdated += iim_PlayerStatusUpdated;
            iim.MacroErrorOccurred += iim_iMacrosError;
            iim.MasterPasswordRequested += iim_MasterPasswordRequested;
            iim.Prompt += iim_Prompt;
            iim.BrowserResize += iim_BrowserResize;

            // The macro can either be a macro embedded in a string starting with "CODE:"
            // or the full path to a macro file. If a relative path is given, 
            // iMacros will look for the macro in iMacros\Macros folder, as usual
            // The iMacros\Macros folder can be set directly in the registry.
            // The macro file could also be an obfuscated macro. 
            // To obfuscate a macro, use iMacros Enterprise Edition and set the 
            // apropriate registry value with your obfuscation password.
            // Only the iMacros Player can read an obfuscated macro, even if the password is known.
            int res = iim.Play(macro);

            // Now we can detach the player events
            iim.PlayerStatusUpdated -= iim_PlayerStatusUpdated;
            iim.MacroErrorOccurred -= iim_iMacrosError;
            iim.MasterPasswordRequested -= iim_MasterPasswordRequested;
            iim.Prompt -= iim_Prompt;
            iim.BrowserResize -= iim_BrowserResize;

            if (res == -99) return; // res == -99 means that the player was in playing mode when Play was called. 

            // Get a single string with all the extracted values, in the scripting interface style.
            string extract = iim.ExtractedValues.Count > 0 ?
                String.Join("[sep]", iim.ExtractedValues.ToArray()) :
                String.Empty;

            // Get the total runtime, which is the first key value pair in the PerformanceData Dictionary. 
            // Here we opted to use the GetPerformancData() method, which uses a syntax similar to the scripting
            // interface pendant.
            string label = null;
            TimeSpan elapsedTime = TimeSpan.Zero;
            iim.GetPerformanceData(0, out label, out elapsedTime);

            // Write all in a text box
            errorBox.Text = String.Format("Error code = {0} \r\nerror text = {1} \r\nextracted = {2} \r\n{3} = {4}",
                iim.ErrorCode, iim.ErrorText, extract,
                label, elapsedTime.Milliseconds.ToString());
        }

        private void ChangeEmulationMode()
        {
            // Check what is the emulation mode used by this application's WebBrowser Control
            int mode = iim.GetBrowserEmulationMode();
            errorBox.Text = string.Format("Emulation mode = {0}", mode.ToString());

            // We can also check if it is the desired value and change it in case it is not, but the new value will only be taken into account the next time the application runs.
            int desiredMode = 11000;
            if (mode != desiredMode)
            {
                try
                {
                    iim.SetBrowserEmulationMode(desiredMode);
                    Application.Restart();
                }
                catch (iMacrosException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to set emulation mode. Exception = {0}", ex.ToString());
                }
            }
        }

        #endregion
    }
}
