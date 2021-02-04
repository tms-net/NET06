using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TMS.NET06.Lesson14.Serialization
{
	public class JsonClass
    {
        [JsonPropertyName("stringProp")]
	    public string StringProp { get; set; }
	    public int numProp { get; set; }
	    public object objProp { get; set; }
	    public object[] arrayProp { get; set; }
        public bool boolProp { get; set; }
        public object unknownProp { get; set; }
    }

    class Program
    {
        // TODO: review how JsonSerializer works with objects
        // - JsonSerializerOptions (case sensitivity, WriteIndented)
        // - work with names (use attributes (JsonPropertyName, JsonIgnore, JsonInclude), known/unknown props)
        // - unknown objects
        // - array with different types (JsonElement, etc.)
        // - serialization exceptions
        

        static void Main(string[] args)
        {
            var json = @"
{
    ""stringProp"": ""this is a string"",
    ""numProp"": 100,
    ""objProp"": {
        ""innerProp"": ""this is inner string""
    },
    ""boolProp"": true,
    ""arrayProp"": [1, ""string"", { ""arrayObjProp"": ""this is string in array""}]
}";

            Console.WriteLine(json);

            try
            {
	            var obj = JsonSerializer.Deserialize<JsonClass>(json);
	            obj.StringProp = "for serialization";
                Console.WriteLine(JsonSerializer.Serialize(obj));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
