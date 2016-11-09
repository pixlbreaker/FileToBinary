using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileToBinary {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e){
           
        }

        private void button1_Click(object sender, EventArgs e) {

            // Variables
            DialogResult result;
            string filename;
            string extension;

            //  Uses Open FileDialog to get a file
            using (var fileChooser = openFileDialog1) {
                fileChooser.FileName = "";
                result = fileChooser.ShowDialog();
                filename = fileChooser.FileName;
            }

            // Opens a fileDialog
            filename = openFileDialog1.FileName;
            extension = Path.GetExtension(filename);


            // Bit Array for the file
            BitArray bits = GetBinaryFile(filename);

            // Save File Dialog Popup
            saveFileDialog1.ShowDialog();
            string pathName = saveFileDialog1.FileName.ToString();
            
            // Writes the bytes to the new selected file
            Stream stream = new FileStream(pathName, FileMode.Create);
            foreach(Boolean b in bits) {
                stream.WriteByte(Convert.ToByte(b));
            }           
            stream.Close(); //closes stream


            MessageBox.Show(filename + " bytes have been stored in " + pathName);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        /// <summary>
        /// Gets a bit array (Collections) from a specific file
        /// http://stackoverflow.com/questions/24106687/convert-file-to-binary-in-c-sharp
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private BitArray GetBinaryFile(string filename) {

            byte[] bytes;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }

            return new BitArray(bytes);
        }
    }
}
