using MySql.Data.MySqlClient;

namespace Zoo
{
    public partial class Auth : Form
    {
        //string connStr = "server=stud-mysql.sdlik.ru;port=33445;user=is_333_X;database=is_333_X_KR;password=ВАШПАРОЛЬ";

        MySqlConnection conn;

        public Auth()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(Setting.server);
            textBox1.Text = "lik";
            textBox2.Text = "123";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pswd = textBox2.Text;

            try
            {
                conn.Open();

                // Используем параметризованный запрос
                string sql = "SELECT COUNT(id) FROM T_Users WHERE login = @login AND pswd = @pswd AND activ = 1";
                // Создаём команду
                MySqlCommand command = new MySqlCommand(sql, conn);
                // Инициализируем (добавляем) параметры в запрос
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pswd", pswd);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Учётные данные верны");
                    AuthClass.Fio = textBox1.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка учётных данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}