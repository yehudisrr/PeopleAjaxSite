﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PeopleAjax.Data
{
    public class PeopleDb
    {
        private readonly string _connectionString;
        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES " +
                              "(@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            conn.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        public List<Person> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            List<Person> ppl = new List<Person>();
            conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return ppl;
        }
        public void Edit(Person person)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People " +
                                "SET FirstName = @FirstName, LastName = @LastName, Age = @age " +
                                "WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            cmd.Parameters.AddWithValue("@LastName", person.LastName);
            cmd.Parameters.AddWithValue("@Age", person.Age);
            cmd.Parameters.AddWithValue("@Id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE from People WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
