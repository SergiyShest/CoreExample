using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System.Diagnostics;
using System.Text;

namespace Tests2e2
{
	public class SypressTests
	{

		Process mvcProcess;

		[OneTimeSetUp]
		public void Setup()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			string command = "dotnet exec mvc.dll --urls=https://localhost:7297";
			string workingDirectory = @"e:\source\repos\Core\MVC\bin\Release\net7.0\publish\";
			mvcProcess = TestsUtils.RunProcess(command, workingDirectory);
			Thread.Sleep(1000);

		}
		[OneTimeTearDown]
		public void TearDown()
		{
			if (mvcProcess != null && !mvcProcess.HasExited)
			{
				mvcProcess.Kill(true);
				mvcProcess.WaitForExit(); // Ждем, чтобы убедиться, что процесс завершен
				mvcProcess.Dispose();
			}

		}

		[TestCaseSource(nameof(GetTestsCommon))]
		public void ViewTests(string command )
		{
			string workingDirectory = @"C:\Users\Saergey\source\repos\Core\Tests2e2\test_e2e";
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
            string jsonConfigPath = @"E:\source\repos\Core\Tests2e2\test_e2e\package.json";
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
