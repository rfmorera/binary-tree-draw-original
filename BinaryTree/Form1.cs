using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace BinaryTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BinaryTree _tree;

        private bool _acting = false;
        private bool _paintAgain = false;
        void PaintTree()
        {
            if (_tree == null) return;
            pictureBox1.Image = _tree.Draw();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _tree = new BinaryTree();
            lblEvents.Text = @"new binary tree";
            PaintTree();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var val = int.Parse(textBox1.Text);
                if (_tree == null)
                    btnCreate_Click(btnCreate, new EventArgs());
                lblEvents.Text = _tree.Add(new Node(val))
                                  ? string.Format("{0} added successfuly", val)
                                  : string.Format("{0} not added: repeated number!", val);
                lblCount.Text = _tree.Count.ToString();
                PaintTree();
                textBox1.SelectAll();
                this.Update();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var val = int.Parse(textBox1.Text);
                lblEvents.Text = _tree.Remove(val)
                                  ? string.Format("{0} removed successfuly", val)
                                  : string.Format("not removed: {0} does not exist", val);
                PaintTree();
                textBox1.Text = _tree.RootNode.Right.Value.ToString();
                this.Update();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(btnAdd, new EventArgs());
            }
        }

        Random rnd = new Random();
        private void btnRnd_Click(object sender, EventArgs e)
        {
            var val = rnd.Next(1, 999);
            var counter = 0;
            if (_tree != null)
            {
                _tree.Exists(val);
                while (_tree.Exists(val) && counter++ < 999)
                    val = rnd.Next(1, 999);
            }
            textBox1.Text = val.ToString();
            btnAdd_Click(btnAdd, new EventArgs());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //new Action(() =>
            //{
            //    for (var i = 0; i < 999; i++)
            //    {

            //        Invoke(new Action(() => btnRnd_Click(btnRnd, new EventArgs())));
            //        System.Threading.Thread.Sleep(5);
            //    }
            //}).BeginInvoke(null, null);
            btnCreate_Click(btnCreate, new EventArgs());
            new Action(() =>
            {
                for (var i = 0; i < 45; i++)
                {
                    var val = rnd.Next(1, 999);
                    var counter = 0;
                    if (_tree != null)
                    {
                        _tree.Exists(val);
                        while (_tree.Exists(val) && counter++ < 998)
                            val = rnd.Next(1, 999);
                    }

                    var res = _tree.Add(new Node(val));

                    Invoke(new Action(() =>
                    {
                        lblEvents.Text = res
                                            ? string.Format("{0} added successfuly", val)
                                            : string.Format("{0} not added: repeated number!",
                                                            val);

                        lblCount.Text = _tree.Count.ToString();
                    }));

                    if (i % 1 == 0)
                        PaintTree();
                    //System.Threading.Thread.Sleep(1);
                }
                PaintTree();

            }).BeginInvoke(null, null);


        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            var codecs = ImageCodecInfo.GetImageEncoders();
            // Find the correct image codec
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            const double quality = 1;
            var d = new SaveFileDialog { Filter = @"jpeg files|*.jpg" };
            try
            {
                if (d.ShowDialog() != DialogResult.OK)
                    return;
                var bmp = pictureBox1.Image;
                var qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                                                                     (long)(quality * 75));
                // Jpeg image codec
                var jpegCodec = GetEncoderInfo("image/jpeg");
                if (jpegCodec == null)
                    return;
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                bmp.Save(d.FileName, jpegCodec, encoderParams);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }
    }
}
