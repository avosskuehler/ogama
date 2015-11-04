using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GTApplication
{
    public partial class MessageWindow : Window 
    {
        #region Variables

        private const string forumUrl = "http://forum.gazegroup.org/";
        private const string videoInstructionsUrl = "http://www.youtube.com/watch?v=vgtr3sH4aY8&feature=channel_page";
        private const string gettingStartedUrl = "http://www.gazegroup.org/software/GT_Users_Guide.pdf";
        private string messageText = "";

        #endregion

        #region Constructor

        public MessageWindow()
        {
            InitializeComponent();
            Topmost = true;
        }

        public MessageWindow(string txt)
        {
            InitializeComponent();
            Topmost = true;
            Text = txt;
        }

        #endregion

        #region Get/Set

        public string Text
        {
            get { return messageText; }
            set
            {
                messageText = value;
                TextBlockMessage.Text = messageText;
            }
        }

        #endregion

        #region Private methods

        private void VisitForum(object sender, RoutedEventArgs e)
        {
            Process.Start(forumUrl);
        }

        private void ReadDocumentation(object sender, RoutedEventArgs e)
        {
            Process.Start(gettingStartedUrl);
        }

        private void VideoInstructions(object sender, RoutedEventArgs e)
        {
            Process.Start(videoInstructionsUrl);
        }

        #endregion

        #region WindowManagement

        private void AppClose(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }

        #endregion
    }
}