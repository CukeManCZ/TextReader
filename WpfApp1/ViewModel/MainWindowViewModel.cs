using Microsoft.Win32;
using WpfApp1.Services;

namespace WpfApp1.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
		#region Properties
		TextDocumentService m_textDocumentService;

		private string m_currentText;

		public string CurrentText
		{
			get { return m_currentText; }
			set { m_currentText = value;
                OnPropertyChanged();
            }
		}

		private int m_currentPage;

		public int CurrentPage
		{
			get { return m_currentPage; }
			set { m_currentPage = value;
				OnPropertyChanged();
			}
		}

		private int m_maxPage;

		public int MaxPage
		{
			get { return m_maxPage; }
			set { m_maxPage = value;
                OnPropertyChanged();
            }
		}

		private int m_fontSize;

		public int FontSize
		{
			get { return m_fontSize; }
			set { m_fontSize = value;
				OnPropertyChanged();
			}
		}

		#endregion


		#region Commands
		public RelayCommand LoadCommand => new RelayCommand(execute => { LoadFile(); });
		public RelayCommand NextPageCommand => new RelayCommand(execute => { NextPage(); });
        public RelayCommand PreviousPageCommand => new RelayCommand(execute => { PreviousPage(); });
        public RelayCommand FirstPageCommand => new RelayCommand(execute => { FirstPage(); });
        public RelayCommand LastPageCommand => new RelayCommand(execute => { LastPage(); });
        #endregion

        public MainWindowViewModel(TextDocumentService textDocumentService)
		{
			FontSize = 16;
			m_textDocumentService = textDocumentService;
			m_textDocumentService.OnText_Update += OnTextUpdate;
		}

        public void UpdateViewPort(double width, double height)
        {
            m_textDocumentService.SetViewPort(
                FontSize,
                (int)width,
                (int)height
            );
        }

        private void OnTextUpdate(string[] currentText, int currentPage, int maxPage)
        {
			if (currentText == null)
				return;

			string newText = "";
			foreach(string current in currentText)
                newText += current;

			CurrentText = newText;
			CurrentPage = currentPage;
			MaxPage = maxPage;
        }

        private void LoadFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "TXT file | *.txt",
                Title = "Please pick txt file..."
            };

            bool? success = fileDialog.ShowDialog();
			if (success == true)
			{
				m_textDocumentService.LoadText(fileDialog.FileName);
			}
        }

		private void NextPage()
		{

		}

		private void PreviousPage()
		{

		}

		private void FirstPage()
		{

		}

		private void LastPage()
		{

		}
    }
}
