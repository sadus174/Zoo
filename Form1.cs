
using MySql.Data.MySqlClient;

namespace Zoo
{
    public partial class Form1 : Form
    {
        string connStr = "server=10.80.1.98;port=3306;user=zoo;database=lik;password=293fh290fg9(#9fh";
        MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pswd = textBox2.Text;
            // ������������� ���������� � ��
            conn.Open();
            // ������
            string sql = "SELECT COUNT(id) as '���������� �������������' FROM T_Users WHERE login='"+login+ "' and pswd='"+ pswd + "' and activ=1";
            // ������ ��� ���������� SQL-�������
            MySqlCommand command = new MySqlCommand(sql, conn);
            // ��������� ������ � �������� �����
            int count = Convert.ToInt32(command.ExecuteScalar().ToString());
            // ������� ����� � �������

            if (count > 0)
            {
                MessageBox.Show("������� ������ �����");
            }
            else
            {
                MessageBox.Show("������ ������� ������");
            }

            // ��������� ���������� � ��
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Close();
        }
    }
}
