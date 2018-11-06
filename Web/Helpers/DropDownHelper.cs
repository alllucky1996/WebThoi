using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.CustomAttributes;
using Common.Enums;

namespace Web.Helpers
{
    public static class DropDownHelper
    {
        public static SelectList ToSelectList<T>(this List<T> items, object objectSelectedValue = null) where T : class
        {
            string dataTextField = "", dataValueField = "";
            string selectedValue = objectSelectedValue != null ? objectSelectedValue.ToString() : "";
            var dropdownAttribute = typeof(T).GetCustomAttributes(typeof(DropDownAttribute), true).FirstOrDefault() as DropDownAttribute;
            if (dropdownAttribute != null)
            {
                dataTextField = dropdownAttribute.TextField;
                dataValueField = dropdownAttribute.ValueField;
            }
            return new SelectList(items, dataValueField, dataTextField, selectedValue);
        }
        public static SelectList ToSelectListEnum<T>(this T enumObj) where T : struct, IComparable, IFormattable, IConvertible
        {
            var values = from T e in Enum.GetValues(typeof(T))
                         select new { Id = Convert.ToInt16(e).ToString(), Name = e.GetDescription() };
            return new SelectList(values, "Id", "Name", Convert.ToInt16(enumObj).ToString());
        }
        public static IEnumerable<SelectListItem> ToSelectListItemEnum<T>(this T enumObj)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = Convert.ToInt16(v).ToString(),// v.ToString(),
                Selected = Convert.ToInt16(v).ToString() == Convert.ToInt16(enumObj).ToString()
            });
        }
        public static SelectList ToSelectListEnumRaw<T>(this T enumObj) where T : struct, IComparable, IFormattable, IConvertible
        {
            var values = from T e in Enum.GetValues(typeof(T))
                         select new { Id = e.ToString(), Name = e.GetDescription() };
            return new SelectList(values, "Id", "Name", enumObj.ToString());
        }
        public static IEnumerable<SelectListItem> ToSelectListItemEnumRaw<T>(this T enumObj)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString(),// v.ToString(),
                Selected = v.ToString() == enumObj.ToString()
            });
        }
    }
}