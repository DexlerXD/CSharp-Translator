# CSharp-Translator
This is a little c# console app which works on a google api.
It is currently working as a console app, but I have plans to use the  method ` TranslateContent() ` in other programs or projects. Feel free to use it in your work, but please, mention this repo or me, thanks!

## How does it works?
I have found a little google api workaround, which allowed me to automatically translate some text.

```
https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}
```
Where `{0}` is language to translate from, `{1}` is language to translate to and `{2}` is the content.
For this program to run, you need to create two txt files, named "input" and "output". In "input.txt" you write the content to translate. The output of the program will be in the
"output.txt". Also, you need to pass the languages in two-letter codes.

### Example

To translate a phrase "Hello!" from english to russian, you need to write down the phrase in the "input.txt".
When program is asking for language to translate from, you write `en`.
Same with the language fot language to translate to, you need to write `ru`.
After all of this, the result of translation will be in "output.txt'.
