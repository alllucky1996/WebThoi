using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common.Enums
{
    //http://www.codeproject.com/Articles/662968/Creating-a-DropDownList-for-Enums-in-ASP-NET-MVC
    //http://paulthecyclist.com/2013/05/24/enum-dropdown/

    //Tham khảo link này để order:
    //http://stackoverflow.com/questions/12768593/c-sharp-enum-ignores-names-order
    //http://stackoverflow.com/questions/13125689/order-properties-of-object-by-value-of-customattribute
    public static class EnumHelpers
    {
        /// <summary>
        /// Lấy về Description Attribule của 1 đối tượng enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue, object[] parameters = null)
        {
            if (null == enumValue) return "";
            Type t = enumValue.GetType();
            FieldInfo f = t.GetField(enumValue.ToString());
            object[] attribs = f.GetCustomAttributes(false);
            var da = attribs.FirstOrDefault(a => a is DescriptionAttribute) as DescriptionAttribute;
            //Nếu Attribute không được khai báo Description thì trả về tên Name
            if (null == da) return f.Name;
            return (null != parameters && parameters.Length > 0) ? String.Format(da.Description, parameters) : da.Description;
        }
        /// <summary>
        /// Lấy về Description Attribule của 1 đối tượng enum chưa biết kiểu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumValue, object[] parameters = null)
        {
            if (null == enumValue) return "";
            Type t = enumValue.GetType();
            FieldInfo f = t.GetField(enumValue.ToString());
            object[] attribs = f.GetCustomAttributes(false);
            var da = attribs.FirstOrDefault(a => a is DescriptionAttribute) as DescriptionAttribute;
            if (null == da) return f.Name;
            return (null != parameters && parameters.Length > 0) ? String.Format(da.Description, parameters) : da.Description;
        }

        /// <summary>
        /// Gets the description of an enumerator
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        ///using Region regionEnumerator = Region.KwaZuluNatal;
        ///string regionDescription = regionEnumerator.GetDescription();
        public static string GetDescription(this Enum enumerator)
        {
            //get the enumerator type
            Type type = enumerator.GetType();

            //get the member info
            MemberInfo[] memberInfo = type.GetMember(enumerator.ToString());

            //if there is member information
            if (memberInfo != null && memberInfo.Length > 0)
            {
                //we default to the first member info, as it's for the specific enum value
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                //return the description if it's found
                if (attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            //if there's no description, return the string value of the enum
            return enumerator.ToString();
        }

        /// <summary>
        /// Lấy về giá trị của 1 đối tượng enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int GetValue<T>(this T enumValue)
        {
            return Convert.ToInt16(enumValue);
        }

        //Lấy danh sách enum theo tên của attribute
        //using var enums = Common.EnumHelpers.FilterEnumWithAttributeOf<Entities.Enums.ModuleEnum, Common.CustomAttributes.ModuleGroupAttribute>();
        public static IEnumerable<TEnum> FilterEnumWithAttributeOf<TEnum, TAttribute>()
            where TEnum : struct
            where TAttribute : class
        {
            foreach (var field in
                typeof(TEnum).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static))
            {

                if (field.GetCustomAttributes(typeof(TAttribute), false).Length > 0)
                    yield return (TEnum)field.GetValue(null);
            }

        }
        /// <summary>
        /// Gets the enumerator from the description passed in
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        ///using string regionDescription = "Northern Cape";
        ///Region regionEnumerator = regionDescription.GetEnumFromDescription<Region>();
        public static T GetEnumFromDescription<T>(this string description)
        {
            //get the member info of the enum
            MemberInfo[] memberInfos = typeof(T).GetMembers();

            if (memberInfos != null && memberInfos.Length > 0)
            {
                //loop through the member info classes
                foreach (MemberInfo memberInfo in memberInfos)
                {
                    //get the custom attributes of the member info
                    object[] attributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    //if there are attributes
                    if (attributes != null && attributes.Length > 0)
                        //if the description attribute is equal to the description, return the enum
                        if (((DescriptionAttribute)attributes[0]).Description == description)
                            return (T)Enum.Parse(typeof(T), memberInfo.Name);
                }
            }

            //this means the enum was not found from the description, so return the default
            return default(T);
        }
    }
}
