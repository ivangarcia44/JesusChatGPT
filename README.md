This is a Unity game engine project that has a non-playable character (NPC) acting as Jesus using ChatGPT.

To use the project do this:

git clone https://github.com/ivangarcia44/JesusChatGPT.git

To run this project you need to create an OPEN AI key. It will also have an organization name.

In your computer (Windows Operating System), setup these two environment variables:

OPENAI_API_KEY
OPENAI_ORG

They will have your open AI key and organization respectively.

Then open in the Unity game engine.

In the ChatGptScript1.cs file you might need to replace this line with another model name:

        request.Model = "ft:gpt-3.5-turbo-0613:personal::8Eg09Cm3";

For example:

        request.Model = "gpt-3.5-turbo";

The current model is a fine tuned model I created using these instructions from OpenAI: https://platform.openai.com/docs/guides/fine-tuning/preparing-your-dataset. I did this in a Google collaboratory Python script, using a text file in the same collaboratory notebook.

After the above steps are done, just open the project in Unity.
