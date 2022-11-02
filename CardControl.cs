using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kitty
{
    public partial class CardControl : UserControl
    {
        private string valueString;
        private string suitString;
        private Color color;

        private string cardString = "";
        public string CardString
        {
            get { return cardString; }
            set
            {

                cardString = value.ToUpper();
                UpdateProperties();
            }
        }

        public int FontSize { get; set; } = 20;
        public CardControl()
        {
            InitializeComponent();
            UpdateProperties();
        }

        public void UpdateProperties()
        {
            if (string.IsNullOrEmpty(this.CardString))
                return;
            this.suitString = this.CardString.Substring(this.CardString.Length - 1, 1);
            this.valueString = this.CardString.Replace(this.suitString, "");
            this.color = this.suitString == "D" || this.suitString == "H" ? Color.Red : Color.Black;
            this.Width = this.FontSize + 25;
            this.Height = this.FontSize + 25;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (string.IsNullOrEmpty(this.CardString))
                return;

            Graphics graphicsObj = this.CreateGraphics();
            Font valueFont = new Font("Helvetica", this.FontSize, FontStyle.Bold);
            Font suitFont = new Font("Helvetica", this.FontSize + 10, FontStyle.Bold);
            Brush brush = new SolidBrush(this.color);

            string suitStringPicture;
            switch (this.suitString)
            {
                case "D":
                    suitStringPicture = "♦";
                    break;
                case "H":
                    suitStringPicture = "♥";
                    break;
                case "S":
                    suitStringPicture = "♠";
                    break;
                case "C":
                    suitStringPicture = "♣";
                    break;
                default:
                    suitStringPicture = this.suitString;
                    break;
            }

            graphicsObj.DrawString(this.valueString, valueFont, brush, 5, 5, StringFormat.GenericTypographic);
            graphicsObj.DrawString(suitStringPicture, suitFont, brush, 5, 5 + this.FontSize, StringFormat.GenericTypographic);

            base.OnPaint(e);
        }
    }
}
