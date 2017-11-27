using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;


namespace FreddieAI
{
    public partial class Form1 : Form
    {
        // Form Decleration...
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Start ( button_click )
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            // List of known words
            clist.Add(new string[] { "hello", "how are you", "what is the current time", "open chrome", "thank you", "close" });

            // Creating a Grammer understaning
            Grammar gr = new Grammar(new GrammarBuilder(clist));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            
            switch (e.Result.Text.ToString())
            {
                case "hello":
                    ss.SpeakAsync("hello Maker");
                    break;
                case "how are you":
                    ss.SpeakAsync("i am  doing great  Maker how about you");
                    break;
                case "what is the current time":
                    ss.SpeakAsync("the current time is " + DateTime.Now.ToLongTimeString());
                    break;
                case "thank you":
                    ss.SpeakAsync("pleasure is mine Maker");
                    break;
                case "open chrom":
                    Process.Start("chrome", "https://www.google.com");
                    break;
                case "close":
                    Application.Exit();
                    break;
            }
            txtContent.Text += e.Result.Text.ToString() + Environment.NewLine;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Stop ( button_click )
            sre.RecognizeAsyncStop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
        
}

