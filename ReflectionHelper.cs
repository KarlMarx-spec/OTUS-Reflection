namespace Reflection
{
    public class ReflectionHelper<T>
    {
        public string Serialize(T obj)
        {
            var fields = new List<string>();

            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                fields.Add(field.Name + "=" + field.GetValue(obj));
            }

            return string.Join(',', fields);
        }

        public object? Deserialize(string str)
        {
            var obj = Activator.CreateInstance(typeof(T));
            var fields = str.Split(',');

            foreach (var f in fields)
            {
                var mas = f.Split('=');
                var field = typeof(T).GetField(mas[0]);
                field!.SetValue(obj, Convert.ChangeType(mas[1], field.FieldType));
            }

            return obj;
        }
    }
}
