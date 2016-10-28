# BubbleGum
A very simple plugin to display 2D speech bubbles in Unity!
**Destined to be used with Unity 2D projects only (Need some code tweaks to work with 3D).**

*Includes Ink Unity Integration (Version: 0.6.1 (Oct 14, 2016) straight from the Asset Store).
Ink is under MIT Licence - Copyright (c) 2016 inkle Ltd.*

**The character sprite in the example scene was done by my talented friend Marion Kartes, go check out her Portfolio!**(http://kartesmarion.wixsite.com/portfolio)

# Quick start guide

## So hey, how do I change the displayed texts?

Well, dear user, for editing text, BubbleGum relies on a simple software called Inky, that you can download in te link below:

=> http://www.inklestudios.com/ink/

This is how you can open .ink files, that are automatically converted into JSON files once imported into a Unity project that includes the Ink Unity Integration Package (Which I already included in the the releases of BubbleGum, so you shouldn't have to worry about it ^^).

## So huh, anything specific I must have in mind while writing those dialogues?

Yup, you can see how it works if you go to the sample assets I left in the release. Try to find the BubbleGumExample.ink file in the plugin folder and open it with Inky.

You should get this:


    -Character1: And that's how you pop up some speech bubbles!
    -Character2: Now go have some fun, you people.
    - ->END

So, let's break down what's going on here:
First, the "-" denotes the beginning of a new line.
Most of the time, the "-" are followed by the name of the speaking character. It should have the **exact same name as the gameObject on which the speech bubble will get attached.**

To mark the end of the dialogue, just finish the ink file with **"- ->END".**

##All right, my dialogue is ready, now what?

Well, the first thing to do is import the "BubbleGum Manager" prefab into your Unity scene.
Then, unfold and see there should be a sample Dialogue inside the DialogueManager object.
As you can see, the dialogue consists of a gameObject with a "Dialogue" component.

So let's see what options we have here:
- Ink JSON Asset is where you should put the ink compiled JSON of your .ink file.
- Dialogue Characters is where you will put your gameObject that are going to speak through the bubbles. They should be objects that bears the same name as the characters that are present in your ink dialogue.
- Flip Side For This Character should have the same size as the Dialogue Characters array, even if you're not planning of using it. This is an option if you need the bubble to appear on one side of the speaking character instead of another.

##Okay, now that my dialogue object is set up too, how can I trigger my dialogue?

This is really easy, the Dialogue component have a public method called "TriggerDialogue".
So, each time you want to trigger a Dialogue, just get its component through a script, then call its "TriggerDialogue" method, it's as simple as that.
You can see an example of how to trigger the dialogue with the "TriggerDialogueExample.cs" file included in the release.

##Can I easily check if my dialogue is currently running or not?

Yup, there are actually 3 different public boolean variables inside the "Dialogue" class/component that can be used to track the current state of your dialogue sequence:

- DialogueTriggered is marked "true" the exact moment your dialogue is triggered.
- Then, "DialogueInProgress" will be "true" the whole time your dialogue is in progress.
- DialogueClosed will be "true" once the dialogue ended. This variable will be "false" if the dialogue is triggered again, until it's closed again.


## And that's it!

If you already did some basic Unity stuff with C# you shouldn't have too many issues using BubbleGum, it's pretty basic stuff, but I hope it will spare you the amount of time I've put in it to make sure those damn words and bubbles would align correctly.
Of course, you can customize the text's font, its display animation, the different pictures that coposes the bubble and its pointer, BubbleGum just uses Unity UI Canvas, so everything should be fairly easy to change according to your tastes.

Be sure to check out the "DialogueDisplay" gameObject too, as its "DialogueDisplayer" component contains some options such as the Display Speed Rate of words, some padding options, color options,...

I know BubbleGum is pretty basic for now, but I really do hope it can be used as a base to make it more complex, with more options for everyone to customize it. Anyway, I hope it'll be useful for you, and don't hesitate to fork this repo to make this very small plugin grow =)

**If you have any problems or questions about BubbleGum, please use GitHub's Issues Tab, and I'll answer you back ASAP!**
