using System.Windows.Forms;

namespace Appium.MainWindow
{
    /// <summary>the main form</summary>
    public partial class MainForm : Form
    {
        private Model _Model;
        private Controller _Controller;

        /// <summary>constructor</summary>
        public MainForm()
        {
            _Model = new Model(this);
            _Controller = new Controller(this._Model);
            InitializeComponent();

        }
    }
}
