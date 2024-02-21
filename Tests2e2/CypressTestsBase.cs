using System.Diagnostics;
using System.Text;

namespace Tests2e2
{
    public class CypressTestsBase
	{

		public static string TestExecDirectory
		{
			get { 
			var execDir = new DirectoryInfo (TestContext.CurrentContext.TestDirectory);
				var projectDir = execDir.Parent.Parent.Parent;
				return projectDir.FullName;
            }
		}

		public static string JsTestDirectory
		{
			get { 
					return Path.Combine(TestExecDirectory, "test_e2e");
            }

		}


        Process mvcProcess;

        [OneTimeSetUp]
        public void Setup()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string command = "dotnet exec mvc.dll --urls=https://localhost:7297";
            var dir = new DirectoryInfo( TestExecDirectory).Parent.FullName;
            string workingDirectory = Path.Combine(dir, @"MVC\bin\Release\net7.0\publish\");
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



    }	

}
