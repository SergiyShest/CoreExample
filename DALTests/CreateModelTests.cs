using NUnit.Framework;
using NUnit.Framework.Internal;
using Sasha.Lims.Tests.TestCore;
using Entity;
using System.Dynamic;
using DAL.Core;
using Newtonsoft.Json;

namespace Tests
{
    [TestFixture]
    //создание модели любого типа
    public class CreateModelTests : BaseTest
    {

        [Test]
        public void CreateModel()
        {var record = new User();
        var dyn = createModel(record);
        var json = JsonConvert.SerializeObject(dyn);
        }

        private static object createModel(object record)
        {
            Type t= record.GetType();
            var props =t.GetProperties();
            Dictionary<string, object> properties = new Dictionary<string, object>();
            foreach (var prop in props)
            {

                dynamic propVal = new ExpandoObject();
                var p = prop.PropertyType;
                if (p.IsNullable()) p = Nullable.GetUnderlyingType(p);
                switch (p.Name)
                {
                    case "String": propVal.type = "text"; break;
                    case "DateOnly":
                    case "DateTime": propVal.type = "date"; break;
                    case "Boolean": propVal.type = "boolean"; break;
                    default:
                        if (p.IsNumericType())
                        {
                            propVal.type = "number";
                        }
                        break;

                }
                if (propVal.type == null)
                {
                    throw new Exception("не определен тип " + p.Name);
                }
                propVal.value = prop.GetValue(record);
                properties.Add(prop.Name, propVal);
            }
            DynObject dyn = new DynObject(properties);
            return dyn;
        }
    }
}
