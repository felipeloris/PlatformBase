using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Loris.Common.Domain
{
    public static class EnumHelper
    {
        public static string GetEnumSeq<T>(List<T> list, char delimit = ';')
        {
            var strResult = string.Empty;

            if (list != null)
            {
                strResult = list.Aggregate(strResult, (current, status)
                    => current + (Convert.ToInt32(status) + delimit.ToString()));
            }

            return strResult;
        }

        public static string GetAbbreviationFromEnum(Enum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumInfoAttribute), false)
                .SingleOrDefault() as EnumInfoAttribute;
            return attribute == null ? value.ToString() : attribute.Abbreviation;
        }

        public static string GetDictionaryFromEnum(Enum value, ResourceManager resManager)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumInfoAttribute), false)
                .SingleOrDefault() as EnumInfoAttribute;
            var ret = attribute?.Dictionary;

            if (ret != null && resManager != null)
                ret = resManager.GetString(ret, CultureInfo.CurrentCulture) ?? ret;

            return ret;
        }

        public static string GetDescriptionFromEnum(Enum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumInfoAttribute), false)
                .SingleOrDefault() as EnumInfoAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static List<EnumModel> GetEnumModel<T>(ResourceManager resManager = null)
        {
            var list = ((T[])Enum.GetValues(typeof(T))).Select(c => new EnumModel()
            {
                Id = Convert.ToInt32(c),
                Name = c.ToString(),
                Abbreviation = GetAbbreviationFromEnum(c as Enum),
                Description = GetDescriptionFromEnum(c as Enum),
                Dictionary = GetDictionaryFromEnum(c as Enum, resManager)
            }).ToList();

            return list;
        }

        public static T GetEnumValueFromDescriptionAttribute<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException();
            }
            var fields = type.GetFields();
            var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
                (f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                      .Description == description);
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class EnumInfoAttribute : Attribute
    {
        private string abbreviation;
        private string dictionary;
        private string description;

        public EnumInfoAttribute(string dictionary)
            : this(dictionary, null, null)
        {
        }

        public EnumInfoAttribute(string dictionary, string abbreviation)
            : this(dictionary, abbreviation, null)
        {
        }

        public EnumInfoAttribute(string dictionary, string abbreviation, string description)
        {
            this.dictionary = dictionary;
            this.abbreviation = abbreviation;
            this.description = description;
        }

        public virtual string Abbreviation
        {
            get { return abbreviation; }
        }

        public virtual string Dictionary
        {
            get { return dictionary; }
        }

        public virtual string Description
        {
            get { return description; }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            var enumInfoAttribute = obj as EnumInfoAttribute;
            if (enumInfoAttribute != null)
            {
                return enumInfoAttribute.dictionary.Equals(this.dictionary) &&
                    enumInfoAttribute.abbreviation.Equals(this.abbreviation) &&
                    enumInfoAttribute.description.Equals(this.description);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.dictionary.GetHashCode();
        }
    }

    public class EnumModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public string Dictionary { get; set; }
    }
}
