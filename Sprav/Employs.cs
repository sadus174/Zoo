using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Zoo.Sprav
{
    public partial class Employs : Form
    {
        //Переменная соединения
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = "0";
        //Строка подключения
        string connStr = "server=10.80.1.98;port=3306;user=zoo;database=lik;password=293fh290fg9(#9fh";

        public void GetListUsers()
        {
            //Запрос для вывода строк в БД
            string commandStr = "SELECT id AS 'Код', " +
                                "fio  AS 'ФИО', " +
                                "login AS 'Логин', " +
                                "pswd AS 'Пароль', " +
                                "activ  AS 'Состояние' " +
                                "FROM T_Users";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
            //Отражаем количество записей в ДатаГриде
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel2.Text = (count_rows).ToString();
        }

        private void EditUser()
        {
            if (dataGridView1.CurrentRow == null) return;

            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string fio = Prompt.ShowDialog("Изменить ФИО:", "Редактирование", dataGridView1.CurrentRow.Cells[1].Value.ToString());
            string login = Prompt.ShowDialog("Изменить логин:", "Редактирование", dataGridView1.CurrentRow.Cells[2].Value.ToString());
            string pswd = Prompt.ShowDialog("Изменить пароль:", "Редактирование", dataGridView1.CurrentRow.Cells[3].Value.ToString());
            string activ = Prompt.ShowDialog("Изменить состояние (1/0):", "Редактирование", dataGridView1.CurrentRow.Cells[4].Value.ToString());

            string sql = "UPDATE T_Users SET fio = @fio, login = @login, pswd = @pswd, activ = @activ WHERE id = @id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fio", fio);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@pswd", pswd);
                cmd.Parameters.AddWithValue("@activ", activ);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            table.Clear();
            GetListUsers();
        }

        private void DeleteUser()
        {
            if (dataGridView1.CurrentRow == null) return;

            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            string sql = "DELETE FROM T_Users WHERE id = @id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            table.Clear();
            GetListUsers();
        }

        private void AddUser()
        {
            string fio = Prompt.ShowDialog("Введите ФИО:", "Добавление пользователя");
            string login = Prompt.ShowDialog("Введите логин:", "Добавление пользователя");
            string pswd = Prompt.ShowDialog("Введите пароль:", "Добавление пользователя");
            string activ = Prompt.ShowDialog("Введите состояние (1/0):", "Добавление пользователя");

            if (fio == "" || login == "" || pswd == "") return;

            string sql = "INSERT INTO T_Users (fio, login, pswd, activ) VALUES (@fio, @login, @pswd, @activ)";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@fio", fio);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@pswd", pswd);
                cmd.Parameters.AddWithValue("@activ", activ);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            table.Clear();
            GetListUsers();
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption, string defaultValue = "")
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterParent
                };
                Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultValue };
                Button confirmation = new Button() { Text = "OK", Left = 270, Width = 90, Top = 80 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;
                prompt.ShowDialog();
                return inputBox.Text;
            }
        }

        public Employs()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e) => AddUser();
        private void BtnEdit_Click(object sender, EventArgs e) => EditUser();
        private void BtnDelete_Click(object sender, EventArgs e) => DeleteUser();

        private void Employs_Load(object sender, EventArgs e)
        {
            // Создание подключения к БД
            conn = new MySqlConnection(connStr);

            // Заполнение DataGridView данными из БД
            GetListUsers();

            // Количество колонок, с которыми работаем
            int columnCount = 5;

            // Настройка отображения колонок: видимость, ширина, только чтение, автозаполнение
            for (int i = 0; i < columnCount; i++)
            {
                var column = dataGridView1.Columns[i];
                column.Visible = true;
                column.FillWeight = (i == 1) ? 40 : 15; // Колонке с ФИО (2ая) даём большую ширину
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            // Скрытие заголовков строк
            dataGridView1.RowHeadersVisible = false;
            // Отображение заголовков столбцов
            dataGridView1.ColumnHeadersVisible = true;
            // Выделение строки
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    // Выделяем строку
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[hit.RowIndex].Cells[0];

                    // Получаем ID из первой ячейки
                    id_selected_rows = dataGridView1.Rows[hit.RowIndex].Cells[0].Value.ToString();

                    // Показываем MessageBox с ID
                    toolStripLabel4.Text = $"{id_selected_rows}";
                }
            }
        }

        private void Удалить_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void Изменить_Click(object sender, EventArgs e)
        {
            EditUser();
        }
    }
}
