using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatcomVS.Tasks;

namespace WatcomVS.Tests
{
    [TestClass]
    public class ToolWrapperTest
    {
        [TestMethod]
        public void TestSimpleToolDLL()
        {
            var tool = new WatcomToolWrapper( "wcc", @"c:\watcom\binnt\wcc.exe", @"c:\watcom\binnt\wccd.dll" );
            tool.Run( "nul" );
        }
    }
}
