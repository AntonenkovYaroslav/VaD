using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Spire.Doc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;




namespace VaDAlpha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Подписка на обработчики событий  
            
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Стили именования", Justification = "<Ожидание>")]
        private void buttonVidSelectPath_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxVid.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonVidLoadPath_Click(object sender, EventArgs e)
        {
            treeViewVid.Nodes.Clear();
            string selectedPath = textBoxVid.Text;

            if (Directory.Exists(selectedPath))
            {
                // Получаем все папки в выбранной директории  
                var folders = Directory.GetDirectories(selectedPath);

                // Перебираем каждую папку  
                foreach (var folder in folders)
                {
                    // Создаем узел для папки  
                    var folderNode = new TreeNode(Path.GetFileName(folder)) { Tag = folder };
                    treeViewVid.Nodes.Add(folderNode);

                    // Получаем видеофайлы в текущей папке  
                    var files = Directory.GetFiles(folder, "*.*")
                                         .Where(f => f.EndsWith(".mp4") || f.EndsWith(".avi") || f.EndsWith(".mkv") || f.EndsWith(".mpg") || f.EndsWith(".mov") || f.EndsWith(".mxf"));

                    // Добавляем файлы как дочерние узлы  
                    foreach (var file in files)
                    {
                        folderNode.Nodes.Add(new TreeNode(Path.GetFileName(file)) { Tag = file });
                    }
                }

                // Проверяем наличие видеофайлов в выбранной директории  
                if (!treeViewVid.Nodes.Cast<TreeNode>().Any(n => n.Nodes.Count > 0))
                {
                    MessageBox.Show("Нет доступных видеофайлов в выбранной директории.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выбранная папка не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeViewVid_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                string videoPath = e.Node.Tag.ToString();
                axWindowsMediaPlayer1.URL = videoPath;
            }
        }

        private void buttonTxtSelectPath_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxTxt.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonTxtLoadPath_Click(object sender, EventArgs e)
        {
            treeViewTxt.Nodes.Clear(); // Очистка TreeView перед загрузкой новых данных  
            string selectedPath = textBoxTxt.Text;

            if (Directory.Exists(selectedPath))
            {
                // Создаем корневой узел для выбранной папки  
                var rootNode = new TreeNode(Path.GetFileName(selectedPath)) { Tag = selectedPath };
                treeViewTxt.Nodes.Add(rootNode);

                // Проверяем наличие текстовых файлов в корневой директории  
                var rootFiles = Directory.GetFiles(selectedPath, "*.*")
                                         .Where(f => f.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase)).ToList();

                // Добавляем файлы, если они найдены  
                foreach (var file in rootFiles)
                {
                    rootNode.Nodes.Add(new TreeNode(Path.GetFileName(file)) { Tag = file });
                }

                // Получаем все папки в выбранной директории  
                var folders = Directory.GetDirectories(selectedPath);

                // Перебираем каждую папку  
                foreach (var folder in folders)
                {
                    // Создаем узел для папки  
                    var folderNode = new TreeNode(Path.GetFileName(folder)) { Tag = folder };
                    rootNode.Nodes.Add(folderNode); // Добавляем как дочерний узел к корневому узлу  

                    // Получаем файлы только в текущей папке  
                    var files = Directory.GetFiles(folder, "*.*")
                                         .Where(f => f.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase)).ToList();

                    // Отладочный вывод  
                    Console.WriteLine($"Найдено {files.Count} текстовых файлов в папке {folder}:");
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                        folderNode.Nodes.Add(new TreeNode(Path.GetFileName(file)) { Tag = file });
                    }

                    // Ищем также вложенные папки и добавляем их  
                    var subFolders = Directory.GetDirectories(folder);
                    foreach (var subFolder in subFolders)
                    {
                        var subFolderNode = new TreeNode(Path.GetFileName(subFolder)) { Tag = subFolder };
                        folderNode.Nodes.Add(subFolderNode);

                        // Получаем файлы в подкаталогах  
                        var subFiles = Directory.GetFiles(subFolder, "*.*")
                                                .Where(f => f.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                                                            f.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                                                            f.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase)).ToList();

                        // Отладочный вывод для подкаталогов  
                        Console.WriteLine($"Найдено {subFiles.Count} текстовых файлов в подкаталоге {subFolder}:");
                        foreach (var subFile in subFiles)
                        {
                            Console.WriteLine(subFile);
                            subFolderNode.Nodes.Add(new TreeNode(Path.GetFileName(subFile)) { Tag = subFile });
                        }
                    }
                }

                // Проверяем наличие текстовых файлов в выбранной директории  
                if (!rootNode.Nodes.Cast<TreeNode>().Any(n => n.Nodes.Count > 0))
                {
                    MessageBox.Show("Нет доступных текстовых файлов в выбранной директории.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Разворачиваем корневой узел, чтобы он был виден  
                rootNode.Expand();
            }
            else
            {
                MessageBox.Show("Выбранная папка не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string OpenXmlReadDocx(string filePath)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
            {
                DocumentFormat.OpenXml.Wordprocessing.Body body = doc.MainDocumentPart.Document.Body;
                return body.InnerText;
            }
        }
        private string ReadDocFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(filePath);
                return document.GetText();
            }
        }
        private string ReadRtfFile(string filepath)
        {
            // Создаем новый документ  
            WordDocument document = new WordDocument();

            try
            {
                // Загружаем RTF файл в документ  
                // Параметр FileFormat.RTF указывает формат входного файла  
                document.Open(filepath, FormatType.Rtf);

                // Получаем текст из документа  
                // Возвращает весь текст документа как строку  
                return document.GetText();
            }
            catch (Exception ex)
            {
                // Обработка исключений  
                MessageBox.Show($"Ошибка при чтении RTF файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            finally
            {
                // Освобождаем все ресурсы, используемые документом  
                document.Close();
            }

        }
        private void treeViewTxt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is string filePath && File.Exists(filePath))
            {
                try
                {
                    string textContent = string.Empty;

                    if (filePath.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        // Читаем текст из .docx файла  
                        textContent = OpenXmlReadDocx(filePath);

                    }
                    else if (filePath.EndsWith(".doc", StringComparison.OrdinalIgnoreCase))
                    {
                        // Читаем текст из .doc файла через Free Spire.Doc  
                        textContent = ReadDocFile(filePath);
                    }
                    else if (filePath.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        textContent = ReadRtfFile(filePath);
                    }

                        richTextBox1.Text = textContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
