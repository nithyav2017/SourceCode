// See https://aka.ms/new-console-template for more information


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Reflection.Metadata.Ecma335;

string json = File.ReadAllText(@"C:\\Users\\djaya\\Documents\\Nithya\\DotNetCore\\JsonSchemaCreator\\samplejson.json");

JSchema schema = JSchema.Parse(@"{
  'type': 'array',
  'item': {'type':'string'}
}");

JsonSchemaCreator creator= new JsonSchemaCreator ();
creator.Generate(json);
public class JsonSchemaCreator
{
    public JSchema Generate(string json)
    {
        var token = JsonConvert.DeserializeObject<JToken>(json, new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        })!;

        var schema = new JSchema();
        Generate(token, schema, schema, "Anonymous");
        return schema;
    }

    private void Generate(JToken token, JSchema schema, JSchema rootSchema, string typeNameHint)
    {
        if(token == null)
        {
            return;
        }

        switch (token.Type)
        {
            case JTokenType.Object:
                GenerateObject(token, schema, rootSchema);
                break;

            case JTokenType.Array:
                GenerateArray(token, schema, rootSchema, typeNameHint);
                break;

            case JTokenType.Date:
                schema.Type = JsonObjectType.String;
                schema.Format = token.Value<DateTime>() == token.Value<DateTime>().Date
                    ? JsonFormatStrings.Date
                    : JsonFormatStrings.DateTime;
                break;

            case JTokenType.String:
                schema.Type = JsonObjectType.String;
                break;

            case JTokenType.Boolean:
                schema.Type = JsonObjectType.Boolean;
                break;

            case JTokenType.Integer:
                schema.Type = JsonObjectType.Integer;
                break;

            case JTokenType.Float:
                schema.Type = JsonObjectType.Number;
                break;

            case JTokenType.Bytes:
                schema.Type = JsonObjectType.String;
                schema.Format = JsonFormatStrings.Byte;
                break;

            case JTokenType.TimeSpan:
                schema.Type = JsonObjectType.String;
                schema.Format = JsonFormatStrings.Duration;
                break;

            case JTokenType.Guid:
                schema.Type = JsonObjectType.String;
                schema.Format = JsonFormatStrings.Guid;
                break;

            case JTokenType.Uri:
                schema.Type = JsonObjectType.String;
                schema.Format = JsonFormatStrings.Uri;
                break;
        }
         
    }
}
