using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DirectX.Capture;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class ParametreWebCam : UserControl
    {
        private Filters InputOptions = null;
        private Filter VideoInput = null;
        private Filter AudioInput = null;
        private Capture CaptureInfo = null;

        public ParametreWebCam()
        {
            InitializeComponent();
        }

        private void ErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Test du Webcam", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                this.Configure();
            }
            catch (Exception)
            {
                MessageBox.Show("Aucun webCam trouvé !!", "Erreur de webCam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureInfo.CaptureFrame();
                btnSave.Enabled = true;
            }
            catch (Exception exept)
            {
                this.ErrorMessage(exept);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgSavePhoto.ShowDialog();
            //dlgSavePhoto.AddExtension = true;
            ////dlgSavePhoto.FileName = "Photo.png";
            //dlgSavePhoto.DefaultExt = "Photo.jpg";
            dlgSavePhoto.DefaultExt = "*.jpg";
            if (dr == DialogResult.OK)
            {
                ptbxCapture.Image.Save(dlgSavePhoto.FileName);
            }
            dlgSavePhoto.Reset();
        }

        /// <summary>
        /// Configure les option d'entree
        /// </summary>
        private void Configure()
        {
            if (cboVideoSource.Items.Count < 1)
                throw new Exception();
            cboVideoSource.Enabled = false;
            cboAudioSource.Enabled = false;
            this.btnApply.Enabled = true;
            this.VideoInput = this.InputOptions.VideoInputDevices[cboVideoSource.SelectedIndex];
            if (cboAudioSource.Items.Count > 0)
                this.AudioInput = this.InputOptions.AudioInputDevices[cboAudioSource.SelectedIndex];
            this.CaptureInfo = new Capture(this.VideoInput, this.AudioInput);
            this.CaptureInfo.PreviewWindow = picturePreview;
            this.CaptureInfo.RenderPreview();
            this.CaptureInfo.FrameCaptureComplete += new Capture.FrameCapHandler(CaptureInfo_FrameCaptureComplete);
            this.btnCapture.Enabled = true;
            this.btnApply.Enabled = false;
        }

        /// <summary>
        /// Est effectue lorsque la capture a reussie
        /// </summary>
        /// <param name="Frame">cette photo est generee</param>
        void CaptureInfo_FrameCaptureComplete(PictureBox Frame)
        {
            ptbxCapture.Image = Frame.Image;
        }

        private void ParametreWebCam_Load(object sender, EventArgs e)
        {
            try
            {
                InputOptions = new Filters();
                btnApply.Enabled = false;
                foreach (Filter f in InputOptions.VideoInputDevices)
                {
                    cboVideoSource.Items.Add(f.Name);
                }
                if (cboVideoSource.Items.Count > 0)
                {
                    btnApply.Enabled = true;
                    cboVideoSource.SelectedIndex = 0;
                }
                foreach (Filter f in InputOptions.AudioInputDevices)
                {
                    cboAudioSource.Items.Add(f.Name);
                }
                if (cboAudioSource.Items.Count > 0) cboAudioSource.SelectedIndex = 0;
            }
            catch (Exception){}
        }

        private void btnClosePersonneCarte_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
