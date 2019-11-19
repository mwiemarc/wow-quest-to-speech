# Quest-to-Speech *(World of Warcraft)*
### **Speech synthesizer for npc dialogs in world of warcraft**
#### currently works with wow classic (retail will follow soon)

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

### **Audio samples**
##### Windows buildin text-to-speech provides the poorest speech quality, for the web services they mostly offer "default" voices and neuronal trained once which sounds best
##### *I'm using the free tiers of that cloud providers, they give you a free amount of characters per month*

[(windows-en-us-zira)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/windows-en-us-zira.mp3)
[(windows-de-de-hedda)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/windows-de-de-hedda.mp3)

[(azure-en-us-jessa-neural)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/azure-en-us-jessa-neural.mp3)
[(azure-de-de-katja-neural)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/azure-de-de-katja-neural.mp3)

[(google-en-us-standard-c)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/google-en-us-standard-c.mp3)
[(google-de-de-standard-a)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/google-de-de-standard-a.mp3)

[(google-en-us-wavenet-c)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/google-en-us-wavenet-c.mp3)
[(google-de-de-wavenet-a)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/google-de-de-wavenet-a.mp3)

[(aws-en-us-joanna)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/aws-en-us-joanna.mp3)
[(aws-de-de-marlene)](https://github.com/mwiemarc/wow-quest-to-speech/blob/master/audio_samples/aws-de-de-marlene.mp3)


&nbsp;

## How it works
* A wow addon encodes the text and additional informations like the gender
* The encoded text will be displayed in a frame over the dialog
* As you copy the text (CTRL+C or +X) a pc programm will detect that and starts working
* From your selected voices a random one will be picked
* Using a text-to-speech module to generate the sound output and play it


### supported text-to-speech engines
* windows buildin
* azure cloud
* google cloud 
* amazon aws

&nbsp;
&nbsp;

## Installation
* Download the latest release form releases section
* Extract content of APPLICATION directory and place it somewhere on your pc (e.g. "C:/Program Files/Quest-to-Speech")
* Extract content of ADDON directory to your wow installation (e.g. "C:/Program Files/World of Warcraft/_classic_/Interface/Addons/QuestToSpeech")
* Start Quest-to-Speech.exe from Quest-to-Speech directory (and create a desktop link)
* Setup apis you maybe want, enable the modules and select voices
* From there you are ready to go, click Start and head into wow to test it
* Have fun

&nbsp;
&nbsp;

# **Screenshots**

![WoW Addon](https://i.imgur.com/eGRExJZ.jpg "WoW Addon")
![PC Software](https://i.imgur.com/Fewbb9w.png "PC Software")
![Voices Settings](https://i.imgur.com/Li4rAXq.png "Voices Settings")
![API Settings](https://i.imgur.com/WcqLb0I.png "API Settings")
