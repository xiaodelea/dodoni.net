﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dodoni.MathLibrary.Basics.LowLevel.Native {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AcmlResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AcmlResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Dodoni.MathLibrary.Basics.LowLevel.Native.AcmlResources", typeof(AcmlResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Blum-Blum-Shub pseudo random number generator is cryptologically secure under the assumption that the quadratic residuosity problem is intractable..
        /// </summary>
        internal static string BlumBlumShub {
            get {
                return ResourceManager.GetString("BlumBlumShub", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The L&apos;Ecuyer&apos;s combined recursive generator combines two multiple recursive generators..
        /// </summary>
        internal static string Ecuyer {
            get {
                return ResourceManager.GetString("Ecuyer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use a standard approach..
        /// </summary>
        internal static string Generation_StandardApproach {
            get {
                return ResourceManager.GetString("Generation_StandardApproach", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use AMDs ACML library, file name &quot;{0}&quot;..
        /// </summary>
        internal static string LibraryDescription {
            get {
                return ResourceManager.GetString("LibraryDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Mersenne Twister is a twisted generalized feedback shift register generator..
        /// </summary>
        internal static string MersenneTwister {
            get {
                return ResourceManager.GetString("MersenneTwister", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The NAG basic generator is a linear congruential generator..
        /// </summary>
        internal static string NAG {
            get {
                return ResourceManager.GetString("NAG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Wichmann-Hill base generator uses a combination of four linear congruential generators..
        /// </summary>
        internal static string WichmannHill {
            get {
                return ResourceManager.GetString("WichmannHill", resourceCulture);
            }
        }
    }
}
