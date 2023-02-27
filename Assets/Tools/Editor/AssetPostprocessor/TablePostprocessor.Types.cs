using System.Collections.Generic;

public partial class TablePostprocessor
{
    public static Dictionary<string, string> TypesPrase = new Dictionary<string, string>()
    {
        { "int", "ParseInt" },
        { "float", "ParseFloat" },
        { "string", "ParseString" },
        { "bool", "ParseBool(string data)" },
        { "int[]", "ParseArray<int>" },
        { "float[]", "ParseArray<float>" },
        { "string[]", "ParseArray<string>" },
        { "bool[]", "ParseArray<bool>" },
        { "Desc", "" }
    };
}