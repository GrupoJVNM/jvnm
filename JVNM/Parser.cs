﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JVNM
{
    public class Parser
    {

        public static MiniSQLQuery Parse(string miniSQLQuery)
        {

            
            const string selectPattern = "SELECT [(\\w+)](\\w+)(\\,(\\s)?(\\w+))? FROM (\\w+) WHERE (\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?;";
            const string selectAllPattern = "SELECT \\* FROM (\\w+) WHERE (\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?;";
            const string selectWithOutCPattern = "SELECT [(\\w+)](\\w+)(\\,(\\s)?(\\w+))? FROM (\\w+);";
            const string selectAllWithOutCPattern = "SELECT \\* FROM (\\w+);";

            const string insertPattern = "INSERT INTO (\\w+) VALUES (\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?([0-9]+)?(\\,(\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?)?;";
            const string deletePattern = "DELETE FROM (\\w+) WHERE (\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?([0-9]+)?(( AND )((\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?)?;";
            const string updatePattern = "UPDATE (\\w+) SET (\\w+)(\\s)?=(\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)? WHERE (\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?(( AND )((\\w+)(\\s)?[=,<,>](\\s)?(\\'[A-Za-z0-9(\\s)(\\.)(\\,)]+\\')?((\\-)?[0-9]+)?))?;";

            
            const string dropPattern = "DROP TABLE (\\w+);";
            const string createTablePattern = "CREATE TABLE (\\w+) \\((\\w+ \\w+)((\\,(\\s)?(\\w+ \\w+))+)?\\);";

            //Select
            Match match = Regex.Match(miniSQLQuery, selectPattern);
            if (match.Success)
            {
                List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
                string table = match.Groups[2].Value;


                DataComparator compare;
                if (match.Groups[3].Value.Contains("="))
                {
                    compare = DataComparator.Equal;

                }
                else if (match.Groups[3].Value.Contains(">"))
                {
                    compare = DataComparator.Bigger;

                }
                else
                {
                    compare = DataComparator.Less;

                }

                List<string> condition = Condition(match.Groups[3].Value);
                String column = condition[0];
                String value = condition[1];


                return new Select(table, columnNames, compare, column, value);
            }

            match = Regex.Match(miniSQLQuery, selectAllPattern);
            if (match.Success)
            {

                string table = match.Groups[2].Value;


                DataComparator compare;
                if (match.Groups[3].Value.Contains("="))
                {
                    compare = DataComparator.Equal;

                }
                else if (match.Groups[3].Value.Contains(">"))
                {
                    compare = DataComparator.Bigger;

                }
                else
                {
                    compare = DataComparator.Less;

                }

                List<string> condition = Condition(match.Groups[3].Value);
                String column = condition[0];
                String value = condition[1];


                return new SelectAll(table, compare, column, value);
            }

            //SelectWithOutC
            match = Regex.Match(miniSQLQuery, selectWithOutCPattern);
            if (match.Success)
            {
                List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
                string table = match.Groups[2].Value;
                return new SelectWithOutC(table, columnNames);
            }

            //SelectAllWithOutC
            match = Regex.Match(miniSQLQuery, selectAllWithOutCPattern);
            if (match.Success)
            {
                string table = match.Groups[2].Value;
                return new SelectAllWithOutC(table);
            }

            //Insert
            match = Regex.Match(miniSQLQuery, insertPattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                List<string> columnNames = insertSeparated(match.Groups[2].Value);
                return new Insert(table, columnNames);
            }

            //Delete
            match = Regex.Match(miniSQLQuery, deletePattern);
            if (match.Success)
            {

                string table = match.Groups[1].Value;


                DataComparator compare;
                if (match.Groups[3].Value.Contains("="))
                {
                    compare = DataComparator.Equal;

                }
                else if (match.Groups[3].Value.Contains(">"))
                {
                    compare = DataComparator.Bigger;

                }
                else
                {
                    compare = DataComparator.Less;

                }

                List<string> condition = Condition(match.Groups[3].Value);
                String column = condition[0];
                String value = condition[1];


                return new DeleteTuple(table, column, compare, value);
            }

            //Update
            match = Regex.Match(miniSQLQuery, updatePattern);
            if (match.Success)
            {
                //update
                string table = match.Groups[1].Value;
                //set
                List<string> set = Condition(match.Groups[2].Value);
                string c = set[0];
                string d = set[1];


                DataComparator compare;
                if (match.Groups[3].Value.Contains("="))
                {
                    compare = DataComparator.Equal;

                }
                else if (match.Groups[3].Value.Contains(">"))
                {
                    compare = DataComparator.Bigger;

                }
                else
                {
                    compare = DataComparator.Less;

                }

                List<string> condition = Condition(match.Groups[3].Value);
                string column = condition[0];
                string value = condition[1];


                return new Update(table, c, d, compare, value, column);

            }
            
            match = Regex.Match(miniSQLQuery, dropPattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;

                return new DropTable(table);
            }

            /*match = Regex.Match(miniSQLQuery, createPattern);
            if (match.Success)
            {
                string DataBase = match.Groups[1].Value;

                return new (DataBase);
            }*/

            match = Regex.Match(miniSQLQuery, createTablePattern);
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                List<String> columns = createTable(match.Groups[2].Value);

                return new CreateTable(name, columns);
            }
            return null;
        }
            static List<string> CommaSeparatedNames(string text)
            {
                string t = text.Trim(' ');
                string[] columnNames = t.Split(',');

                return columnNames.ToList();
            }
            static List<string> insertSeparated(string text)
            {

                string t = text.Trim(' ');

                string[] columnNames = t.Split(',');
                columnNames[0].Replace("(", " ").Trim(' ');
                columnNames[columnNames.Length - 1].Replace(")", " ").Trim(' ');
                for (int i = 0; i < columnNames.Length; i++)
                {

                    columnNames[i] = columnNames[i].Replace("'", " ").Trim(' ');

                }
                return columnNames.ToList();
            }

            static List<string> Condition(string text)
            {
                Char[] array = { '=', '>', '<' };
                string t = text.Trim(' ');
                string[] a = t.Split(array);


                return a.ToList();
            }

            static List<string> createTable(string text)
            {

                string sql2 = text.Replace(", ", " ");
                string[] a = sql2.Split(' ');

                return a.ToList();
            }
    }
}


