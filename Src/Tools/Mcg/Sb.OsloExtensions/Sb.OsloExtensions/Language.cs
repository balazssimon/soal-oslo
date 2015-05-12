using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Dataflow;
using System.IO;
using System.Dynamic;
using Microsoft.M;

namespace OsloExtensions
{
    public class Language
    {
        MImage image;
        string moduleName, languageName;
        ParserFactory factory;
    
        internal Language(MImage image, string moduleName, string languageName)
        {
            this.image = image;
            this.moduleName = moduleName;
            this.languageName = languageName;
            this.factory = this.image.ParserFactories[moduleName + "." + languageName];
        }
        
        internal Language(Assembly assm, string moduleName, string languageName)
            : this(MImage.LoadFromAssembly(assm), moduleName, languageName)
        {
        }
         
        public MImage Image { get { return this.image; } }
        public string ModuleName { get { return moduleName; } }
        public string LanguageName { get { return languageName; } }
        
        public static Language Load(MImage image, string moduleName, string languageName)
        {
            return new Language(image, moduleName, languageName);
        }
        
        public static Language Load(Assembly assm, string moduleName, string languageName)
        {
            return new Language(assm, moduleName, languageName);
        }
        
        public static Language LoadFromCurrentAssembly(string moduleName, string languageName)
        {
            var assm = Assembly.GetCallingAssembly();
            return new Language(assm, moduleName, languageName);
        }
        
        public dynamic ParseString(string text)
        {
            return Parse(new StringReader(text));
        }
        
        public dynamic ParseString(string text, ErrorReporter errors)
        {
            return Parse(new StringReader(text), errors);
        }
        
        public dynamic ParseString(string text, ErrorReporter errors, string fileName)
        {
            return Parse(new StringReader(text), errors, fileName);
        }
        
        public dynamic ParseString(string text, ErrorReporter errors, string fileName, SourcePoint startLocation)
        {
            return Parse(new StringReader(text), errors, fileName, startLocation);
        }
        
        public dynamic Parse(TextReader reader)
        {
            return Parse(new TextReaderTextStream(reader), ErrorReporter.Standard);
        }
        
        public dynamic Parse(TextReader reader, ErrorReporter errors)
        {
            return Parse(new TextReaderTextStream(reader), errors);
        }

        public dynamic Parse(TextReader reader, ErrorReporter errors, SourcePoint startLocation)
        {
            return Parse(new TextReaderTextStream(reader), errors, startLocation);
        }

        public dynamic Parse(TextReader reader, ErrorReporter errors, string fileName, SourcePoint startLocation)
        {
            return Parse(new TextReaderTextStream(fileName, reader), errors, startLocation);
        }
        
        public dynamic Parse(TextReader reader, ErrorReporter errors, string fileName)
        {
            return Parse(new TextReaderTextStream(fileName, reader), errors);
        }
        
        public dynamic Parse(ITextStream stream, ErrorReporter errors)
        {
            Parser parser = CreateParser();
            return parser.Parse(stream, errors);
        }

        public dynamic Parse(ITextStream stream, ErrorReporter errors, SourcePoint startLocation)
        {
            Parser parser = CreateParser();
            return parser.Parse(stream, errors, startLocation);
        }
        
        Parser CreateParser()
        {
            return new Parser(factory);
        }
        
    }

}
