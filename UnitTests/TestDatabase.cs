﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JVNM;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    
    [TestClass]
    public class TestDatabase
    {

        Database Db = new Database("bd1", "username", "password");
        public List <Table> Tables;
      


        [TestMethod]
        public void SelectWithOutCTest()
        {

            List<TableColumn> columnList = new List<TableColumn>();
            List<String> selectC = new List<String>();
            List<List<String>> returnList = new List<List<String>>();



            TableColumn ColumnInt = new TableColumn("ColumnInt", DataType.Int);
            TableColumn ColumnDouble = new TableColumn("ColumnDouble", DataType.Double);
            TableColumn ColumnText = new TableColumn("ColumnText", DataType.Text);
            columnList.Add(ColumnInt);
            columnList.Add(ColumnDouble);
            columnList.Add(ColumnText);
            Table table = new Table("myTable", columnList);
            ColumnText.Add("Hola");
            ColumnText.Add("Adios");
            ColumnText.Add("Hola");
            ColumnInt.Add("1");
            ColumnInt.Add("2");
            ColumnInt.Add("3");
            ColumnDouble.Add("1,1");
            ColumnDouble.Add("2,1");
            ColumnDouble.Add("3,1");
            selectC.Add("ColumnInt");
            Db.AddTable(table);
            returnList = Db.SelectWithOutC(table.Name,selectC);

            List<List<String>> resultOk = new List<List<String>>();

            List<String> listInt = new List<string>();
            listInt.Add("1");
            listInt.Add("2");
            listInt.Add("3");
            resultOk.Add(listInt);

            for (int i = 0; i < returnList.Count; i++)
            {
                for (int j = 0; j < returnList[i].Count; j++)
                {
                    Assert.AreEqual(resultOk[i][j], returnList[i][j]);
                }

            }


        }

        [TestMethod]
        public void SelectTest()
        {

            List<TableColumn> columnList = new List<TableColumn>();

            TableColumn ColumnInt = new TableColumn("ColumnInt", DataType.Int);
            TableColumn ColumnDouble = new TableColumn("ColumnDouble", DataType.Double);
            TableColumn ColumnText = new TableColumn("ColumnText", DataType.Text);
            columnList.Add(ColumnInt);
            columnList.Add(ColumnDouble);
            columnList.Add(ColumnText);
            Table table = new Table("myTable", columnList);
            ColumnText.Add("Hola");
            ColumnText.Add("Adios");
            ColumnText.Add("Hola");
            ColumnInt.Add("1");
            ColumnInt.Add("2");
            ColumnInt.Add("3");
            ColumnDouble.Add("1,1");
            ColumnDouble.Add("2,1");
            ColumnDouble.Add("3,1");
            List<String> dataList2 = new List<String>();
            dataList2.Add("ColumnInt");
            dataList2.Add("ColumnDouble");
            dataList2.Add("ColumnText");
            Db.AddTable(table);
            List<List<String>> returnList = Db.Select(table.Name, dataList2, DataComparator.Equal, ColumnText, "Hola");


            List<List<String>> resultOk = new List<List<String>>();

            List<String> listInt = new List<string>();
            List<String> listDouble = new List<string>();
            List<String> listText = new List<string>();
            listText.Add("Hola");
            listText.Add("Hola");
            listInt.Add("1");
            listInt.Add("3");
            listDouble.Add("1,1");
            listDouble.Add("3,1");
            resultOk.Add(listInt);
            resultOk.Add(listDouble);
            resultOk.Add(listText);
            Assert.IsTrue(resultOk[0].Count == 2);
            Assert.IsTrue(resultOk[1].Count == 2);
            Assert.IsTrue(resultOk[2].Count == 2);

            for (int i = 0; i < returnList.Count; i++)
            {
                for (int j = 0; j < returnList[i].Count; j++)
                {
                    Assert.AreEqual(resultOk[i][j], returnList[i][j]);
                }

            }
            
            List<List<String>> returnList2 = Db.Select(table.Name, dataList2, DataComparator.Bigger, ColumnInt, "1");

            List<List<String>> resultOk2 = new List<List<String>>();

            List<String> listInt2 = new List<string>();
            List<String> listDouble2 = new List<string>();
            List<String> listText2 = new List<string>();
            listText2.Add("Adios");
            listText2.Add("Hola");
            listInt2.Add("2");
            listInt2.Add("3");
            listDouble2.Add("2,1");
            listDouble2.Add("3,1");
            resultOk2.Add(listInt2);
            resultOk2.Add(listDouble2);
            resultOk2.Add(listText2);

            for (int i = 0; i < returnList2.Count; i++)
            {
                for (int j = 0; j < returnList2[i].Count; j++)
                {
                    Assert.AreEqual(resultOk2[i][j], returnList2[i][j]);
                }

            }

            List<List<String>> returnList3 = Db.Select(table.Name, dataList2, DataComparator.Less, ColumnDouble, "1,1");


            Assert.IsTrue(returnList3.Count == 0);

        }

        [TestMethod]

        public void SelectAllTest()
        {

            List<TableColumn> columnList = new List<TableColumn>();

            TableColumn ColumnInt = new TableColumn("ColumnInt", DataType.Int);
            TableColumn ColumnDouble = new TableColumn("ColumnDouble", DataType.Double);
            TableColumn ColumnText = new TableColumn("ColumnText", DataType.Text);
            columnList.Add(ColumnInt);
            columnList.Add(ColumnDouble);
            columnList.Add(ColumnText);
            Table table = new Table("myTable", columnList);
            ColumnText.Add("Hola");
            ColumnText.Add("Adios");
            ColumnText.Add("Hola");
            ColumnInt.Add("1");
            ColumnInt.Add("2");
            ColumnInt.Add("3");
            ColumnDouble.Add("1,1");
            ColumnDouble.Add("2,1");
            ColumnDouble.Add("3,1");
            Db.AddTable(table);
            List<List<String>> returnList = Db.SelectAll(table.Name,DataComparator.Equal, ColumnText, "Hola");

            List<List<String>> resultOk = new List<List<String>>();

            List<String> listInt = new List<string>();
            List<String> listDouble = new List<string>();
            List<String> listText = new List<string>();
            listText.Add("Hola");
            listText.Add("Hola");
            listInt.Add("1");
            listInt.Add("3");
            listDouble.Add("1,1");
            listDouble.Add("3,1");
            resultOk.Add(listInt);
            resultOk.Add(listDouble);
            resultOk.Add(listText);
            Assert.IsTrue(resultOk[0].Count == 2);
            Assert.IsTrue(resultOk[1].Count == 2);
            Assert.IsTrue(resultOk[2].Count == 2);

            for (int i = 0; i < returnList.Count; i++)
            {
                for (int j = 0; j < returnList[i].Count; j++)
                {
                    Assert.AreEqual(resultOk[i][j], returnList[i][j]);
                }

            }

            List<List<String>> returnList2 = Db.SelectAll(table.Name,DataComparator.Bigger, ColumnText, "1");

            List<List<String>> resultOk2 = new List<List<String>>();

            List<String> listInt2 = new List<string>();
            List<String> listDouble2 = new List<string>();
            List<String> listText2 = new List<string>();
            listText2.Add("Adios");
            listText2.Add("Hola");
            listInt2.Add("2");
            listInt2.Add("3");
            listDouble2.Add("2,1");
            listDouble2.Add("3,1");
            resultOk2.Add(listInt2);
            resultOk2.Add(listDouble2);
            resultOk2.Add(listText2);

            for (int i = 0; i < returnList2.Count; i++)
            {
                for (int j = 0; j < returnList2[i].Count; j++)
                {
                    Assert.AreEqual(resultOk2[i][j], returnList2[i][j]);
                }

            }

            List<List<String>> returnList3 = Db.SelectAll(table.Name,DataComparator.Less, ColumnDouble, "1,1");

            Assert.IsTrue(returnList3.Count == 0);




        }

        [TestMethod]
        public void selectAllWithOutCTest()
        {

            List<TableColumn> columnList = new List<TableColumn>();

            TableColumn ColumnInt = new TableColumn("ColumnInt", DataType.Int);
            TableColumn ColumnDouble = new TableColumn("ColumnDouble", DataType.Double);
            TableColumn ColumnText = new TableColumn("ColumnText", DataType.Text);
            columnList.Add(ColumnInt);
            columnList.Add(ColumnDouble);
            columnList.Add(ColumnText);
            Table table = new Table("myTable", columnList);
            ColumnText.Add("Hola");
            ColumnText.Add("Adios");
            ColumnText.Add("Hola");
            ColumnInt.Add("1");
            ColumnInt.Add("2");
            ColumnInt.Add("3");
            ColumnDouble.Add("1,1");
            ColumnDouble.Add("2,1");
            ColumnDouble.Add("3,1");
            Db.AddTable(table);
            List<List<String>> returnList = Db.selectAllWithOutC(table.Name);

            List<List<String>> resultOk = new List<List<String>>();

            List<String> listInt = new List<string>();
            List<String> listDouble = new List<string>();
            List<String> listText = new List<string>();
            listText.Add("Hola");
            listText.Add("Adios");
            listText.Add("Hola");
            listInt.Add("1");
            listInt.Add("2");
            listInt.Add("3");
            listDouble.Add("1,1");
            listDouble.Add("2,1");
            listDouble.Add("3,1");
            resultOk.Add(listInt);
            resultOk.Add(listDouble);
            resultOk.Add(listText);

            for (int i = 0; i < returnList.Count; i++)
            {
                for (int j = 0; j < returnList[i].Count; j++)
                {
                    Assert.AreEqual(resultOk[i][j], returnList[i][j]);
                }

            }




        }

        [TestMethod]
        public void InsertTest()
        {

            List<TableColumn> columnList = new List<TableColumn>();

            TableColumn columnInt = new TableColumn("columnInt", DataType.Int);
            TableColumn columnDouble = new TableColumn("columnDouble", DataType.Double);
            TableColumn columnText = new TableColumn("columnText", DataType.Text);
            columnList.Add(columnInt);
            columnList.Add(columnDouble);
            columnList.Add(columnText);
            Table table = new Table("myTable", columnList);

            List<String> list2 = new List<String>();
            list2.Add("18");
            list2.Add("22,6");
            list2.Add("higb");

            Assert.IsTrue(table.GetListTableColumn().Count == 3);
            Db.AddTable(table);
            Db.Insert(table.Name,list2);
            Assert.IsTrue(table.GetListTableColumn()[0].GetList().Count != 0);
            Assert.IsTrue(table.GetListTableColumn()[1].GetList().Count != 0);
            Assert.IsTrue(table.GetListTableColumn()[2].GetList().Count != 0);




        }
        [TestMethod]

        public void DeleteTest()
        { 
            List<TableColumn> columnList = new List<TableColumn>();

            TableColumn columnInt = new TableColumn("columnInt", DataType.Int);
            TableColumn columnDouble = new TableColumn("columnInt", DataType.Double);
            TableColumn columnText = new TableColumn("columnInt", DataType.Text);
            List<String> list2 = new List<String>();
            columnList.Add(columnInt);
            columnList.Add(columnDouble);
            columnList.Add(columnText);
            Table table = new Table("myTable", columnList);
            columnText.Add("Hola");
            columnText.Add("Adios");
            columnText.Add("Hola");
            columnInt.Add("1");
            columnInt.Add("2");
            columnInt.Add("3");
            columnDouble.Add("1,1");
            columnDouble.Add("2,1");
            columnDouble.Add("3,1");
            Db.AddTable(table);
            Db.Delete(table.Name,columnText, "Hola");

            List<List<String>> resultOk = new List<List<String>>();

            List<String> listInt = new List<string>();
            List<String> listDouble = new List<string>();
            List<String> listText = new List<string>();
            listText.Add("Adios");
            listInt.Add("2");
            listDouble.Add("2,1");
            resultOk.Add(listInt);
            resultOk.Add(listDouble);
            resultOk.Add(listText);

            for (int i = 0; i < table.GetListTableColumn().Count; i++)
            {
                for (int j = 0; j < table.GetListTableColumn()[i].GetList().Count; j++)
                {
                    Assert.AreEqual(resultOk[i][j], table.GetListTableColumn()[i].GetList()[j]);
                }

            }
        }
    



        [TestMethod]
        public void TestCreate()
        {
            string path = @"../Debug/MyDB_CODIGO/bd1.txt";
            Db.Create("bd1");
            Assert.IsTrue(File.Exists(path));
            
        }

        /*[TestMethod]
        public void TestLoad()  //open
        {
            string path = @"../Debug/MyDB_CODIGO/bd4.txt";
            //crear una base datos
            Db.Create("bd4");
            //meter contenido
            List<TableColumn> columns = new List<TableColumn>();
            Table t1 = new Table("empleados",columns);
            TableColumn tc = new TableColumn("id", DataType.Int);
            tc.GetList().Add("3");
            columns.Add(tc);
            Db.AddTable(t1);
                    //FALTARIA AÑADIR EL SAVE()
            Db.Close("bd4");
            Db.Load("bd4");
          
          




            //intentar abrir




            //intertar abrir una no existente









        }*/

        [TestMethod]
        public void TestSaveAndLoad()
        {
           // Db.Create("bd2");
            List<TableColumn> columnasT1 = new List<TableColumn>();
            Table t1 = new Table("empleados",columnasT1);
            TableColumn tc1 = new TableColumn("nombre", DataType.Text);
            tc1.GetList().Add("Ana");
            columnasT1.Add(tc1);

            List<TableColumn> columnasT2 = new List<TableColumn>();
            Table t2 = new Table("directores", columnasT2);
            TableColumn tc2 = new TableColumn("nombres", DataType.Text);
            TableColumn tc21 = new TableColumn("apellidos", DataType.Text);
            TableColumn tc22 = new TableColumn("edad", DataType.Text);
            tc2.GetList().Add("Fran");
            tc21.GetList().Add("Perez");
            tc22.GetList().Add("32");

            columnasT2.Add(tc2);
            columnasT2.Add(tc21);
            columnasT2.Add(tc22);

            Db.AddTable(t1);
            Db.AddTable(t2);
            Db.Save("bd2");
            Db.Close("bd2");
                                  Console.WriteLine(Db.GetList().Contains(t2));
            //COMPROBAMOS
            Db.Load("bd2");
           
            if (Db.GetList().Find(tables => tables.GetTableName().Equals(t1)) != null)
            {
                Console.WriteLine("Lo ha guardado");
            }
            Db.Close("bd2");

        }
        [TestMethod]
        public void TestAddTable()
        {
            Db.Create("bd3");
            List<TableColumn> li = new List<TableColumn>();
            Table t1 = new Table("empleados", li);
            Db.AddTable(t1);

    
            //COMPROBAMOS
          
            if (Db.GetList().Find(tables => tables.GetTableName().Equals(t1)) !=null )
            {
                Console.WriteLine("Se ha creado");
            }


        }

            /*
        [TestMethod]
        public void DropDatabaseTest()
        {
            string path = @"../Debug/MyDB_CODIGO/database.txt";

            Db.Create("database");

              Db.DropDatabase("database");
                Assert.IsTrue(File.Exists(path)==false);
        }
        */

        [TestMethod]
        public void TestClose()
        {
            Db.Create("bd6");

        }
        //[TestMethod]
        //public void TestDeleteTable()
        //{
        //    Db.Load("bd1");
        //    List<TableColumn> li = new List<TableColumn>();
        //    Table t1 = new Table("empleados", li);
        //    Db.AddTable(t1);

        //    Db.DeleteTable(t1);

        //    //COMPROBAMOS

        //    if (Db.GetList().Find(tables => tables.GetTableName().Equals(t1)) == null)
        //    {
        //        Console.WriteLine("Lo ha borrado");
        //    }
        //}
    }
}
