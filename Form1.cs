namespace ClientInstaller
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Windows.Forms;
    using Microsoft.Win32;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string falloutPath = string.Empty;
            bool isFound = false;
            button1.Enabled = false;

            // 1. Try Auto-Detection (Registry check)
            string[] registryKeys = {
                @"SOFTWARE\WOW6432Node\Interplay\Fallout",
                @"SOFTWARE\Interplay\Fallout"
            };

            foreach (string regKey in registryKeys)
            {
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKey))
                {
                    if (key != null)
                    {
                        object pathObj = key.GetValue("Installed Path") ?? key.GetValue("Path");
                        if (pathObj != null && System.IO.Directory.Exists(pathObj.ToString()))
                        {
                            string candidate = pathObj.ToString();
                            if (System.IO.File.Exists(System.IO.Path.Combine(candidate, "fallout.dat")))
                            {
                                falloutPath = candidate;
                                isFound = true;
                                break;
                            }
                        }
                    }
                }
            }

            // 2. Try Auto-Detection (Common default paths)
            if (!isFound)
            {
                string[] defaultPaths = {
                @"C:\Program Files (x86)\Steam\steamapps\common\Fallout",
                @"C:\Program Files\Steam\steamapps\common\Fallout",
                @"C:\GOG Games\Fallout",
                @"C:\Interplay\Fallout",
                @"C:\Games\Fallout"
            };

                foreach (string path in defaultPaths)
                {
                    if (System.IO.Directory.Exists(path) && System.IO.File.Exists(System.IO.Path.Combine(path, "fallout.dat")))
                    {
                        falloutPath = path;
                        isFound = true;
                        break;
                    }
                }
            }

            // 3. Fallback: Manual point if auto-detection fails
            if (!isFound)
            {
                MessageBox.Show("Fallout 1 could not be automatically detected. Please select your Fallout 1 installation folder manually.", "Fallout 1 Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (FolderBrowserDialog falloutDialog = new FolderBrowserDialog())
                {
                    falloutDialog.Description = "Select your existing Fallout 1 installation folder (the one containing fallout.dat):";
                    if (falloutDialog.ShowDialog() == DialogResult.OK)
                    {
                        string candidate = falloutDialog.SelectedPath;
                        if (System.IO.File.Exists(System.IO.Path.Combine(candidate, "fallout.dat")))
                        {
                            falloutPath = candidate;
                            isFound = true;
                        }
                        else
                        {
                            MessageBox.Show("The selected folder does not contain 'fallout.dat'. Installation aborted.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            button1.Enabled = true; // Re-enable if an error occurs so they can retry
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // 4. Select target installation directory
            using (FolderBrowserDialog targetDialog = new FolderBrowserDialog())
            {
                targetDialog.Description = "Select the folder where you want to install the project files.";
                if (targetDialog.ShowDialog() == DialogResult.OK)
                {
                    string targetDirectory = targetDialog.SelectedPath;

                    try
                    {
                        // Update status if you named your label 'label1'
                        label3.Text = "Copying fallout.dat...";
                        // Application.DoEvents();

                        // Copy fallout.dat from verified Fallout 1 installation
                        string sourceDatPath = System.IO.Path.Combine(falloutPath, "fallout.dat");
                        string destDatPath = System.IO.Path.Combine(targetDirectory, "fallout.dat");
                        System.IO.File.Copy(sourceDatPath, destDatPath, true);

                        // Find and extract embedded payload.zip with progress tracking
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                        string resourceName = assembly.GetManifestResourceNames()
                            .FirstOrDefault(r => r.EndsWith("payload.zip", StringComparison.OrdinalIgnoreCase));

                        if (resourceName != null)
                        {
                            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                            {
                                using (System.IO.Compression.ZipArchive archive = new System.IO.Compression.ZipArchive(resourceStream, System.IO.Compression.ZipArchiveMode.Read))
                                {
                                    int totalEntries = archive.Entries.Count;
                                    int currentEntry = 0;

                                    // Configure progress bar limits if you named it 'progressBar1'
                                    progressBar2.Minimum = 0;
                                    progressBar2.Maximum = totalEntries;
                                    progressBar2.Value = 0;

                                    foreach (System.IO.Compression.ZipArchiveEntry entry in archive.Entries)
                                    {
                                        string destinationPath = System.IO.Path.Combine(targetDirectory, entry.FullName);
                                        string directoryName = System.IO.Path.GetDirectoryName(destinationPath);

                                        if (!System.IO.Directory.Exists(directoryName))
                                        {
                                            System.IO.Directory.CreateDirectory(directoryName);
                                        }

                                        if (!string.IsNullOrEmpty(entry.Name))
                                        {
                                            entry.ExtractToFile(destinationPath, true);
                                        }

                                        currentEntry++;
                                        int percentComplete = (int)((double)currentEntry / totalEntries * 100);

                                        // Update progress bar and label live
                                        progressBar2.Value = currentEntry;
                                        label3.Text = $"Extracting... {percentComplete}% ({currentEntry}/{totalEntries} files)";
                                        Application.DoEvents(); // Keeps the window responsive
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Could not find embedded 'payload.zip'. Make sure its Build Action is set to Embedded Resource.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            button1.Enabled = true; // Re-enable if an error occurs so they can retry
                            return;
                        }

                        MessageBox.Show("Installation completed successfully! No system settings were modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred during installation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.Enabled = true; // Re-enable if an error occurs so they can retry
                    }
                }
            }
        }
    }
}
