Package: Cambia.BaseN 
Author: Steve Lautenschlager
Website: www.CambiaResearch.com
Platform: .NET 4.0 C# Class Library


Summary:
    * Parse and Convert Numbers to Different Bases Using Built-In Alphabets or Custom Alphabets.


Features: 
    * Parse number strings of different bases into BigInteger, Guid, long, ulong, int, uint.
    * Represent number types (BigInteger, Guid, long, ulong, int, uint) in different bases
    * Convert numbers in different bases and alphabets from one to the other.
    * Use a standard base alphabet or define your own custom alphabet.
    * Extension methods for BigInteger, Guid, long, ulong, int, uint.
    * 500+ unit tests


Documentation:
    * Intro to Number Bases and the Cambia.BaseN Package
        * https://www.cambiaresearch.com/articles/241022
    * Cambia.BaseN Documentation and Code Samples
        * https://www.cambiaresearch.com/articles/969077


LICENSE:
--------

The author retains copyright and grants you the right to use based on the following license:

    MS-Pl - Microsoft Public License


CHANGE LOG:
-----------

V2.2.3 - Signed assembly with a strong name
V2.2.0 - Added BaseNAlphabet.GetMaxRadix(StandardAlphabetPattern a) plus units tests for stuff related to StandardAlphabetPattern
V2.1.0 - Added StandardAlphabetPatterns enumeration and BaseNAlphabet.GetAlphabet(int radix, StandardAlphabetPattern a)
V2.0.1 - Renamed the BaseN class to BaseConverter.  Aside from being a more clear description of what the class provides 
the BaseN class name was the same as the Cambia.BaseN namespace name and this did not play nice with intellisense.  
Because this is a breaking change, I have incremented the major version number.
