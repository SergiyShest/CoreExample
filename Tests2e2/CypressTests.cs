using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System.Diagnostics;
using System.Text;

namespace Tests2e2
{

    [TestFixture]
	public class CypressTestsCommon:CypressTestsBase
	{





		[TestCaseSource(nameof(GetTestsCommon))]
		public void ViewTests(string command )
		{

			var process = TestsUtils.RunProcess("npm run "+command, JsTestDirectory);
			process.WaitForExit();
			int exitCode = process.ExitCode;

			// Проверяем exit code
			if (exitCode != 0)
			{
				Assert.Fail($"Тесты завершились с ошибкой. Exit Code: {exitCode}");
				// Здесь вы можете вызвать Environment.Exit(exitCode), чтобы завершить приложение
			}
			else
			{
				Console.WriteLine("Тесты успешно выполнены.");
			}

        } 

        public static IEnumerable<TestCaseData> GetTestsCommon()
        {
            string jsonConfigPath = Path.Combine( JsTestDirectory,"package.json");
            string jsonConfig = File.ReadAllText(jsonConfigPath);
            JObject packageJson = JObject.Parse(jsonConfig);
            var scripts = packageJson["scripts"];

            if (scripts != null)
            {
                foreach (JProperty script in scripts.Children<JProperty>())//find scripts in packageJson section scripts
                {
					if (!script.Name.Contains("run:e2e.")) continue;
					yield return new TestCaseData(script.Name.ToString()) { TestName = script.Name};
                }
            }
            else
            {
                Console.WriteLine("Scripts not found in the JSON.");
            }
           
        }


    }

	[TestFixture]
	public class CypressTestsWithDb :CypressTestsBase
	{



		[TestCase(nameof(GetTestsCommon))]
		public void ViewTests(string command )
		{
            string workingDirectory = JsTestDirectory;
			var process = TestsUtils.RunProcess("npm run "+command, workingDirectory);
			process.WaitForExit();
			int exitCode = process.ExitCode;
			// Проверяем exit code
			if (exitCode != 0)
			{
				Assert.Fail($"Тесты завершились с ошибкой. Exit Code: {exitCode}");
				// Здесь вы можете вызвать Environment.Exit(exitCode), чтобы завершить приложение
			}
			else
			{
				Console.WriteLine("Тесты успешно выполнены.");
			}

        } 

        public static IEnumerable<TestCaseData> GetTestsCommon()
        {
            string jsonConfigPath = Path.Combine(JsTestDirectory,"package.json");
            string jsonConfig = File.ReadAllText(jsonConfigPath);
            JObject packageJson = JObject.Parse(jsonConfig);
            var scripts = packageJson["scripts"];

            if (scripts != null)
            {
                foreach (JProperty script in scripts.Children<JProperty>())//find scripts in packageJson section scripts
                {
					if (!script.Name.Contains("run:e2e.")) continue;
					yield return new TestCaseData(script.Name.ToString()) { TestName = script.Name};
                }
            }
            else
            {
                Console.WriteLine("Scripts not found in the JSON.");
            }
           
        }


    }

}
