using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatcomVS.Tasks;

namespace WatcomVS.Tests
{
    [TestClass]
    public class WatcomToolWrapperTest
    {
        [TestMethod]
        public void TestSimpleToolDLL()
        {
            var dll = new WatcomToolWrapper("wcc", @"c:\watcom\binnt\wcc.exe" );
            dll.Run( "nul" );
        }
    }
}
