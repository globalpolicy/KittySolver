using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kitty
{
    public partial class Form1 : Form
    {
        List<CardControl> cardControls = new List<CardControl>();
        List<Card> handOfCards = new List<Card>();
        Stopwatch stopWatch = new Stopwatch();
        List<Hand> combo;
        Hand orderedBestHand;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// Draws card controls in the form
        /// </summary>
        /// <param name="cards">List of cards to draw</param>
        /// <param name="xPos">Starting x position</param>
        /// <param name="yPos">Starting y position</param>
        /// <param name="clearExisting">Existing card controls are cleared and drawn anew if True, false otherwise</param>
        private void DrawCards(List<Card> cards, int xPos = 50, int yPos = 50, bool clearExisting = true)
        {
            if (clearExisting)
            {
                foreach (CardControl cardControl in cardControls)
                {
                    this.Controls.Remove(cardControl);
                    cardControl.Dispose();
                }
            }


            foreach (Card card in cards)
            {
                CardControl cardControl = new CardControl();
                cardControl.CardString = card.ToString();
                cardControl.BorderStyle = BorderStyle.Fixed3D;
                cardControl.BackColor = Color.White;
                cardControl.FontSize = 10;
                cardControl.Location = new Point(xPos, yPos);
                xPos += cardControl.Width;
                this.Controls.Add(cardControl);
                this.cardControls.Add(cardControl);
            }
        }

        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripLabel1.Text = "Generating random cards..";
            handOfCards = CardFunctions.GetRandomCards(9);
            toolStripLabel1.Text = "Random cards generated";
            DrawCards(handOfCards);
        }

        private void optimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            randomizeToolStripMenuItem.Enabled = false;
            optimizeToolStripMenuItem.Enabled = false;
            toolStripTextBox1.Enabled = false;
            toolStripButton1.Enabled = false;

            toolStripLabel1.Text = "Generating all possible arrangements..";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;

            stopWatch.Reset();
            stopWatch.Start();
            worker.RunWorkerAsync(handOfCards);
        }


        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();
            combo = (List<Hand>)e.Result;

            Hand bestHand = CardFunctions.GetBestHand(combo);
            orderedBestHand = CardFunctions.GetOrderedHand(bestHand);

            this.toolStripLabel1.Text = $"Optimized from {combo.Count} arrangements in {stopWatch.ElapsedMilliseconds / 1000.0} s";
            DrawCards(orderedBestHand.ToList(), 50, 150, false);

            randomizeToolStripMenuItem.Enabled = true;
            optimizeToolStripMenuItem.Enabled = true;
            toolStripTextBox1.Enabled = true;
            toolStripButton1.Enabled = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
            this.toolStripLabel1.Text = $"Generating arrangement #{(int)(e.ProgressPercentage / 100.0 * (9 * 8 * 7 * 6 * 5 * 4 * 3 * 2 * 1))}";
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<Hand> combo = CardFunctions.GetAllCombinations((List<Card>)e.Argument, new CardFunctions.ProgressReporter(progress =>
              {
                  worker = sender as BackgroundWorker;
                  worker.ReportProgress(progress);
              }));
            e.Result = combo;
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
                PopulateCustomHand();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PopulateCustomHand();
        }

        private void PopulateCustomHand()
        {
            if (string.IsNullOrEmpty(toolStripTextBox1.Text))
                return;
            try
            {
                handOfCards = CardFunctions.GetCards(toolStripTextBox1.Text);
                toolStripLabel1.Text = "User-defined cards generated";
                DrawCards(handOfCards);
            }
            catch (Exception ex)
            {
                toolStripLabel1.Text = "Error processing user-defined cards!";
            }



        }
    }
}
