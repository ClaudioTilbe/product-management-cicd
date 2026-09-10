using ProductManagement.UITests.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Tests.Create
{
    public class ChromeProductCreateTests : ProductCreateTests
    {
        public ChromeProductCreateTests() : base(BrowserType.Chrome) { }
    }


}
