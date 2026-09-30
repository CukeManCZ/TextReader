using System.IO;

namespace WpfApp1.Services
{
    public class TextDocumentService
    {
        private string[] m_currentText;
        private int m_pageCount;
        private int m_currentPage;
        private int m_pageSize; //Number of lines per page

        public event Action<string[], int, int> OnText_Update;

        public TextDocumentService() 
        {
            
        }

        /// <summary>
        /// Updates view port data to update page counts
        /// </summary>
        /// <param name="fontSize"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetViewPort(int fontSize, int width, int height)
        {
            double charWidth = fontSize * 0.6;
            double lineHeight = fontSize * 1.2;

            // Characters that fit horizontally
            int charactersPerLine = Math.Max(1, (int)(width / charWidth));

            // Visual lines that fit vertically
            int linesPerPage = Math.Max(1, (int)(height / lineHeight));

            // Assume an average source line has this many characters
            const int averageLineLength = 80;

            // How many visual lines an average source line occupies
            double wrapFactor = (double)averageLineLength / charactersPerLine;

            // Convert visual lines into source lines
            m_pageSize = Math.Max(
                1,
                (int)(linesPerPage / wrapFactor)
            );

            LoadCurrentText();
        }

        /// <summary>
        /// Load and set stream
        /// </summary>
        /// <param name="path"></param>
        public void LoadText(string path)
        {
            m_currentText = File.ReadAllLines(path);
            m_pageCount = (int)Math.Ceiling(
                (double)m_currentText.Length / m_pageSize
            );
            m_currentPage = 0;
            
            LoadCurrentText();
        }

        /// <summary>
        /// Update current pages text according to position in document (0.0-1.0)
        /// </summary>
        /// <param name="documentPlace"></param>
        public void UpdatePages(float documentPlace)
        {
            m_currentPage = (int) (m_currentText.Count() * documentPlace);
            LoadCurrentText();
        }

        /// <summary>
        /// Update current text based on view port and place in document
        /// </summary>
        private void LoadCurrentText()
        {
            if(m_currentText != null)
                m_pageCount = (int)Math.Ceiling(
                    (double)m_currentText.Length / m_pageSize
                );
            OnText_Update?.Invoke(m_currentText, m_currentPage, m_pageCount);
        }
    }
}
