# Quest-to-Speech *(World of Warcraft)*
### **Speech synthesizer for npc dialogs in world of warcraft**

&nbsp;
## **Features**
* Should support every dialog and text window from npcs and items, also from questlog
* Supports multiple text-to-speech engines (at the same time)
* You can select as much voices as you want and as you play a text a random voice will be picked
* You can queue multiple texts
* Minimize to tray
* Super easy to use

#### **Addon commands**
* **/qts** - show addon options
* **/qts history** - show a history of the last 20 copied texts
* **/qts autofocus on/off** - sets autofocus of copy textbox on show (default is off)

### currently works with wow classic (retail will follow soon)

&nbsp;
&nbsp;

## How it works
* A wow addon encodes the text and additional informations like the gender
* The encoded text will be displayed in a frame over the dialog
* As you copy the text (CTRL+C or +X) a pc programm will detect that and starts working
* From your selected voices a random one will be picked
* Using a text-to-speech module to generate the sound output and play it

## Installation
* Download the latest release form releases section
* Extract the Quest-to-Speech directory and place it somewhere on your pc
* Extract content of WoWAddon to your wow installation (e.g. "C:/Program Files/World of Warcraft/_classic_/Interface/Addons/QuestToSpeech")
* Start Quest-to-Speech.exe from Quest-to-Speech directory
* Setup apis you maybe want, enable the modules and select voices
* From there you are ready to go, click Start and head into wow to test it
* Have fun

### supported text-to-speech engines
* windows buildin
* azure cloud
* google cloud 
* amazon aws

&nbsp;
&nbsp;

# **Screenshots**

![WoW Addon](https://i.imgur.com/eGRExJZ.jpg "WoW Addon")
![PC Software](https://i.imgur.com/Fewbb9w.png "PC Software")
![Voices Settings](https://i.imgur.com/Li4rAXq.png "Voices Settings")
![API Settings](https://i.imgur.com/WcqLb0I.png "API Settings")
