using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net.Http;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Spire.Doc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using FFmpeg.Native;
using WinSCP;
using System.Diagnostics;




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
            // Инициализация  
            treeViewVid.Nodes.Clear();
            string selectedPath = textBoxVid.Text;
            int totalFolders = 0;
            int totalVideos = 0;
            var stopwatch = Stopwatch.StartNew();

            Debug.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Начало обработки видеофайлов");
            Debug.WriteLine($"Выбранный путь: {selectedPath}");

            try
            {
                if (!Directory.Exists(selectedPath))
                {
                    Debug.WriteLine("Ошибка: директория не существует");
                    MessageBox.Show("Выбранная папка не существует.", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создание корневого узла  
                var rootNode = new TreeNode(Path.GetFileName(selectedPath))
                {
                    Tag = selectedPath,
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };
                treeViewVid.Nodes.Add(rootNode);
                Debug.WriteLine($"Создан корневой узел: {rootNode.Text}");

                // Обработка файлов в корневой папке  
                var videoExtensions = new[] { ".mp4", ".avi", ".mkv", ".mov", ".wmv",
                                    ".mpg", ".mpeg", ".flv", ".m4v", ".webm", ".mxf" };

                ProcessDirectory(rootNode, selectedPath, videoExtensions, ref totalFolders, ref totalVideos);

                // Проверка результатов  
                Debug.WriteLine($"Обработка завершена. Папок: {totalFolders}, Видеофайлов: {totalVideos}");

                if (totalVideos == 0)
                {
                    Debug.WriteLine("Предупреждение: видеофайлы не найдены");
                    MessageBox.Show("Нет доступных видеофайлов в выбранной директории.",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Критическая ошибка: {ex}");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                stopwatch.Stop();
                Debug.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Обработка завершена за {stopwatch.Elapsed.TotalSeconds:0.000} сек");
            }
        }

        // Рекурсивный метод обработки директорий  
        private void ProcessDirectory(TreeNode parentNode, string path,
                                    string[] extensions, ref int folderCount, ref int fileCount)
        {
            try
            {
                // Обработка файлов  
                var files = Directory.EnumerateFiles(path, "*.*")
                                    .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                                    .ToList();

                foreach (var file in files)
                {
                    var fileNode = new TreeNode(Path.GetFileName(file))
                    {
                        Tag = file,
                        Name = "videoFile",
                        ImageKey = "video",
                        SelectedImageKey = "video"
                    };
                    parentNode.Nodes.Add(fileNode);
                    fileCount++;

                    Debug.WriteLine($"Добавлен файл: {file}");
                }

                // Обработка подпапок  
                foreach (var directory in Directory.EnumerateDirectories(path))
                {
                    var dirNode = new TreeNode(Path.GetFileName(directory))
                    {
                        Tag = directory,
                        ImageKey = "folder",
                        SelectedImageKey = "folder"
                    };
                    parentNode.Nodes.Add(dirNode);
                    folderCount++;

                    Debug.WriteLine($"Обработка папки: {directory}");
                    ProcessDirectory(dirNode, directory, extensions, ref folderCount, ref fileCount);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"Нет доступа к {path}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обработке {path}: {ex.Message}");
            }
        }

        private void treeViewVid_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                string videoPath = e.Node.Tag.ToString();
                axWindowsMediaPlayer1.URL = videoPath;
                Console.WriteLine(videoPath);
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


        //h264 format .mp4
        private void CompressVideo(string inputPath, string outputPath)
        {
            string ffmpegPath = @"ffmpeg.exe"; // Укажите правильный путь к ffmpeg.exe
            string arguments = $"-i \"{inputPath}\" -vcodec libx264 -crf 23 \"{outputPath}\"";

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = ffmpegPath;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();

                // Отладочный вывод
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                MessageBox.Show($"FFmpeg Output: {output}\nFFmpeg Error: {error}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
        }
        private void SendFileToServer(string localFilePath, string remoteFilePath)
        {
            using (var session = new WinSCP.Session())
            {
                // Создаем сессию и задаем параметры
                var sessionOptions = new WinSCP.SessionOptions
                {
                    Protocol = WinSCP.Protocol.Sftp,
                    HostName = "10.15.8.220",
                    UserName = "metalhead",
                    Password = "metalhead",
                    // SshPrivateKeyPath = @"C:\path\to\your\privatekey.ppk", // Задайте путь к вашему приватному ключу, если требуется
                    SshHostKeyFingerprint = "ssh-ed25519 255 g/0NbJfRCRNfD6zR0UcHnBEShZZvTQ1cNUxbTrdTdY4"
                };

                try
                {
                    session.Open(sessionOptions);
                    // Переносим файл
                    TransferOptions transferOptions = new TransferOptions
                    {
                        TransferMode = TransferMode.Binary
                    };
                    TransferOperationResult transferResult;
                    transferResult = session.PutFiles(localFilePath, remoteFilePath, false, transferOptions);
                    // Проверка на ошибки
                    transferResult.Check();
                    MessageBox.Show("Файл успешно передан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); //Сообщение об успехе
                }
                catch (WinSCP.SessionException ex)
                {
                    //Обработка исключения WinSCP. Важно!
                    MessageBox.Show($"Ошибка WinSCP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    //Обработка других исключений.
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSendVideo_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewVid.SelectedNode;
            if (selectedNode != null && selectedNode.Tag is string videoPath)
            {
                MessageBox.Show($"Выбранный файл: {videoPath}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); // Отладочное сообщение
                string compressedVideoPath = Path.Combine(Path.GetDirectoryName(videoPath), "compressed_" + Path.GetFileName(videoPath));

                // Сжимаем видео
                CompressVideo(videoPath, compressedVideoPath);

                // Передаем сжатый файл на сервер
                SendFileToServer(compressedVideoPath, "/home/metalhead/video_compressed"); // Замените на ваш путь на сервере
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите видеофайл для отправки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
