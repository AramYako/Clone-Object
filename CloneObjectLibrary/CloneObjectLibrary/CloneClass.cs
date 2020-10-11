using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace CloneObjectLibrary
{
    public class CloneClass : ICloneClass
    {
        public T DeepClone<T>(T source)
        {
            XmlSerializer xmlSerilizer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerilizer.Serialize(textWriter, source);

                using (TextReader tr = new StringReader(textWriter.ToString()))

                    return (T)xmlSerilizer.Deserialize(tr);
            }
        }

        public T ShallowClone<T>(T source)
        {
            var clone = FormatterServices.GetUninitializedObject(source.GetType());
            for (var type = source.GetType(); type != null; type = type.BaseType)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var field in fields)
                    field.SetValue(clone, field.GetValue(source));
            }
            return (T)clone;

        }
    }
}
