using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Savory.TransformService.Biz.Test
{
    [TestClass]
    public class CodeEngineTest
    {

        [TestMethod]
        public void Test1()
        {
            CodeEngine engine = new CodeEngine();

            var template = "<#for(var i=0;i<3;i++){#><#=i#><#}#>";

            var result = engine.ToCode(template, null, null);

            Assert.AreEqual("012", result.Output);

            Console.WriteLine(result.Output);
        }

        [TestMethod]
        public void Test2()
        {
            CodeEngine engine = new CodeEngine();

            var template = "<#=this.Item.Name#>";

            var result = engine.ToCode(template, null, "{\"Name\":\"zhang\"}");

            Assert.AreEqual("zhang", result.Output);
        }

        [TestMethod]
        public void Test3()
        {
            CodeEngine engine = new CodeEngine();

            var template = "<#=this.Item.Name#>, <#=this.Item.Age#>, <#=this.Item.IsBoy#>";

            var result = engine.ToCode(template, "public class TT { public string Name { get; set; } public int Age { get; set; } public bool IsBoy { get; set; } } public TT Item { get;set; }", "{\"Name\":\"zhang\", \"Age\": 18, \"IsBoy\": true}");

            Assert.AreEqual("zhang, 18, True", result.Output);
        }
    }
}
