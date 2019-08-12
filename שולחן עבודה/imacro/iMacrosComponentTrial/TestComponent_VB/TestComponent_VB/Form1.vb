Imports iMacros.Component
Imports iMacros.Component.EventHelpers

Public Class Form1

    Dim iim As iMacrosControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim licenseKey As String
        licenseKey = "CMPNTJJH3W"
        iim = iMacrosControl.Create(licenseKey)
        iim.Dock = DockStyle.Fill

        browserPane.Controls.Add(iim)
        ' Now that the iMacros browser control has been initialized, we can attach to the browser events.
        AddHandler iim.BrowserStatusUpdated, AddressOf iim_BrowserStatusUpdated

    End Sub

    Private Sub iim_MasterPasswordRequested(sender As System.Object, e As MasterPasswordEventArgs)
        ' Handle the MasterPasswordRequested event, when iMacros needs a TEMPKEY do decrypt a temporary password

        ' Set the master password 
        e.MasterPassword = "other"
    End Sub

    Private Sub iim_Prompt(sender As System.Object, e As PromptEventArgs)
        ' If the PROMPT command does not require any user input, a simple MessageBox can be used to communicate with the user.
        If (Not e.NeedsInput) Then
            MessageBox.Show(e.Message)
        Else
            ' Otherwise it is necessary to implement a modal dialog to obtain user input. 
            ' Here we use the MessageBox as a simple alternative which only provides very limited user feedback.
            Dim result = MessageBox.Show(e.Message, "TestComponent", MessageBoxButtons.YesNo)
            e.Value = result.ToString()
            e.Handled = True
        End If
    End Sub

    Private Sub iim_BrowserStatusUpdated(sender As Object, e As EventArgs)
        If iim Is Nothing Then Return
        ' Handle the BrowserStatusUpdated event by displaying the BrowserStatus in a text box 
        ' when it is updated (most common to see in the status bar)
        browserBox.Text = iim.BrowserStatus
    End Sub

    Private Sub iim_PlayerStatusUpdated(sender As Object, e As EventArgs)
        If iim Is Nothing Then Return
        ' Handle the PlayerStatusUpdated event by displaying the updated PlayerStatus (macro, command and error messages)
        playerBox.Text = iim.PlayerStatus
    End Sub

    Private Sub iim_MacroErrorOccurred(sender As Object, e As EventArgs)
        If iim Is Nothing Then Return
        ' Handle the iMacrosError event by displaying  the ErrorCode and ErrorText in a text box.
        errorBox.Text = String.Format("Error {0} occurred: {1}", iim.ErrorCode, iim.ErrorText)
    End Sub

    Private Sub iim_BrowserResize(sender As Object, e As BrowserResizeEventArgs)
        If iim Is Nothing Then Return
        ' Handle the BrowserResize event when iMacros Player requests the browser to be resized (due to a SIZE command, for instance)
        Dim oldsize As Size
        oldsize = iim.Size
        If oldsize <> e.Size Then
            Width = Width + e.Size.Width - oldsize.Width
            Height = Height + e.Size.Height - oldsize.Height
        End If
        Application.DoEvents()
    End Sub

    Private Sub play_Click(sender As System.Object, e As System.EventArgs) Handles play.Click
        errorBox.Clear()
        If openFile.ShowDialog = DialogResult.OK Then
            ' Play the macro file
            PlayMacro()
        End If
    End Sub

    Private Sub stopBt_Click(sender As System.Object, e As System.EventArgs) Handles stopBt.Click
        If iim Is Nothing Then Return
        ' To stop the player just call Stop()
        iim.Stop()
    End Sub

    Private Sub pause_Click(sender As System.Object, e As System.EventArgs) Handles pause.Click
        If iim Is Nothing Then Return
        If pause.Text = "Pause" Then
            pause.Text = "Continue"
        Else
            pause.Text = "Pause"
        End If
        ' PauseOrContinue() pauses a playing instance, or resumes playback mode if the instance is paused
        iim.PauseOrContinue()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChangeEmulationMode()
    End Sub

    Private Sub PlayMacro()
        If iim Is Nothing Then Return
        ' Take care that the player is in idle mode before starting to play a macro
        If iim.PlayerMode <> PlaybackModes.Idle Then Return

        'Subscribe to the events which are fired by iMacros player.
        AddHandler iim.MasterPasswordRequested, AddressOf iim_MasterPasswordRequested
        AddHandler iim.Prompt, AddressOf iim_Prompt
        AddHandler iim.MacroErrorOccurred, AddressOf iim_MacroErrorOccurred
        AddHandler iim.BrowserResize, AddressOf iim_BrowserResize
        AddHandler iim.PlayerStatusUpdated, AddressOf iim_PlayerStatusUpdated

        Dim res As Integer
        ' Here we could also play a macro embedded in a string starting with "CODE:"
        ' If a relative path is given, iMacros will look for the macro in iMacros\Macros folder, as usual
        ' The iMacros\Macros folder can be set directly in the registry.
        res = iim.Play(openFile.FileName)

        ' Now we can detach the player events
        RemoveHandler iim.MasterPasswordRequested, AddressOf iim_MasterPasswordRequested
        RemoveHandler iim.Prompt, AddressOf iim_Prompt
        RemoveHandler iim.MacroErrorOccurred, AddressOf iim_MacroErrorOccurred
        RemoveHandler iim.BrowserResize, AddressOf iim_BrowserResize
        RemoveHandler iim.PlayerStatusUpdated, AddressOf iim_PlayerStatusUpdated

        If res = -99 Then Return ' res == -99 means that the player was in playing mode when Play was called.

        'Get a single string with all the extracted values, in the scripting interface style.
        Dim extract = String.Empty
        If iim.ExtractedValues.Count > 0 Then
            extract = String.Join("[sep]", iim.ExtractedValues.ToArray())
        End If

        ' Get the total runtime, which is the first key value pair in the PerformanceData Dictionary. 
        ' Here we opted to use the GetPerformancData() method, which uses a syntax similar to the scripting
        ' interface pendant.
        Dim label = String.Empty
        Dim elapsedTime = TimeSpan.Zero
        iim.GetPerformanceData(0, label, elapsedTime)

        ' Write all in a text box
        errorBox.Text = String.Format("Error code = {0} error text = {1} extracted = {2} {3} = {4}",
            iim.ErrorCode & vbNewLine, iim.ErrorText & vbNewLine, extract & vbNewLine,
            label, elapsedTime.Milliseconds.ToString())

    End Sub

    Private Sub ChangeEmulationMode()
        'Check what Is the emulation mode used by this application's WebBrowser Control
        Dim mode = iim.GetBrowserEmulationMode()
        errorBox.Text = String.Format("Emulation mode = {0}", mode.ToString())

        'We can also check if it Is the desired value And change it in case it Is Not, but the New value will only be taken into account the next time the application runs.
        Dim desiredMode = 11000
        If mode <> desiredMode Then
            Try
                iim.SetBrowserEmulationMode(desiredMode)
                Application.Restart()
            Catch ex As iMacrosException
                Debug.WriteLine("Failed to set emulation mode. Exception = {0}", ex.ToString())
            End Try

        End If
    End Sub


End Class
