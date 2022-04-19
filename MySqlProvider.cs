using System;
using MySql.Data.MySqlClient;

namespace MySqlProvider
{
    /**
     * @brief Предназначен для подключения к базе данных MySQL, а также реализации отправки SQL-запросов и получения данных.
     */
    public class MySqlProvider
    {
        #region fields

            /**
             * @brief Сооединение с базой данных
             */
            private MySqlConnection connection;

            /**
             * @brief Обработчик sql-запросов
             */
            private MySqlCommand command;

            /**
             * @brief Хранилище результа выполнения sql-запроса
             */
            private MySqlDataReader? dataReader;

        #endregion


        #region constructor and destructor

            /**
             * @brief Конструктор
             * @param string connectionString Строка подключения к базе данных
             */
            public MySqlProvider(string connectionString)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                command = new MySqlCommand();
                command.Connection = connection;
            }

            ~MySqlProvider()
            {
                connection.Close();
            }

        #endregion

        #region methods

            /**
             * @brief Выполняет инструкцию SQL и возвращает количество задействованных в инструкции строк.
             * @param string query SQL-запрос
             * @return int
             */
            public int Execute(string query)
            {
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }

            /**
             * @brief Выполняет инструкцию SQL и возвращает результат запроса в виде объекта
             * @param string query SQL-запрос
             * @return MySqlDataReader
             */
            public MySqlDataReader Query(string query)
            {
                if (dataReader != null && !dataReader.IsClosed) dataReader.Close();
                command.CommandText = query;
                dataReader = command.ExecuteReader();
                return dataReader;
            }

        #endregion
    }
}