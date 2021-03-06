// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.

========================================================================
    Win32 APPLICATION : MUIAppSample Project Overview
========================================================================


Note: This documentation is preliminary and is subject to change. 
Note: This application sample must be compiled with the Windows SDK for Vista and Microsoft Visual Studio 2005, and will run only on Windows Vista and later.

The Windows SDK for Vista includes this application sample to demonstrate MUI functions, most of which are new in Windows Vista. This application is in the form of a complete project that can be built either in Visual Studio or from the console command line. It can be found in the folder ProgramFiles\Microsoft SDKs\Windows\v6.0\Samples\WinUI\MUISampleApp or the folder that you select during installation. Because setting up an MUI application requires considerable project configuration, the sample includes an MUI application project template for Visual Studio 2005. 

The application sample is localized for the following languages, and allows the user to select any of these languages for the user interface: 

en-US: English (United States) 
fr-FR: French (France) 
de-DE: German (Germany) 
ja-JP: Japanese (Japan) 
pt-BR: Portuguese (Brazil) 

In addition, the user can select any of the user preferred UI languages set in Regional and Language Options in the Control Panel for the application user interface. If the user chooses a language from the list that is not one of the languages for which the application is localized, the application uses the appropriate fallback language.

Note: This application sample fails if none of the five supported languages is the default machine user interface language or is installed as part of a language MUI Pack or a Language Interface Pack (LIP) on the computer.

Once the user chooses a user interface language, the application selects the resources appropriate to the selected language, provided in an image file that supports an audio clip with closed captioning. The application implicitly passes the user interface settings to embedded Windows Media Player and Internet Explorer controls, and to the Common Controls dialog box used for language selection. In addition, the application uses the GetFileMUIPath function to load an image, providing a typical example of loading a non-Win32 resource according to user interface language preferences.

Note: The locales and the banners displayed by the application sample have been chosen arbitrarily for demonstration purposes. The screenshot pictures shown in this topic are specific to these locales. The picture that shows what the user will first see when the application is launched is based on the assumption that the system default UI language is English (United States).

Runtime Requirements
=======================
This sample is designed to run on x86 and x64 versions of Windows Vista. It is not designed to run on IA64 (Itanium) versions of Windows Vista, or server versions of x86 and x64 Windows Vista.

To run the sample, you must ensure that Windows Media Player is installed on your computer.

How to Build this Application Sample
=====================================

Build Environment Requirements
-------------------------------
To build this application sample from source, you must have the Windows SDK for Vista installed and registered. At least a beta 2 version of the SDK is required.

Note: This sample is configured to build for x86 and x64 target platforms. The IA64 (Itanium) platform is not supported. If you are planning to build an x64 version of the sample, ensure that Visual Studio 2005 support for x64 is installed on your computer.

Before building the project, you must register the SDK directories with Visual Studio. To do this from the Start menu: 

1. Click All Programs. 
2. Now select Microsoft Windows SDK, followed by Visual Studio Registration. 
3. Right-click Register SDK Directories with Visual Studio 2005 and select Run as administrator. 
4. Copy the MUISampleApp folder into a folder outside of Program Files to which you have write access. A suggested folder is My Documents\Visual Studio 2005\Projects. For more information, see the Readme file available from Start → All Programs → Microsoft Windows SDK → Readme. 


Building the Project in Visual Studio
--------------------------------------
1. Run Visual Studio 2005 from Start → All Programs → Visual Studio 2005. 
2. From the application sample source code folder, open the MUIAppSample.sln solution file. 
3. Select the required configuration (release/debug) and platform (win32/x64) and build the solution. The executable and the .mui files are placed in the 4. folder appropriate to the selected configuration and platform and in language subfolders. 
5. Building the Project from the Command Line


To build the project from the command line:
--------------------------------------------
1. Open the command prompt from Start → All Programs → Microsoft Visual Studio 2005 → Visual Studio Tools → Visual Studio 2005 Command Prompt. 
2. Navigate to the folder containing the application sample source code, and type the following: 

vcbuild MUIAppSample.sln

3. The above procedure builds the sample for all available configurations and platforms. For more information about controlling the command line build, refer to the Vcbuild.exe command line help by typing the following:

vcbuild /?

What Happens During the Build?
--------------------------------
When you build this sample, files are built separately for each language. Each build creates identical language-neutral MUIAppSample.exe and language-specific MUIAppSample.exe.mui files. In addition, various other files are copied to the appropriate release folders.

The resource configuration file Mui.rcconfig, common to all languages, indicates which resources are language-neutral and which are language-specific. It can also provide the checksum that is used to associate each language-specific .mui file with a particular version of the language-neutral (LN) file, although it does not do this for the sample. As discussed in Creating Win32 Resources, there are several ways to generate a checksum value that can be used for this purpose, and it does not have to be a true checksum.

The build process for the sample sets up a Visual Studio project for each language. In each project, the RC Compiler utility compiles and splits the non-localizable and localizable resources into two different object files, using the information in the resource configuration file. The additional parameters passed to the RC Compiler can be seen in the property pages for the project under Configuration Properties → Resources → Command Line → Additional options.

As part of the normal Visual C++ build, which is a post-build event, the language-neutral resources are linked into an LN file, and the language-dependent resources are linked into a language-specific .mui file. The results of the event can be seen in the property pages for each language project under Configuration Properties → Build Events → Post-Build Event → Command Line. The LN files for each language are identical except for the checksum values in them.

The project must select a copy of the LN file as the base, and arbitrarily chooses the file for English (United States). The checksum value from this language is applied to the .mui files in another language using the MUIRCT utility.

As a final step in the build process, the project copies the files to the appropriate release folders using MUI-compliant techniques.

Running the application
========================

To run the application, execute the MuiAppSample.exe file in the [ProjectsourceFolder]\[Win32/x64]\[Debug/Release] folder.


Files Included in the Sample
=============================

Source Files
-------------
The application sample is distributed as a ZIP file. The following table defines the files included in the ZIP file that are used to build the application.

C++ source files 

File 				Notes 
CCWindow.h, CCWindow.cpp 			- Declare and implement the closed captioning window. 
CWMPEventDispatch.h, CWMPEventDispatch.cpp 	- Declare and implement the event dispatcher. 
GlobalConfig.h 					- Declares a global structure to store information about the install language, the user preferred UI 						languages, the system preferred UI languages, and the user interface languages supported by the application 						sample, including the currently selected language. 
IEController.h, IEController.cpp 		- Declare and implement the container for the embedded Internet Explorer browser control. 
Iectrl.h 					- Declares the embedded Internet Explorer browser object. 
MPController.h, MPController.cpp 		- Declare and implement the container for the embedded Windows Media Player control. 
Mui.h 						- Declares the version resource for the application sample. This is a Visual Studio-generated resource header file. It is included in the .rc file so that the final binary will contain the version resource. 


MUIAppSample.h, MUIAppSample.cpp 		- Declare and implement the main code for the application. This code renders most of the MUI functionality. 
Resource.h 					- Declares identifiers for resources in the application. 
Stdafx.cpp, Stdafx.h 				- Represent standard includes. 
Wmp.h, Wmpids.h 				- Included by CWMPEventDispatch.h. 



The next table defines other files included in the ZIP file.

Other files File Notes 
Mui.rcconfig 		- Supports resource configuration, which indicates the resources that are language-neutral and the ones that are language-specific. 			Resource configuration also provides the checksum that is used to associate the language-specific MUIAppSample.exe.mui files with 			this particular version of the LN file MUIAppSample.exe. 
MUIAppSample.ico 	- Defines the default 32x32-pixel application icon.  
MUIAppSample.sln 	- Defines the Visual Studio "solution" file. 
Multilang.smi 		- Supports multilingual closed captioning. 
Multilang.wma 		- Supports a multimedia sample, with multiple audio streams. 
Openfile.ico 		- Supports an Open File icon. 
Small.ico 		- Supports a default 16x16-pixel application icon.  

The main source folder for the application sample also contains a "dll" folder containing sources related to the embedded Internet Explorer control, and a language folder for each supported language. Each language folder contains additional copies of the icon files and of Resource.h, plus the files defined in the following table.

Language folder files File Notes 
Banner.jpg 		- Shows an image appropriate to the locale. 
<language>.rc 		- Indicates an appropriately named resource file for a language. For example, the English-language resource file is EN-US.rc. This 			file contains both language-specific resources and language-neutral resources.  
<language>.vcproj 	- Indicates an appropriately named Visual Studio project file for a language. It is used in building the application for this language. 

Runtime Files
---------------
For the application to run properly, certain files must be present on the target computer. When the release version of the application is built, the following files are placed in the Release folder:


Iectrl.dll 		— Internet Explorer core DLL 
MUIAppSample.exe 	— main executable file 
Multilang.smi 		— multilingual closed captioning file 
Multilang.wma 		— sample multimedia file with multiple audio streams 
The Release folder also contains a language folder for each supported language. Each folder contains two files:

Banner.jpg 		— an image appropriate to the locale 
MUIAppSample.exe.mui 	— language-specific resource file 

API Function Coverage
======================
Windows Vista provides a number of API functions to allow you to leverage the MUI technology. The code in MUIAppSample.cpp demonstrates five of these functions, as well as several other functions that are closely related to MUI technology.

API function Demonstrated by this sample 
EnumUILanguages 		- Yes. The application function InitGlobalConfig calls this function to verify that the default user interface language for 				the user is supported and installed. This function can also be used to obtain a complete list of supported and installed user 				interface languages. 
GetFileMUIInfo 			- No. 
GetFileMUIPath 			- Yes. The application function LoadBanner calls this function to identify the correct banner picture to load in the upper 				right panel, based on a language name. 
GetSystemDefaultUILanguage 	- No.  
GetSystemPreferredUILanguages 	- No. 
GetThreadPreferredUILanguages 	- Yes. The application function FillUserAndSystemPreferredLanguages calls this function to get a merged list of all languages 				in the fallback list. The return is used to populate the language list that the user sees in the UI Language Settings window 					upon selecting Windows Language Setting. When the application function calls the MUI function, there are no languages set 					specifically for the thread. However, passing the flags MUI_MERGE_SYSTEM_FALLBACK | MUI_MERGE_USER_FALLBACK to the MUI 						function ensures retrieval of a full list of user and system preferences. The application makes two calls to the MUI 				function, the first to determine the required buffer size, and the second to obtain the data. 
GetThreadUILanguage 		- No. 
GetUILanguageInfo 		- No. 
GetUserDefaultUILanguage 	- Yes. InitGlobalConfig calls this function to get the user interface language for the current user. 
GetUserPreferredUILanguages 	- No. 
SetThreadPreferredUILanguages 	- Yes. InitGlobalConfig calls this MUI function to set the selected user interface language. The application function 				ReloadUI does the same, and then reloads miscellaneous resources and sets window texts so that the user interface language 				change takes immediate effect. The application function FillSystemPreferredLanguages also calls the MUI function with an 				empty language list to clear the thread preferred UI languages list so that there is nothing to take priority over user and 				system preferences. 
SetThreadUILanguage 		- No. 

In addition, the application sample demonstrates the use of the MUI-compatible functions and macros defined in the following table. Function/macro Remarks 
EnumResourceLanguages The application sample uses this function to list resource languages. Alternatively, the application could call EnumResourceLanguagesEx, to take more control over the location to search for resources. The application shows one possible use of this function, which is to determine the MUI languages supported by an application executable. 
GetLocaleInfo The application sample uses this function to retrieve locale data. Alternatively, the application could call GetLocaleInfoEx, passing a locale name instead of a locale identifier. 

LCIDToLocaleName 		- The application sample uses this function to convert a language identifier to a language name. 
LocaleNameToLCID 		- The application sample uses this function to convert a language name to a language identifier. 
LANGIDFROMLCID 			- The application sample uses this macro to convert a locale identifier to a language identifier. 


Debug Version
=============
Windows Vista does not come by default with the debug version of the Internet Explorer runtime library required to run a debug version of the application sample. To run the debug version of the operating system, you must install Visual Studio 2005 or .NET Framework 2.0 SDK on the computer used to run the debug version of the sample.

User Interface
==============

The left panel presents an embedded Internet Explorer browser. The upper right corner displays a banner picture and the closed captioning window. The lower right corner displays an embedded Windows Media Player. The sole purpose of these controls is to demonstrate how MUI technology is used in Windows Vista and later.

The components Internet Explorer, Windows Media Player, and the common dialog box obtained through the Open File icon, are MUI-enabled. The application sample is also MUI-enabled. When you change the user interface language setting for this application, the user interface immediately refreshes to its new locale.

Note: Although the embedded Internet Explorer component is MUI-enabled, the HTML page that it displays is not. Internet Explorer has an Accept Languages setting that controls the language(s) to use when rendering a Web page. This setting is not currently consistent with the MUI setting. Thus, if you right-click in the Internet Explorer window, the resulting context menu is in the correct language. However, an application user interface setting does not affect the content of the displayed HTML page accessed in Internet Explorer. Another application, though, can have its own own language-specific HTML resource, just like this sample has its own language-specific .jpg resource.


Changing the Language Setting
==============================
To change the user interface language setting for the application sample only, select Options → UI Language Settings.
 
If necessary, you can set the application sample to use the language settings of the operating system. To do this, use the Control Panel to access the user interface language preferences for the user and then change the preferred language.

Once you have selected a different language, the user interface for the application sample automatically refreshes. Because the user interface components are MUI-enabled, the thread user interface language of each component changes. For instance, you can right-click in the Internet Explorer window to bring up the context menu, which displays in the appropriate language.

 
Playing Language-sensitive Multimedia Files
============================================
The embedded Windows Media Player control can play multilingual multimedia files. It shows the closed captioning in your current user interface language, and plays the audio stream in that language. Try this feature using the sample media file Multilang.wma, included in the application sample package.
