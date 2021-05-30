namespace FindPathProc.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Constructor, initialize components
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new StartPage();
        }

    }
}