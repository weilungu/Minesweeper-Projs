namespace 第十一組程式設計期末報告
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
            MessageBox.Show("遊戲難度設置為簡單");
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            difficulty = 2;
            MessageBox.Show("遊戲難度設置為普通");
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            difficulty = 3;
            MessageBox.Show($"遊戲難度設置為困難" );
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
