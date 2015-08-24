using System;
using System.IO;
using System.Windows.Forms;

namespace Taxi
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initialization of the main form of the application.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();

            string filePath = of.FileName;
            Tuple<int, int> res = DataCleaner.CleanMissingData(filePath);
            MessageBox.Show(res.Item1 + " " + res.Item2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Neighboorhood file");
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();

            string ballsCsv = of.FileName;
            string ballsName = Path.GetFileNameWithoutExtension(ballsCsv);
            Cover nb = new Cover(ballsCsv);

            MessageBox.Show("Test file");
            OpenFileDialog of2 = new OpenFileDialog();
            of2.ShowDialog();

            FeatureWriter.FromNeighborhood(nb, ballsName, of2.FileName, false, true);

            MessageBox.Show("Train file");

            OpenFileDialog of3 = new OpenFileDialog();
            of3.ShowDialog();

            FeatureWriter.FromNeighborhood(nb, ballsName, of3.FileName, true, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test file");
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();

            string testFilePath = of.FileName;

            MessageBox.Show("Train file");
            of.ShowDialog();

            string trainFilePath = of.FileName;

            MessageBox.Show("Sample submission");
            of.ShowDialog();

            string sample = of.FileName;

            string[] keywords = keywordsTbx.Text.Split(';');
            string[] maxOccurences = maxoccurencesTbx.Text.Split(';');
            string[] minOccurences = minOccurencesTbx.Text.Split(';');
            string[] exps1 = expo1Tbx.Text.Split(';');
            string[] exps2 = expo2Tbx.Text.Split(';');

            StreamingLearning sl = new StreamingLearning();

            foreach (string keyword in keywords)
                foreach (string maxOc in maxOccurences)
                    foreach (string minOc in minOccurences)
                        foreach (string exp1 in exps1)
                            foreach (string exp2 in exps2)
                            {
                                string learning = keyword + "_" + minOc + "_" + maxOc + "_" + exp1 + "_" + exp2;
                                LearningParameters learningParams = new LearningParameters(learning);
                                sl.TrainPredictAndWrite(trainFilePath, testFilePath, testFilePath + "pred_1.1_" + learning, sample, learningParams);
                            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Train file");
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            if (!of.CheckFileExists) return;

            string trainFilePath = of.FileNames[0];

            StreamingLearning.DistanceFunction distance = Distances.Haversine;
            
            string[] keywords = keywordsTbx.Text.Split(';'),
                maxOccurences = maxoccurencesTbx.Text.Split(';'),
                minOccurences = minOccurencesTbx.Text.Split(';'),
                exps1 = expo1Tbx.Text.Split(';'),
                exps2 = expo2Tbx.Text.Split(';');

            string errors = "";

            foreach (string keyword in keywords)
            {
                int nFolds = 5;

                StreamingLearning sl = new StreamingLearning();
                LearningParameters learningParameters = new LearningParameters(keyword + "_0_0_0_0");
                sl.CrossLearning(trainFilePath, nFolds, learningParameters);

                foreach (string maxOc in maxOccurences)
                    foreach (string minOc in minOccurences)
                        foreach (string exp1 in exps1)
                            foreach (string exp2 in exps2)
                            {
                                string learning = keyword + "_" + minOc + "_" + maxOc + "_" + exp1 + "_" + exp2;
                                learningParameters = new LearningParameters(learning);
                                double[] err = sl.CrossValidationScore(trainFilePath, learningParameters, nFolds, distance);
                                errors += learning + ";" + String.Join(";", err) + Environment.NewLine;
                            }
            }
            string fileName = Path.GetFileNameWithoutExtension(trainFilePath);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + fileName + "_CrossValidation_1.1.csv", errors);
            MessageBox.Show(errors);
        }
    }
}
