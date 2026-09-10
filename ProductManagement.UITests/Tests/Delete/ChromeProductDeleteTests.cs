using ProductManagement.UITests.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Tests.Delete
{
    public class ChromeProductDeleteTests : ProductDeleteTests
    {
        public ChromeProductDeleteTests() : base(BrowserType.Chrome) { }
    }


}
