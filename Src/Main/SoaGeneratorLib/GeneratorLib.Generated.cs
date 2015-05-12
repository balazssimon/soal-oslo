using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // Inheritace from 'Generator<List<object>, GeneratorContext>' and constructor is only generated into the main file.
    public partial class GeneratorLib
    {
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\GeneratorLib.mcg"
            public string Generated_FirstLetterLow(string s)
            {
                return s.Substring(0, 1).ToLower() + s.Substring(1);
            }
            
            public string Generated_FirstLetterUp(string s)
            {
                return s.Substring(0, 1).ToUpper() + s.Substring(1);
            }
            
            public string Generated_GetUri(Namespace ns)
            {
                return GeneratorLibExtensions.GetUri(ns);
            }
            
            public string Generated_GetUriWithSlash(Namespace ns)
            {
                return GeneratorLibExtensions.GetUriWithSlash(ns);
            }
            
            public string Generated_GetPackage(Namespace ns)
            {
                return GeneratorLibExtensions.GetPackage(ns);
            }
            
            public string Generated_IsNillableType(Type t)
            {
                if (t is BuiltInType)
                {
                    if (t == BuiltInType.String)
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }
                }
                else if (t is EnumType)
                {
                    return "false";
                }
                else
                {
                    return "true";
                }
            }
            
            #endregion
        }
    }
    
