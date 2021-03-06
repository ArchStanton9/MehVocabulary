﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Translator
{
    public class Note
    {
        public string Word { get; set; }
        [JsonProperty("id")]
        public int ID { get; private set; }
        public string Translation { get; set; }
        [JsonProperty("ts")]
        public string Transcription { get; set; }
        [JsonProperty("def")]
        public List<Defenition> Defenitions { get; private set; }
        [JsonProperty("date")]
        public DateTime TranslationDate { get; private set; }

        public Note(string word)
        {
            ID = GetHashCode();
            TranslationDate = DateTime.Now;
            Word = word.ToLower();
            if (Defenitions == null)
                Defenitions = Translator.Translate(word);
            if (Defenitions?.Count != 0)
            {
                Translation = Defenitions[0].Translations[0].Text;
                Transcription = Defenitions[0].Transcription;
            }
            else
                Translation = Word;
        }

        public Note()
        {
            //default ctror for serialization
        }
    }

    public class Word
    {
        public string Text { get; set; }
        public string Pos { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public class Defenition : Word
    {
        [JsonProperty("tr")]
        public List<Translation> Translations { get; set; }
        [JsonProperty("ts")]
        public string Transcription { get; set; }
    }

    public class Translation : Word
    {
        [JsonProperty("syn")]
        public List<Word> Synonyms { get; set; }
        [JsonProperty("mean")]
        public List<Word> Meaning { get; set; }
        [JsonProperty("ex")]
        public List<Example> Examples { get; set; }
    }

    public class Example
    {
        public string Text { get; set; }
        [JsonProperty("Tr")]
        public List<Translation> Translations { get; set; }
    }
}
