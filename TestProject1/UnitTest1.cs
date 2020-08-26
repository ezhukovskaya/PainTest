using System.IO;
using System.Linq;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;


namespace TestProject1
{
    public class Tests
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string PaintAppId = @"C:\Windows\System32\mspaint.exe";
        public Application application = null;
        public Window paintWindow = null;

        [Test]
        public void Test1()
        {
            application = Application.Launch(PaintAppId);
            paintWindow = application.GetWindow("Untitled - Paint");
            MenuBar menuBar = paintWindow.MenuBar;
            Assert.IsNotNull(menuBar, "Paint is not open");
            Menu open = menuBar.MenuItem("File", "Open");
            open = menuBar.MenuItemBy(SearchCriteria.ByText("File"), SearchCriteria.ByText("Open"));
            open.Click();
            Window childWindow = null;
            if(paintWindow.ModalWindows().Any())
            {
                childWindow = paintWindow.ModalWindow("child");
                TextBox textBox = childWindow.Get<TextBox>("currentValueTextBox");
                textBox.Text = "Some image address";
                Button openButton = childWindow.Get<Button>("Open");
                openButton.Click();
                Assert.IsNull(childWindow);
            }
            Button selectExt = paintWindow.Get<Button>(SearchCriteria.ByText("Locator to the extended select"));
            selectExt.Click();
            Button selectAll = paintWindow.Get<Button>(SearchCriteria.ByText("Select All"));
            selectAll.Click();
            Button cut = paintWindow.Get<Button>(SearchCriteria.ByText("Cut"));
            cut.Click();
            application.Close();
        }
    }
}