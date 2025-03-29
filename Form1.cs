
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
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = "SELECT COUNT(id) as 'Количество пользователей' FROM T_Users WHERE login='"+login+ "' and pswd='"+ pswd + "' and activ=1";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // выполняем запрос и получаем ответ
            int count = Convert.ToInt32(command.ExecuteScalar().ToString());
            // выводим ответ в консоль

            if (count > 0)
            {
                MessageBox.Show("Учётные данные верны");
            }
            else
            {
                MessageBox.Show("Ошибка учётных данных");
            }

            // закрываем соединение с БД
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Close();
        }
    }
}
