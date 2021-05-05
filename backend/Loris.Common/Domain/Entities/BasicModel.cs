using System.Collections.Generic;
using System.Linq;

namespace Loris.Common.Domain.Entities
{
    public class BasicModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class HierarchyModel : BasicModel
    {
        public int ParentId { get; set; }

        public List<HierarchyModel> Children { get; set; } = new List<HierarchyModel>();

        public static void SetChildren(List<HierarchyModel> models)
        {
            foreach (var item in models)
                item.Children.Clear();

            foreach (var item in models)
            {
                var parent = models.FirstOrDefault(x => x.Id == item.ParentId);
                if (parent == null)
                {
                    item.ParentId = 0;
                    continue;
                }
                parent.Children.Add(item);
            }
        }

        public static List<BasicModel> GetFullHierarchyInName(List<HierarchyModel> models)
        {
            var ret = new List<BasicModel>();
            SetChildren(models);
            var levelOne = models.Where(x => x.ParentId == 0).ToList();

            foreach (var item in levelOne)
                RecursiveFullHierarchyInName(ret, string.Empty, item);
            return ret;
        }

        private static void RecursiveFullHierarchyInName(List<BasicModel> ret, string name, HierarchyModel model)
        {
            var iName = model.Name;
            if (!string.IsNullOrEmpty(name?.Trim()))
                iName = $"{name} | {model.Name}";
            ret.Add(new BasicModel() { Id = model.Id, Name = iName });

            foreach (var item in model.Children)
                RecursiveFullHierarchyInName(ret, iName, item);
        }
    }
}
