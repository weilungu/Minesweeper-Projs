namespace ��ʮһ�M��ʽ�OӋ��ĩ���
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int difficulty = 0;
        public int getDifficulty { get => difficulty; }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            difficulty = 1;
            MessageBox.Show("�[���y���O�Þ麆��");
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            difficulty = 2;
            MessageBox.Show("�[���y���O�Þ���ͨ");
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            difficulty = 3;
            MessageBox.Show($"�[���y���O�Þ����y" );
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
