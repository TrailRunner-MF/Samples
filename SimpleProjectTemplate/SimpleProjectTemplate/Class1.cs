using System;
using System.Diagnostics;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;

namespace $safeprojectname$
{
	public class Class1
	{
		public void Hello(string personName)
		{
			Console.WriteLine("Hello " + personName + ".");
		}
	}
}
