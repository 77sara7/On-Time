iMacros Component
=================
version: 12.5.503.8802 		
updated: 2019-05-07 13:03:06Z	  		

							**********************************************
								This version targets .NET Framework 4.5.2 
							**********************************************

The iMacros WebBrowser Component gives you the ability to build your own .NET application with all of the playback power of the iMacros Browser, but you have complete control over the look and feel.
The component includes all of the playback functionality of iMacros organized in a tabbed web browser control (100% Microsoft Internet Explorer web browser control compatible), a dialog manager, image recognition, plus DirectScreen technology for automating Flash, Java, and Silverlight. In essence, you’re getting the core engine used in the iMacros Browser for rendering and automating web pages.
It is important to note that it is a player-only component. It does not provide recording functionality, so you need at least one other version of iMacros for recording.

Getting Started
===============

1. Unpack the zip file to any place in your development machine. The main folder iMacrosComponent contains 2 other sub-folders: Bin and Docs. In Docs you find the documentation in web style of the exposed namespaces and their members. The Bin folder contains the dlls needed to compile and run iMacros.

2. Open a new windows forms project. Target it to the x86 platform.

3. Add a reference to iMacrosComponent\Bin\iMacros.Component.dll. Build the solution. On compiling, Visual Studio copies iMacros.Component.dll to the current configuration bin folder, together with some of its dependencies: Interop.SHDocVw.dll (supposing that you have the reference property "Copy Local" set to true)

4. Copy also the other dlls from the iMacrosComponent\Bin folder to your project's bin\configuration (Debug or Release, for instance) folder. There are 4 additional dlls (5 in total.) They are necessary during runtime.

5. Now you are ready to add an iMacrosControl to your form. The iMacrosControl cannot be added in the designer. Use the factory method iMacrosControl.Create() to instantiate an iMacrosControl object in your code. Add it to the form's or a panel's controls (see sample projects).

6. MacrosControl.Create() needs a license key. If you are using the trial version, enter "CMPNTJJH3W" as key.


What else you should know
=========================

1. The first time you run your application in an user account which has never installed or used iMacros, a folder structure similar to the one used by the iMacros Browser is created in the user's Documents folder. You can control this by setting in advance - either in your installer, or in your application before iMacrosControl is instantiated - the proper registry strings in HKEY_CURRENT_USER\Software\Ipswitch\iMacros (both 32 and 64 bit OS): FolderDataSources, FolderDownloads, FolderLogs, FolderMacros.

2. A  few other properties of the iMacros player, which might be of interest, are stored in the same HKCU key:
    * PlaybackDelay (0) [corresponds to macro variable !PlaybackDelay, however in milliseconds, instead of seconds]
    * TimeoutPage (60) [corresponds to macro variable !TIMEOUT_PAGE]
    * MasterPassword [Encrypted. Please, contact support if you need to change this]
    * EncryptionPasswordMode (Storedkey) [corresponds to macro variable !ENCRYPTION]
    * ScriptErrorsSuppressed (True) [controls if script errors are immediately dismissed by the dialog manager]
    * UseRegionalSettings (False) [controls if csv files use a comma (US) or the regional settings separator]
    * LogEvents (True) [whether a log file is produced at each run. The file nae can be set with !FILELOG]
    * Profiler (False) [whether to write a performance profile xml file. See http://wiki.imacros.net/Log_Performance]

3. By default, the browser control emulates IE7. In order to have it emulating a newer version of Internet Explorer, your application has to be listed in HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION. For instance, YourApplication.exe, value 11000, for IE11 (see http://wiki.imacros.net/IE9_Nags#What_IE_compatibility_mode_does_the_webbrowser_control_use.3F).

4. When deploying your application, deploy also the iMacros dlls (in the Bin folder, 5 altogether.) iMacros expects to find these dlls in the same directory as your executable.

5. iMacros Dialog Manager, DirectScreen and the Image Recognition API depend on the Visual C++ 2015 Runtime Libraries x86 (v140).  Please, make sure your users have it installed in their system. You may as well simply install vcredist_x86.exe, from https://www.microsoft.com/en-us/download/details.aspx?id=48145, or http://download.microsoft.com/download/6/D/F/6DF3FF94-F7F9-4F0B-838C-A328D1A7D0EE/vc_redist.x86.exe

License Agreement
=================

Please refer to License.txt on the main distribution folder.
