using Loris.Common.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Loris.Common.Test.Domain
{
    [TestClass]
    public class BasicModelTest
    {
        private List<HierarchyModel> models = new List<HierarchyModel>();

        public BasicModelTest()
        {
            models.Add(new HierarchyModel { Id = 1, Name = "teste 1", ParentId = 0 });
            models.Add(new HierarchyModel { Id = 2, Name = "teste 2", ParentId = 1 });
            models.Add(new HierarchyModel { Id = 3, Name = "teste 3", ParentId = 1 });
            models.Add(new HierarchyModel { Id = 4, Name = "teste 4", ParentId = 0 });
            models.Add(new HierarchyModel { Id = 5, Name = "teste 5", ParentId = 0 });
            models.Add(new HierarchyModel { Id = 6, Name = "teste 6", ParentId = 5 });
            models.Add(new HierarchyModel { Id = 7, Name = "teste 7", ParentId = 6 });
            models.Add(new HierarchyModel { Id = 8, Name = "teste 8", ParentId = 5 });
            models.Add(new HierarchyModel { Id = 9, Name = "teste 9", ParentId = 6 });
            models.Add(new HierarchyModel { Id = 10, Name = "teste 10", ParentId = 2 });
            models.Add(new HierarchyModel { Id = 11, Name = "teste 11", ParentId = 4 });
            models.Add(new HierarchyModel { Id = 12, Name = "teste 12", ParentId = 11 });
            models.Add(new HierarchyModel { Id = 13, Name = "teste 13", ParentId = 11 });
            models.Add(new HierarchyModel { Id = 14, Name = "teste 14", ParentId = 13 });
            models.Add(new HierarchyModel { Id = 15, Name = "teste 15", ParentId = 13 });
            models.Add(new HierarchyModel { Id = 16, Name = "teste 16", ParentId = 13 });
            models.Add(new HierarchyModel { Id = 17, Name = "teste 17", ParentId = 14 });
            models.Add(new HierarchyModel { Id = 18, Name = "teste 18", ParentId = 15 });
            models.Add(new HierarchyModel { Id = 19, Name = "teste 19", ParentId = 18 });
        }

        [TestMethod]
        public void TestFullHierarchyInName()
        {
            try
            {
                var basics = HierarchyModel.GetFullHierarchyInName(models);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
