using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RandomDogPicture
{
    internal class PictureForm : Form
    {
        public PictureForm()
        {
            Size = new Size(1920, 1080);

            var picture = new PictureBox();
            picture.Size = new Size(Width, Height);
            picture.Location = new Point(0, 0);
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            picture.SizeChanged += (sender, args) => 
                Size = picture.Size;

            picture.MouseClick += (sender, args) =>
                GetRandomDogPicture(picture);

            GetRandomDogPicture(picture);

            Size = picture.Size;
            Controls.Add(picture);
        }

        private static void GetRandomDogPicture(PictureBox picture)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://dog.ceo/api/breeds/image/random");
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);

            string sReadData = sr.ReadToEnd();
            response.Close();
            dynamic d = JObject.Parse(sReadData);
            picture.ImageLocation = d.message;
        }
    }
}
