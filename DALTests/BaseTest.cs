#define  TEST
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Core;
using NUnit.Framework;


namespace Sasha.Lims.Tests.TestCore
{
	public class BaseTest
    {


		[SetUp]
		public static void TestSetUp()
		{        
			AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Trace.Listeners.Add(new ConsoleTraceListener());
            UnitOfWork.RemembeNewEntity=true;
		}

		[TearDown]
		public static void TearDown()
		{
			UnitOfWork.DeleteTestAdded();

		}



		static internal UserDTO _user = new UserDTO() { Id = "testUser", UserId = 55 };
		#region UOW

		private IUnitOfWorkEx _uat;
		public IUnitOfWorkEx UOW
		{
			get
			{
				if (_uat == null)
				{
					_uat = new UnitOfWork();
				}
				return _uat;
			}
			set => _uat = value;
		}

		#endregion





	}
}
