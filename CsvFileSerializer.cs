using System.Text;

namespace Reflection
{
    public static class CsvFileSerializer<T> where T : class
    {
        public static string Serialize(T obj)
        {
            var sb = new StringBuilder();

            List<string> head = new ();
            foreach (var property in typeof(T).GetFields())
                head.Add($"{property.Name}");
            sb.AppendLine(string.Join(";", head));

            List<string> body = new ();
            foreach (var property in typeof(T).GetFields())
                body.Add($"{typeof(T).GetField(property.Name)!.GetValue(obj)}");

            sb.AppendLine(string.Join(";", body));

            return sb.ToString();
        }

        public static T Deserialize(string str)
        {
            var massString = str.Split("\r\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var headfields = massString[0].Split(";");
            var stringValue = massString[1].Split(";");

            var result = Activator.CreateInstance(typeof(T));

            for (int j = 0; j < headfields.Length; j++)
            {
                var fieldKey = typeof(T).GetField(headfields[j]);
                var fieldType = fieldKey!.FieldType;
                fieldKey.SetValue(result, Convert.ChangeType(stringValue[j], fieldType));
            }

            return (T)result!;
        }

    }
}