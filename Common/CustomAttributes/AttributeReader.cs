using System;
using System.Linq;

namespace Common.CustomAttributes
{
    public static class AttributeReader
    {
        //Lấy tất cả attributes của Type t
        public static Att[] GetTypeAttributes<Att>(Type t) where Att : Attribute
        {
            Att[] result =
                (Att[])Attribute.GetCustomAttributes(t, typeof(Att));
            return result;
        }

        //Lấy 1 attribute của Type t
        public static Att GetTypeAttribute<Att>(Type t) where Att : Attribute
        {
            Att result =
                (Att)Attribute.GetCustomAttribute(t, typeof(Att));
            return result;
        }
        //Lấy attribute của 1 method
        public static Att GetMethodAttribute<Att>(Type t, string methodName) where Att : Attribute
        {
            var MyMemberInfo = t.GetMethods().Where(o => o.Name == methodName).ToList().FirstOrDefault();
            if (MyMemberInfo == null) return null;
            Att result = (Att)Attribute.GetCustomAttribute(MyMemberInfo, typeof(Att));
            return result;
        }
        public static Att GetPropertyAttribute<Att>(Type t, string propName) where Att : Attribute
        {
            var MyMemberInfo = t.GetProperty(propName);
            if (MyMemberInfo == null) return null;
            Att result = (Att)Attribute.GetCustomAttribute(MyMemberInfo, typeof(Att));
            return result;
        }
        //public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
        //{
        //    return ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, new ViewDataDictionary<TModel>(model)).DisplayName;
        //}
        //public static string GetDisplayName(Type dataType, string fieldName)
        //{
        //    DisplayNameAttribute attr;
        //    attr = (DisplayNameAttribute)dataType.GetProperty(fieldName).GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
        //    if (attr == null)
        //    {
        //        MetadataTypeAttribute metadataType = (MetadataTypeAttribute)dataType.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
        //        if (metadataType != null)
        //        {
        //            var property = metadataType.MetadataClassType.GetProperty(fieldName);
        //            if (property != null)
        //            {
        //                attr = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
        //            }
        //        }
        //    }
        //    return (attr != null) ? attr.DisplayName : String.Empty;
        //}
    }
}
