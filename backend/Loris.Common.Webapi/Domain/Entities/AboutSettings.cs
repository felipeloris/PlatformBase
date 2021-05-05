using System;
using System.Reflection;

namespace Loris.Common.Webapi.Domain.Entities
{
    public class AboutSettings
    {
        public string Title { get; }

        public string FullName { get; }

        public string Description { get; }

        public Version Version { get; }

        public AboutSettings()
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblyName = assembly.GetName();
            Title = assemblyName.Name;
            FullName = assemblyName.FullName;
            Version = assemblyName.Version;

            var description = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
            Description = description?.Description;
        }
    }
}